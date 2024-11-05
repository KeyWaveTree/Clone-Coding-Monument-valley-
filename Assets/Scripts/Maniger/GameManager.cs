using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    //origin object
    
    //origin scripts
    private MovePoint movePoint;//좌표경로에 대한 스크립트 

    //utilize variable
    private Vector3 empty; //임의의 벡터 
    private List<Transform> pathPoints;//모든 좌표의 경로 

    void Awake()
    {
        //1.준비 부분
        //1- 정의 부분
        empty = new Vector3(0, 0, 0);//vector empty

        //1-find 부분
        movePoint = FindObjectOfType<MovePoint>(); //이 컴포넌트를 가지고있는 오브젝트를 찾아서 컴포넌트를 return 해라.                                    

        //1-조건 부분
        
        //1- 정의 및 조건부분
        //1-1 정의
        pathPoints = movePoint.GetPathPointList(); //pathPoints안에 있는 기능을 사용해서 각 포인트의 transfrom list 저장

        //1-2 조건


        //2.제어 부분 

        //모든 pathPoint 오브젝트안에 Addcomponent를 해서 Walkable 삽입
        foreach(Transform element in pathPoints)
        {
            //Walkable을 넣으면 바로 실행(단 start가 있을시)
            element.AddComponent<Walkable>();

        }
    }

    //특정위치(버튼)에서 player가 존재하면 이벤트 처리(특정 기둥을 회전)
    void PressedEventPathRotated()
    {

    }

    void FixedUpdate()
    {
      
    }
}
