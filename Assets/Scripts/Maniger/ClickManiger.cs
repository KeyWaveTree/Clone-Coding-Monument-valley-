using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManiger : MonoBehaviour
{

    //origin object  
    [SerializeField]
    private GameObject player;//�÷��̾� 
    [SerializeField]
    private Camera mainCamera;//ī�޶�

    //origin scripts
    private MovePoint movePoint;//��ǥ��ο� ���� ��ũ��Ʈ 
    private LoadClickEvent loadClick; //ī�޶� �� Ŭ�� �̺�Ʈ ��ũ��Ʈ 

    //utilize variable
    private Vector3 empty; //������ ���� 
    private Transform clickPoint; //Ŭ���� �� ��ǥ -> Ȥ�ó� Transform���� Ȱ���� �� �ֱ⶧���� Vector���� Transform���� ���� 
    private List<Transform> pathPoints;//��� ��ǥ�� ��� 
    
    //Mouse down up ���� 
    


    void Start()
    {
        //1.�غ� �κ�

        //1- ���� �κ�
        empty = new Vector3(0, 0, 0);//vector empty

        //1-find �κ�
        movePoint = FindObjectOfType<MovePoint>(); //�� ������Ʈ�� �������ִ� ������Ʈ�� ã�Ƽ� ������Ʈ�� return �ض�.                                    


        //1-���� �κ�
        if (player == null) player = GameObject.FindWithTag("Player");//�÷��̾� ������ null �̶�� �±װ� player�� ã�Ƽ� �־�� 
        if (mainCamera == null) mainCamera = Camera.main;//ī�޶� ���ٸ� main ī�޶� ������ �Ͷ�. 

        //1- ���� �� ���Ǻκ�
        //1-1 ����
        pathPoints = movePoint.GetPathPointList(); //pathPoints�ȿ� �ִ� ����� ����ؼ� �� ����Ʈ�� transfrom list ����
        loadClick = mainCamera.GetComponent<LoadClickEvent>(); //main ī�޶� �ִ� Ŭ�� �̺�Ʈ ������Ʈ�� ������Ͷ�.  
        clickPoint = loadClick.GetMouseHitTransform();

        //1-2 ����


        //2.���� �κ� 
        //ó�� �÷��̾� ��ġ�� �ݵ�� start point ��ġ�� ������ �ؾ� �Ѵ�. 
        empty.Set(0f, 0.5f, 0f);//���� ���͸� Ȱ���ؼ� ���ο� ������ �����Ѵ�.
        player.transform.position = pathPoints[0].position + empty;
        empty.Set(0f, 0f, 0f);

        //��� pathPoint ������Ʈ�ȿ� Addcomponent�� �ؼ� Walkable ����
        foreach (Transform element in pathPoints)
        {
            //Walkable�� ������ �ٷ� ����(�� start�� ������)
            element.AddComponent<Walkable>();
        }
    }

    //�÷��̾� �����̴� �ڵ� 
    void MovePlayer()
    {
        Transform currentObject= loadClick.GetMouseHitTransform();
        //���� ����� clickPoint�� ���� ī�޶󿡼� Ŭ���� GetMouseHitTransform�� �� Transfrom�� ���ٸ� return 
        //�Ǵ� Ŭ���� ���콺 ������Ʈ �±װ� "PathPoint"�� �ƴ϶��  
        if (clickPoint == currentObject || currentObject.CompareTag("PathPoint")) return;

        clickPoint = currentObject;

        //top ���̾�θ� �ν��ϴ� ī�޶� ������ ������ ���̾ ��ϰ� ���� ������ش�. 
        player.gameObject.layer = clickPoint.gameObject.layer;
        player.transform.SetParent(clickPoint);

        empty.Set(0f, 0.5f, 0f);
        player.transform.position = clickPoint.position + empty;
        empty.Set(0f, 0f, 0f);
    }

    //�޴����� ó���� ���ٻ� �Է��� �����ʴ´�. 
    void RotateBridge()
    {
        Transform currentObject = loadClick.GetMouseHitTransform();
        //���� ���� Ŭ���� ������Ʈ�� BridgeValve �±׸� ���� �ʴٸ� �Ǵ�  
        //onMouseDrag
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotateBridge();
    }
}
