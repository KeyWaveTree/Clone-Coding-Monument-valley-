using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadConnecter : MonoBehaviour
{
    [SerializeField]
    private List<Transform> serializePoint; 
    private HashSet<Transform> connecterSet;//다른 노드와 연결 및 해제 할 수 있는 포인트 후보들 
    
    private List<Transform> pointSet; // 도로 정보들 
    
    private MovePoint pointObject;

    private Quaternion valveRotate;
    private int index, angle;

    [SerializeField]
    private Transform bridgeTransform;
    [SerializeField]
    private float[] rotationAngle;


    //알고리즘
    //1. bridge 회전이 안된 상태로 도로를 검사한다.  
    //2. walkable 스크립트를 가진 도로중 노드가 1개인 것을 찾아서 넣는다. 
    //3. bridge를 90도 회전한다. 
    //4. 2번을 다시 수행한다. 

    //초기화 영역 
    void Start()
    {
        //만약 bridge트렌스폼이 없다면 찾아서 넣어라.
        if (bridgeTransform == null) bridgeTransform = GameObject.Find("Bridge").transform;

        //특정 각도로 회전할 수치가 없다면  
        if(rotationAngle.Length == 0 ) rotationAngle = new float[] { 270f, 180f, 90f, 0f };

        //hashSet 초기화
        connecterSet = new HashSet<Transform>();
        
        //valveRotate 초기화 
        valveRotate = new Quaternion(0f, 0f, 0f, 0f);

        //MovePoint에서 만든 경로지점들을 다 가지고 온다.
        pointObject = GameObject.Find("PointMap").GetComponent<MovePoint>();
        pointSet = pointObject.GetPathPointList();

        Invoke(nameof( FindJunctionPoint), 0.5f);
       
    }
    
    //알고리즘 동작 
    void FindJunctionPoint()
    {
        Transform pushPoint;
        //현재 설정된 회전 축만큼 반복
        for (angle = 0; angle < rotationAngle.Length; angle++)
        {
            //valveRotatie를 지금 지정한 각도만큼 지정 
            valveRotate.Set(0f, rotationAngle[angle], 0f, 0f);
            //회전다리의 회전축을 지정한 각도 만큼 회전
            bridgeTransform.rotation = valveRotate;

            //현재 스테이지있는 모든 경로들중에서 인접한 경로가 1이 하나라도 나올때 분리 될 수 있다고 판단. 
            for (index = 0; index < pointSet.Count; index++)
            {
                //만약 현재 지정된 경로에서 인접한 경로가 1개일때는 넣고 아닐때는 null을 넣어라 
                pushPoint = (pointSet[index].GetComponent<Walkable>().
                            checkHitCount(1)) ? pointSet[index] : null;
                //만약 null이라면 현재 경로는 원하는 경로가 아니므로 continue
                if (pushPoint == null) continue;
                //원하는 경로라면 삽입.
                connecterSet.Add(pushPoint);
            }
        }
        showTheValueInSet();
    }

    void showTheValueInSet()
    {
        foreach(var test in connecterSet)
        {
            var t = test;
            Debug.Log(t);
        }
        
    }

}
