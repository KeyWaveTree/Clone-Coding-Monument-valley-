using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    //origin object
    
    //origin scripts
    private MovePoint movePoint;//��ǥ��ο� ���� ��ũ��Ʈ 

    //utilize variable
    private Vector3 empty; //������ ���� 
    private List<Transform> pathPoints;//��� ��ǥ�� ��� 

    void Awake()
    {
        //1.�غ� �κ�
        //1- ���� �κ�
        empty = new Vector3(0, 0, 0);//vector empty

        //1-find �κ�
        movePoint = FindObjectOfType<MovePoint>(); //�� ������Ʈ�� �������ִ� ������Ʈ�� ã�Ƽ� ������Ʈ�� return �ض�.                                    

        //1-���� �κ�
        
        //1- ���� �� ���Ǻκ�
        //1-1 ����
        pathPoints = movePoint.GetPathPointList(); //pathPoints�ȿ� �ִ� ����� ����ؼ� �� ����Ʈ�� transfrom list ����

        //1-2 ����


        //2.���� �κ� 

        //��� pathPoint ������Ʈ�ȿ� Addcomponent�� �ؼ� Walkable ����
        foreach(Transform element in pathPoints)
        {
            //Walkable�� ������ �ٷ� ����(�� start�� ������)
            element.AddComponent<Walkable>();

        }
    }

    //Ư����ġ(��ư)���� player�� �����ϸ� �̺�Ʈ ó��(Ư�� ����� ȸ��)
    void PressedEventPathRotated()
    {

    }

    void FixedUpdate()
    {
      
    }
}
