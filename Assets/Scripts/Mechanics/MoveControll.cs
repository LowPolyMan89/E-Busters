using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class MoveControll : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _flashLight;
    [SerializeField] private Rigidbody _playerBody;
    [SerializeField] private float depth;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private DataProvider dataProvider;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Player player;
    [SerializeField] private float deadZoneRadius;
    [SerializeField] private float distanceDelta;
    private Vector3 EulerAngles;
    private float speed = 0;
    [SerializeField] private NavMeshAgent agent;
    AnimationController animationController;
    [SerializeField] private float positionYOffset;

    private Vector3 hitP;

    private float noizeAddSprintTime = 0f;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        _playerBody = _player.GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
        player = dataProvider.Player;
        animationController = new AnimationController();
        StartCoroutine(InitAnimator());
        
    }

    private IEnumerator InitAnimator()
    {
        yield return new WaitForSeconds(0.2f);
        animationController.InitController(dataProvider.Player.Animator);
    }

    private void AimTarget()
    {
        if (Input.GetMouseButton(1))
        {
            animationController.SetAim("AimingRifle", true);

            RaycastHit hit;
            if (Physics.Raycast(dataProvider.Player.LookPoint.position, dataProvider.Player.LookPoint.forward * 20f, out hit, 100f, layerMask))
            {
                float dist = Vector3.Distance(hit.point, dataProvider.Player.WeaponSlot.position);
                if (dist > 2f)
                {
                    dataProvider.Player.WeaponSlot.rotation = Quaternion.LookRotation((hit.point - dataProvider.Player.WeaponSlot.position) * dist);

                }
                else
                {
                    dataProvider.Player.WeaponSlot.rotation = Quaternion.LookRotation(dataProvider.Player.LookPoint.forward * 20f);
                }
                hitP = hit.point;
            }
            else
            {
                dataProvider.Player.WeaponSlot.rotation = Quaternion.LookRotation(dataProvider.Player.LookPoint.forward * 20f);

            }


            if (Input.GetMouseButton(0))
            {
                dataProvider.Player.CurrentWeapon.Shoot();
            }
        }
        else
        {
            animationController.SetAim("AimingRifle", false);
        }
    }

    private void AimItem()
    {
        if (Input.GetMouseButton(1))
        {
            animationController.SetAim("AimingRifle", true);

            if (Input.GetMouseButton(0))
            {
                dataProvider.Player.ItemInActiveSlot.Use();
            }
        }
        else
        {
            animationController.SetAim("AimingRifle", false);
        }
    }

    private void KeyWord()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _flashLight.SetActive(!_flashLight.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.R) && dataProvider.Player.CurrentWeapon)
        {
            dataProvider.Player.CurrentWeapon.Reload(dataProvider.Player.CurrentWeapon.ReloadTime);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            dataProvider.Events.EnteractiveAction();
            dataProvider.Events.OpenTerminal(dataProvider.Player.ClosesTerminal, dataProvider.Player.ClosesTerminal);
            dataProvider.Player.Inventory.PickUpItems();
        }
    }

    private void PlayerRotation()
    {
        var camera = Camera.main;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var playerTransform = _player.transform;
        var playerPosition = playerTransform.position;

        var point = camera.ScreenToWorldPoint(Input.mousePosition);
        var height = playerPosition.y + positionYOffset - point.y;
        point += ray.direction.normalized * height / Mathf.Cos(Vector3.Angle(ray.direction, playerTransform.up) * Mathf.Deg2Rad);
        point = new Vector3(point.x, playerTransform.position.y, point.z);

        EulerAngles = Quaternion.LookRotation(point - playerTransform.position).eulerAngles;
        //Vector3.MoveTowards(EulerAngles, point, distanceDelta * Time.deltaTime);

        if (Vector3.Distance(point, playerPosition) > deadZoneRadius)
        {
            // playerTransform.LookAt(EulerAngles);
            playerTransform.localRotation = Quaternion.Lerp(playerTransform.localRotation, Quaternion.Euler(EulerAngles), distanceDelta * Time.deltaTime);
            // playerTransform.localEulerAngles = new Vector3(0f, _player.transform.localRotation.eulerAngles.y, 0f);
        }
        _cameraTransform.position = new Vector3(playerPosition.x + _cameraOffset.x, _cameraOffset.y, playerPosition.z + _cameraOffset.z);
    }

    private void Update()
    {

        KeyWord();

        if(dataProvider.Player.CurrentWeapon)
            AimTarget();
        else if(dataProvider.Player.ItemInActiveSlot)
             AimItem();
        else
        {
            animationController.SetAim("AimingRifle", false);
        }

        PlayerRotation();

        if (dataProvider.BattleUI.TerminalPanel.isActiveAndEnabled)
        {
            return;
        }

        dataProvider.Player.WeaponSlot.Rotate(dataProvider.Player.WeaponSlot.forward, 0f);

        

        if(dataProvider.Player.CurrentWeapon)
        {
            animationController.PickUpItemAnim("PickUpRifle", "Rifle", true);
        }
        else
        {
            animationController.PickUpItemAnim("PickUpRifle", "Rifle", false);
        }

        if (dataProvider.Player.ItemInActiveSlot)
        {
            animationController.PickUpItemAnim("PickUpRifle", "Rifle", true);
        }
        else
        {
            animationController.PickUpItemAnim("PickUpRifle", "Rifle", false);
        }
    }

    private void SwitchWeapon()
    {
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hitP, 0.2f);
    }

    private void FixedUpdate()
    {
        if(!player || dataProvider.BattleUI.TerminalPanel.isActiveAndEnabled)
        {
            return;
        }

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float localspeed = 0f;

            if(Input.GetKey(KeyCode.LeftShift) && !Input.GetMouseButton(1))
            {
                localspeed = player.PlayerStats.SprintSpeed;
                dataProvider.Events.NoizeChangeEvent(player.PlayerStats.NoizePerSprint * 0.01f, player.PlayerStats.NoizePerSprint * 0.01f);
                animationController.Move("Run", "Run", true);
            }
            else if(Input.GetMouseButton(1))
            {
                if(player.CurrentWeapon)
                     localspeed = player.PlayerStats.AimSpeed;
                else if(player.ItemInActiveSlot)
                    localspeed = 0;

                animationController.Move("Run", "Run", false);
            }
            else
            {
                localspeed = player.PlayerStats.MoveSpeed;
                animationController.Move("Run", "Run", false);
            }

            speed = Mathf.Lerp(speed, localspeed, 10 * Time.deltaTime);

            Vector3 direction = (Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical")).normalized;

            agent.Move(direction * speed * Time.deltaTime);

            animationController.Move("Walking", "Walking", true);
        }
        else
        {
            speed = 0;
            animationController.Move("Walking", "Walking", false);
            animationController.Move("Run", "Run", false);
        }


    }

    private class AnimationController
    {
        private Animator animator;

        public void InitController(Animator value)
        {
            animator = value;
        }


        public void SetAim(string id,  bool value)
        {
            if(animator)
                animator.SetBool(id, value);
        }

        public void PickUpItemAnim(string id, string itemType, bool value)
        {
            if (animator)
                animator.SetBool(id, value);
        }

        public void Move(string id, string moveType, bool value)
        {
            if (animator)
                animator.SetBool(id, value);
        }
    }
}


