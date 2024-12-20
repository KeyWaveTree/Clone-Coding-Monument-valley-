using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class Walkable : MonoBehaviour
{

    //Verify Adjacency Linked List
    [SerializeField]
    private Transform[] directionPath;
    [SerializeField]
    private float maxDist;
    [SerializeField]
    private const int cardinalPoint = 4;


    //private variable lay
    private LayerMask layerMask;
    private bool[] isHits;
    private Vector3[] directions;
    private RaycastHit[] destinations;

    //this GameObject
    private Transform node;
    private Vector3 nodePosition;

    private void Start()
    {
           
        maxDist = (maxDist == 0f) ? 1f : maxDist;

        //layermask 설정 
        layerMask = (1 << LayerMask.NameToLayer("Path")) |
                    (1 << LayerMask.NameToLayer("Top")) |
                    (1 << LayerMask.NameToLayer("SenceArea"));
        //Raycast의 결과를 담을 수 있는 배열 생성
        isHits = new bool[cardinalPoint];//상하좌우 4방향이니 4개 생성 
        //Raycast의 목적지 
        destinations = new RaycastHit[cardinalPoint];

        //Raycast의 방향
        directions = new Vector3[cardinalPoint];
        directions[0] = Vector3.forward;
        directions[1] = Vector3.left;
        directions[2] = Vector3.back;
        directions[3] = Vector3.right;

        //경로 초기화 
        directionPath = new Transform[cardinalPoint];

        //현재 이 스크립트가 삽입되어있는 노드를 가지고와서 vector3위치로 저장한다. 
        node = this.transform; //현재 walkable scripts를 가지고 있는 오브젝트를 저장한다. 
        nodePosition = node.position;//오브젝트의 포지션을 저장한다. 

        //굳이 Invoke를 안써도 될것 같음. 
        //AdjancencyPathFindToRay();
    }

    private int AdjancencyPathFindToRay()
    {
        int index;
        int hitCount = 0;//현재 이 스크립트를 가지고 있는 오브젝트에서 발사한 ray를 맞은 오브젝트 개수 
        for (index = 0; index < cardinalPoint; index++)
        {

            //각 상하좌우방향으로 (y축 제외) raycast를 쏜다.
            //쏜 방향에 Trigger가 맞았다면 
            //ray의 최대거리는 1f 이다.
            //굳이 안맞으면 false여서 초기화 안해도 됨. 
            isHits[index] = Physics.Raycast(nodePosition,
                                           directions[index],
                                           out destinations[index],
                                           maxDist,
                                           layerMask);

            //만약 지정된 Object가 맞지 않았다면 
            if (!isHits[index]) continue;
            //맞았으면 오브젝트 개수 1개 증가 
            hitCount++;
            //맞은 오브젝트가 원본이 아니라는 뜻
            //원본이 아니라면 현재 오브젝트의 부모를 가리켜라 

            directionPath[index] = destinations[index].transform;
            //Debug.Log(index + " 번호에 " + destinations[index].transform+"삽입"); -> 삽입 잘 됨. 

        }

        return hitCount;

    }


    public bool checkHitCount(int hitObjectCount)
    {
        //만약 현재 오브젝트에서 Ray를 맞은 노드 개수가 hitObjectCount개라면 return true
        if (hitObjectCount == AdjancencyPathFindToRay()) return true;
        else return false;
    }
    //만약 현재 오브젝트에서 Ray를 맞은 노드 개수가 1개라면 경로가 변경a될 수 있는 길 지점이다.
    //bridge의 각도를 임의로 변경하여 Walkable코드들을 AdjancencyPathFindToRay()한번 더하고 경로가 변경될 지점을 찾은 다음 함수의 인자값으로 전달해준다. 
    //if (hitCount == 1)
    //{
            
    //}
}
