using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControll : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _flashLight;
    [SerializeField] private Rigidbody _playerBody;
    [SerializeField] private float depth;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private DataProvider _dataProvider;
    [SerializeField] private LayerMask layerMask;
    private void Start()
    {
        _dataProvider = DataProvider.Instance;
        _playerBody = _player.GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
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
        if(Input.GetMouseButton(0))
        {
            _dataProvider.CurrentWeapon.Shoot();
        }


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            if(hit.transform.tag == "Floor")
            {
                _player.transform.LookAt(hit.point);
            }        
        }

        _player.transform.rotation = Quaternion.Euler(new Vector3(0, _player.transform.rotation.eulerAngles.y, 0));

        _cameraTransform.position = new Vector3(_player.transform.position.x + _cameraOffset.x, _cameraOffset.y, _player.transform.position.z + _cameraOffset.z);

    }




    private void FixedUpdate()
    {
        _playerBody.AddForce(Vector3.right * _speed * Input.GetAxis("Horizontal"), ForceMode.Force);
        _playerBody.AddForce(Vector3.forward * _speed * Input.GetAxis("Vertical"), ForceMode.Force);
    }
}
