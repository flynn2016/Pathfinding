using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    Vector3 startpos;
    void Start()
    {
        startpos = this.transform.position;
        cam = this.GetComponent<Camera>();
    }
    void Update()
    {
        cam.orthographicSize += Input.mouseScrollDelta.y;
    }

    public void LookAt(Transform Target)
    {
        this.transform.position = startpos + Target.transform.position;
    }
}
