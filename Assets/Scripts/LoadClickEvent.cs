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
        //마우스 좌 클릭을 눌렀을때  
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("click");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isHit = Physics.Raycast(ray,out RaycastHit loadHit, Mathf.Infinity, layerMask);//origin: 쏘는 위치, 원점(시작점)에서부터의 방향, 최대 거리, layer
            
            //if(isHit) Debug.Log("name : " + loadHit.collider.name); adjance area가 출력 

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
