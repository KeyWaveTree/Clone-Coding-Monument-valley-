using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSignal : MonoBehaviour
{
    //버튼이 눌려졌을때 신호 전달 

    [SerializeField]
    private GameObject targetRotateObject; //회전할 오브젝트 표시
    [SerializeField]
    private Quaternion tartgetStartPosition; //초기 위치 
    [SerializeField]
    private Quaternion tartgetEndPosition; //목표 위치 

    // Start is called before the first frame update
    void Start()
    {
        //시작 위치 결정 
        targetRotateObject.transform.rotation = tartgetStartPosition; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    //버튼을 눌렀을때 발생 -> GameManiger로 
    void PressButton()
    {
        //게임 메니저의 메소드를 이용해서 정보를 전달함. 동작은 게임 메니저에서 작동함. 
    }

}
