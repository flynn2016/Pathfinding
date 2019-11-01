using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = this.GetComponent<Camera>();
    }
    void Update()
    {
        cam.orthographicSize += Input.mouseScrollDelta.y;
    }
}
