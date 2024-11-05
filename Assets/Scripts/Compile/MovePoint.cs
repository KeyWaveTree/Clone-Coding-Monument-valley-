using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MovePoint : MonoBehaviour
{

    //변수를 inspector창에 정보를 볼 수 있도록 공개화 하는 attribute
    [SerializeField]
    private List<Transform> movePointSet; //플레이어가 움직일 수 있는 경로 집합 

    [SerializeField]
    Transform map;
    
    //compare function 
    int compare(Transform a, Transform b)
    {
        if (int.Parse(a.name) < int.Parse(b.name))
            return -1;
        return 1;
    }

    //컴파일 실행 옵션
    [ContextMenu("Find All Move point")]
    void FindAllMovePoint()
    {
        movePointSet.Clear();
        //맵이 없을때 지정해줘서 찾는다. 
        if (map == null)  map = GameObject.Find("Map").transform;

        Transform[] allChildren = map.GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if(child.tag == "PathPoint")
            {
                movePointSet.Add(child);
            }
        }
        movePointSet.Sort(compare);

    }

    //게임 메니저 코드가 List를 가지고오기 위한 메서드
    //static이면 가져와야 하는 변수도 static이여야 한다. 
    public List<Transform> GetPathPointList()
    {
        //만약 지금 movePointSet이 null 이라면 FindAllMovePoint먼저 실행하라. 
        if (movePointSet == null) FindAllMovePoint();

        //return을 해라 
        return movePointSet;
    }

}
