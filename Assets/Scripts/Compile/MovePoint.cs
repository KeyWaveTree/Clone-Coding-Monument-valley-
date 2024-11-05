using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MovePoint : MonoBehaviour
{

    //������ inspectorâ�� ������ �� �� �ֵ��� ����ȭ �ϴ� attribute
    [SerializeField]
    private List<Transform> movePointSet; //�÷��̾ ������ �� �ִ� ��� ���� 

    [SerializeField]
    Transform map;
    
    //compare function 
    int compare(Transform a, Transform b)
    {
        if (int.Parse(a.name) < int.Parse(b.name))
            return -1;
        return 1;
    }

    //������ ���� �ɼ�
    [ContextMenu("Find All Move point")]
    void FindAllMovePoint()
    {
        movePointSet.Clear();
        //���� ������ �������༭ ã�´�. 
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

    //���� �޴��� �ڵ尡 List�� ��������� ���� �޼���
    //static�̸� �����;� �ϴ� ������ static�̿��� �Ѵ�. 
    public List<Transform> GetPathPointList()
    {
        //���� ���� movePointSet�� null �̶�� FindAllMovePoint���� �����϶�. 
        if (movePointSet == null) FindAllMovePoint();

        //return�� �ض� 
        return movePointSet;
    }

}
