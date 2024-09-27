using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadClickEvent : MonoBehaviour
{
    private Transform hitPoint;
    private Ray ray;
    private bool isHit;
   
    private void Start()
    {
        hitPoint = null;
    }

    private void FixedUpdate()
    {
        //���콺 �� Ŭ���� ��������  
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("click");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isHit = Physics.Raycast(ray,out RaycastHit loadHit);//origin: ��� ��ġ, ����(������)���������� ����
            if(isHit && loadHit.collider.tag == "PathPoint")
            {
                //
                Debug.Log("name : " + loadHit.collider.name);
                hitPoint = loadHit.transform;
            }
        }
    }

    public Transform GetMouseHitTransform()
    {
        return hitPoint;
    }
}
