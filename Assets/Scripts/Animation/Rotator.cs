using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 orientation;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(orientation, speed + Time.deltaTime);
    }
}
