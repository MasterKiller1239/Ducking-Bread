using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    public Transform camTransform;

    Quaternion originalRotation;

    void Start()
    {
        camTransform = Camera.main.transform;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
