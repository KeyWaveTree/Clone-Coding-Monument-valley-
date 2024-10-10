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
    private int layerMask;
    private bool[] isHits;
    private Vector3[] directions;
    private RaycastHit[] destinations;

    //this GameObject
    private Transform node;
    private Vector3 nodePosition;
    
    private void Start()
    {
        maxDist = (maxDist == 0f) ? 1f: maxDist;

        //layermask ���� 
        layerMask = (1 << LayerMask.NameToLayer("Path")) |
                    (1 << LayerMask.NameToLayer("Top"))  |
                    (1 << LayerMask.NameToLayer("SenceArea"));
        //Raycast�� ����� ���� �� �ִ� �迭 ����
        isHits = new bool[cardinalPoint];//�����¿� 4�����̴� 4�� ���� 
        //Raycast�� ������ 
        destinations = new RaycastHit[cardinalPoint];

        //Raycast�� ����
        directions = new Vector3[cardinalPoint];
        directions[0] = Vector3.forward;
        directions[1] = Vector3.left;
        directions[2] = Vector3.back;
        directions[3] = Vector3.right;

        //��� �ʱ�ȭ 
        directionPath = new Transform[cardinalPoint];

        //���� �� ��ũ��Ʈ�� ���ԵǾ��ִ� ��带 ������ͼ� vector3��ġ�� �����Ѵ�. 
        node = this.transform; //���� walkable scripts�� ������ �ִ� ������Ʈ�� �����Ѵ�. 
        nodePosition = node.position;//������Ʈ�� �������� �����Ѵ�. 

        //���� Invoke�� �Ƚᵵ �ɰ� ����. 
        Invoke(nameof(AdjancencyPathFindToRay), 0.1f);
    }
    void AdjancencyPathFindToRay()
    {
        int index;

        for(index=0;index < cardinalPoint;index++)
        {

            //�� �����¿�������� (y�� ����) raycast�� ���.
            //�� ���⿡ Trigger�� �¾Ҵٸ� 
            //ray�� �ִ�Ÿ��� 1f �̴�.
            //���� �ȸ����� false���� �ʱ�ȭ ���ص� ��. 
            isHits[index] = Physics.Raycast(nodePosition,
                                           directions[index], 
                                           out destinations[index],
                                           maxDist, 
                                           layerMask);

            if (!isHits[index]) continue;
            //���� ������Ʈ�� ������ �ƴ϶�� ��
            //������ �ƴ϶�� ���� ������Ʈ�� �θ� �����Ѷ� 
            //destinations[index] = destinations[index]; -> ���� ���ص� �� �Ͱ���. ����: ��¥�� �浹 �ɰ� ���Ƽ� 
            directionPath[index] = destinations[index].transform;
            Debug.Log(index + " ��ȣ�� " + destinations[index].transform+"����");

            
        }

    }
}
