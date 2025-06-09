using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;
    void LateUpdate()
    {
        if (cam == null)
            cam = Camera.main;

        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward);
    }
}
