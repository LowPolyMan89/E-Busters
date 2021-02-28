using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MoveControll : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _flashLight;
    [SerializeField] private Rigidbody _playerBody;
    [SerializeField] private float depth;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private DataProvider _dataProvider;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Player player;

    private float noizeAddSprintTime = 0f;

    private void Start()
    {
        _dataProvider = DataProvider.Instance;
        _playerBody = _player.GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
        player = _dataProvider.Player;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _flashLight.SetActive(!_flashLight.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            _dataProvider.CurrentWeapon.Reload(_dataProvider.CurrentWeapon.ReloadTime);
        }

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButton(0))
            {
                _dataProvider.CurrentWeapon.Shoot();
            }
        }



       // RaycastHit hit;
        var camera = Camera.main;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hit, 100f, layerMask))
        //{
        //    if(hit.transform.tag == "Floor")
        //    {
        //        _player.transform.LookAt(hit.point);
        //    }        
        //}
        //playerTransform.Rotate(_player.transform.up, _player.transform.rotation.eulerAngles.y);

        var playerTransform = _player.transform;
        var playerPosition = playerTransform.position;

        var point = camera.ScreenToWorldPoint(Input.mousePosition);
        var height = playerPosition.y - point.y;
        point += ray.direction.normalized * height / Mathf.Cos(Vector3.Angle(ray.direction, playerTransform.up) * Mathf.Deg2Rad);
        _player.transform.LookAt(point);
        playerTransform.localEulerAngles = new Vector3(0f, _player.transform.localRotation.eulerAngles.y, 0f);

        _cameraTransform.position = new Vector3(playerPosition.x + _cameraOffset.x, _cameraOffset.y, playerPosition.z + _cameraOffset.z);
    }




    private void FixedUpdate()
    {
        if(!player)
        {
            return;
        }

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float speed = 0;

            if(Input.GetKey(KeyCode.LeftShift) && !Input.GetMouseButton(1))
            {
                speed = player.PlayerStats.SprintSpeed;
                _dataProvider.Events.NoizeChangeEvent(player.PlayerStats.NoizePerSprint * 0.01f, player.PlayerStats.NoizePerSprint * 0.01f);
            }
            else if(Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift))
            {
                speed = player.PlayerStats.AimSpeed;
            }
            else
            {
                speed = player.PlayerStats.MoveSpeed;
            }


            _playerBody.AddForce(Vector3.right * speed * Input.GetAxis("Horizontal"), ForceMode.Force);
            _playerBody.AddForce(Vector3.forward * speed * Input.GetAxis("Vertical"), ForceMode.Force);
        }


    }
}
