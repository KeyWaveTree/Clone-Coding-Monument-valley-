using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DragDrop : MonoBehaviour
{
    bool rotating;
    float rotateSpeed = 0.5f;
    float fixAngle = 30.0f;
    Vector3 mousePos, offset, rotation;

    void Start()
    {
        rotating = false;
    }

    void OnMouseDrag()
    {
        rotating = true;
        mousePos = Input.mousePosition;
    }

    void OnMouseUp()
    {
        rotating = false;

        float yAngle = ((int)transform.localEulerAngles.y / (int)fixAngle) * fixAngle;
        Vector3 localEulerAngles = transform.localEulerAngles;

        localEulerAngles.y = yAngle;
        this.transform.localEulerAngles = localEulerAngles;
    }

    void Update()
    {
        if (rotating)
        {
            offset = (Input.mousePosition - mousePos);

            rotation.y = -(offset.x + offset.y) * rotateSpeed;

            transform.Rotate(rotation);

            mousePos = Input.mousePosition;
        }
    }
}
