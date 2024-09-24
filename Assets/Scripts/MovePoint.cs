using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MovePoint : MonoBehaviour
{


    //구조체, class를 inspector 창에 정보를 볼 수 있도록 공개화 하는 attribute
    //[SerializeReference]

    //변수를 inspector창에 정보를 볼 수 있도록 공개화 하는 attribute
    [SerializeField]
    //플레이어가 움직일 수 있는 경로 집합 
    private List<Transform> movePointSet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
