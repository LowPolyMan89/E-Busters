using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControll : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _flashLight;


    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _flashLight.SetActive(!_flashLight.activeSelf);
        }
    }

    //private void Update()
    //{
    //    _player.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    //    _player.transform.position += new Vector3(0, 0, Input.GetAxis("Vertical"));
    //}

    //private void FixedUpdate()
    //{
    //    _playerBody.AddForce(_player.transform.right * _speed * Input.GetAxis("Horizontal"), ForceMode.Force);
    //    _playerBody.AddForce(_player.transform.forward * _speed * Input.GetAxis("Vertical"), ForceMode.Force);
    //}
}
