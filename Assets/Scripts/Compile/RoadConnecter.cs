using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadConnecter : MonoBehaviour
{
    [SerializeField]
    private List<Transform> serializePoint; 
    private HashSet<Transform> connecterSet;//�ٸ� ���� ���� �� ���� �� �� �ִ� ����Ʈ �ĺ��� 
    
    private List<Transform> pointSet; // ���� ������ 
    
    private MovePoint pointObject;

    private Quaternion valveRotate;
    private int index, angle;

    [SerializeField]
    private Transform bridgeTransform;
    [SerializeField]
    private float[] rotationAngle;


    //�˰���
    //1. bridge ȸ���� �ȵ� ���·� ���θ� �˻��Ѵ�.  
    //2. walkable ��ũ��Ʈ�� ���� ������ ��尡 1���� ���� ã�Ƽ� �ִ´�. 
    //3. bridge�� 90�� ȸ���Ѵ�. 
    //4. 2���� �ٽ� �����Ѵ�. 

    //�ʱ�ȭ ���� 
    void Start()
    {
        //���� bridgeƮ�������� ���ٸ� ã�Ƽ� �־��.
        if (bridgeTransform == null) bridgeTransform = GameObject.Find("Bridge").transform;

        //Ư�� ������ ȸ���� ��ġ�� ���ٸ�  
        if(rotationAngle.Length == 0 ) rotationAngle = new float[] { 270f, 180f, 90f, 0f };

        //hashSet �ʱ�ȭ
        connecterSet = new HashSet<Transform>();
        
        //valveRotate �ʱ�ȭ 
        valveRotate = new Quaternion(0f, 0f, 0f, 0f);

        //MovePoint���� ���� ����������� �� ������ �´�.
        pointObject = GameObject.Find("PointMap").GetComponent<MovePoint>();
        pointSet = pointObject.GetPathPointList();

        Invoke(nameof( FindJunctionPoint), 0.5f);
       
    }
    
    //�˰��� ���� 
    void FindJunctionPoint()
    {
        Transform pushPoint;
        //���� ������ ȸ�� �ุŭ �ݺ�
        for (angle = 0; angle < rotationAngle.Length; angle++)
        {
            //valveRotatie�� ���� ������ ������ŭ ���� 
            valveRotate.Set(0f, rotationAngle[angle], 0f, 0f);
            //ȸ���ٸ��� ȸ������ ������ ���� ��ŭ ȸ��
            bridgeTransform.rotation = valveRotate;

            //���� ���������ִ� ��� ��ε��߿��� ������ ��ΰ� 1�� �ϳ��� ���ö� �и� �� �� �ִٰ� �Ǵ�. 
            for (index = 0; index < pointSet.Count; index++)
            {
                //���� ���� ������ ��ο��� ������ ��ΰ� 1���϶��� �ְ� �ƴҶ��� null�� �־�� 
                pushPoint = (pointSet[index].GetComponent<Walkable>().
                            checkHitCount(1)) ? pointSet[index] : null;
                //���� null�̶�� ���� ��δ� ���ϴ� ��ΰ� �ƴϹǷ� continue
                if (pushPoint == null) continue;
                //���ϴ� ��ζ�� ����.
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
