using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadClickEvent : MonoBehaviour
{
    private Transform hitPoint;
    private Ray ray;
    private bool isHit;
    private int layerMask;
   
    private void Start()
    {
        layerMask = (1 << LayerMask.NameToLayer("Path")) | (1<< LayerMask.NameToLayer("Top"));
        hitPoint = null;
    }

    private void FixedUpdate()
    {
        //���콺 �� Ŭ���� ��������  
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("click");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isHit = Physics.Raycast(ray,out RaycastHit loadHit, Mathf.Infinity, layerMask);//origin: ��� ��ġ, ����(������)���������� ����, �ִ� �Ÿ�, layer
            
            //if(isHit) Debug.Log("name : " + loadHit.collider.name); adjance area�� ��� 

            if (isHit && loadHit.collider.tag == "PathPoint")
            {
            
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
