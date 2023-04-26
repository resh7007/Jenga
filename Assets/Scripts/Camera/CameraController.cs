using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    public float turnSpeed = 4.0f;
    public Transform _target;
    private Vector3 offset;
    public void SetTarget(Transform target)
    {
        _target = target;
        offset =  new Vector3(target.position.x, 5 , -4);
        transform.position =_target.position + offset;
        transform.LookAt(target);

    }

    private void Update()
    {
        if(_target==null) return;

        if (Input.GetMouseButton(1))
        { 
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            transform.position = _target.position + offset;
            transform.LookAt(_target.position);
        }
    }
}
