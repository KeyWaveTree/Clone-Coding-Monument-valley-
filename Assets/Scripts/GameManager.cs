using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <tip>
/// 만약 어느 오브젝트에 있는 스크립트 내부 메서드를 활용해야 할때  
/// 이미 스크립트에 등록되어 있다면 그 스크립트를 가지고 있는 오브젝트를 찾아서 넣으면된다. 
/// 그러면 생성을 하지 않고 static을 사용하지 않고도 조작이 가능하다. 
/// </tip>

public class GameManager : MonoBehaviour
{
    //origin object  
    [SerializeField]
    private GameObject player;//플레이어 
    [SerializeField]
    private Camera mainCamera;//카메라
    
    //origin scripts
    private MovePoint movePoint;//좌표경로에 대한 스크립트 
    private LoadClickEvent loadClick; //카메라 길 클릭 이벤트 스크립트 

    //utilize variable
    private Vector3 empty; //임의의 벡터 
    private Transform clickPoint; //클릭한 블럭 좌표 -> 혹시나 Transform으로 활용할 수 있기때문에 Vector에서 Transform으로 변경 
    private List<Transform> pathPoints;//모든 좌표의 경로 

    // Start is called before the first frame update

    
    void Start()
    {
        //1.준비 부분

        //1- 정의 부분
        empty = new Vector3(0, 0, 0);//vector empty

        //1-find 부분
        movePoint = FindObjectOfType<MovePoint>(); //이 컴포넌트를 가지고있는 오브젝트를 찾아서 컴포넌트를 return 해라.                                    
        

        //1-조건 부분
        if (player == null) player = GameObject.FindWithTag("Player");//플레이어 변수가 null 이라면 태그가 player를 찾아서 넣어라 
        if (mainCamera == null) mainCamera = Camera.main;//카메라가 없다면 main 카메라를 가지고 와라. 
        
        //1- 정의 및 조건부분
        //1-1 정의
        pathPoints = movePoint.GetPathPointList(); //pathPoints안에 있는 기능을 사용해서 각 포인트의 transfrom list 저장
        loadClick = mainCamera.GetComponent<LoadClickEvent>(); //main 카메라에 있는 클릭 이벤트 컴포넌트를 가지고와라.  
        clickPoint = loadClick.GetMouseHitTransform();

        //1-2 조건


        //2.제어 부분 
        //처음 플레이어 위치는 반드시 start point 위치에 삽입을 해야 한다. 
        empty.Set(0f, 0.5f, 0f);//기존 벡터를 활용해서 새로운 값으로 변경한다.
        player.transform.position = pathPoints[0].position + empty;
        empty.Set(0f, 0f, 0f);

        //모든 pathPoint 오브젝트안에 Addcomponent를 해서 Walkable 삽입
        foreach(Transform element in pathPoints)
        {
            //Walkable을 넣으면 바로 실행(단 start가 있을시)
            element.AddComponent<Walkable>();
        }
    }

    //플레이어 움직이는 코드 
    void MovePlayer()
    {
        //현재 저장된 clickPoint와 현재 카메라에서 클릭한 GetMouseHitTransform을 한 Transfrom이 같다면 return 
        if (clickPoint == loadClick.GetMouseHitTransform()) return;

        clickPoint = loadClick.GetMouseHitTransform();

        //top 레이어로만 인식하는 카메라 렌더링 때문에 레이어를 블록과 같게 만들어준다. 
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
