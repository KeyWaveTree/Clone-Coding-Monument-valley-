using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <tip>
/// ���� ��� ������Ʈ�� �ִ� ��ũ��Ʈ ���� �޼��带 Ȱ���ؾ� �Ҷ�  
/// �̹� ��ũ��Ʈ�� ��ϵǾ� �ִٸ� �� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ã�Ƽ� ������ȴ�. 
/// �׷��� ������ ���� �ʰ� static�� ������� �ʰ� ������ �����ϴ�. 
/// </tip>

public class GameManager : MonoBehaviour
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

    // Start is called before the first frame update

    
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
        foreach(Transform element in pathPoints)
        {
            //Walkable�� ������ �ٷ� ����(�� start�� ������)
            element.AddComponent<Walkable>();
        }
    }

    //�÷��̾� �����̴� �ڵ� 
    void MovePlayer()
    {
        //���� ����� clickPoint�� ���� ī�޶󿡼� Ŭ���� GetMouseHitTransform�� �� Transfrom�� ���ٸ� return 
        if (clickPoint == loadClick.GetMouseHitTransform()) return;

        clickPoint = loadClick.GetMouseHitTransform();

        //top ���̾�θ� �ν��ϴ� ī�޶� ������ ������ ���̾ ��ϰ� ���� ������ش�. 
        player.gameObject.layer = clickPoint.gameObject.layer;
        player.transform.SetParent(clickPoint);

        empty.Set(0f, 0.5f, 0f);
        player.transform.position = clickPoint.position + empty;
        empty.Set(0f, 0f, 0f);
    }

    void FixedUpdate()
    {
        MovePlayer();    
    }
}
