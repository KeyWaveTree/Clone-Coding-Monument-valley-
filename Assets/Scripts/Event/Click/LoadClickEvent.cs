using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

    
public class LoadClickEvent : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private Transform hitPoint;
    private Transform valve;
    private Ray ray;
    private bool isHit;
    private bool isValveOneThouch;
    private int layerMask;


    //angle에 대해서
    private Quaternion targetRotation;
    private float[] currentAngle;
    private float[] rangeAngle;//임계 범위에 대해서  
    private const int maxRange = 4;
    private const float maxAngle = 360f;



    private void Start()
    {
        hitPoint         = null;
        isHit            = false;
        isValveOneThouch = false;

        currentAngle = new float[maxRange+1] {45f, 135f, 225f, 315f, 405f};
        rangeAngle   = new float[maxRange] {90f, 180f, 270f, 0f};

        layerMask = (1 << LayerMask.NameToLayer("Path")) | 
                    (1 << LayerMask.NameToLayer("Top")) |
                    (1 << LayerMask.NameToLayer("BridgeValve"));
       
    }

    private void FixedUpdate()
    {
        if (valve && !isValveOneThouch)
        {
            //스무스 스탭 A->B 까지 정확히 도달, 일정 시간 내에, 감속
            //스무스 damp A->B 까지 정확히 도달, 적당한 시간 내에, 가속/감속
            valve.rotation = Quaternion.Slerp(valve.rotation, targetRotation, 5 * Time.fixedDeltaTime);
        }
        //먼저 기둥 터치를 안했다면 false로 만든다. 
        if (Input.GetMouseButtonUp(0))
        {
            AutoSetRotation();
            isValveOneThouch = false;
        }

        //마우스 좌 클릭을 누르지 않았을때   
        if (!Input.GetMouseButtonDown(0)) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isHit = Physics.Raycast(ray,out RaycastHit loadHit, Mathf.Infinity, layerMask);//origin: 쏘는 위치, 원점(시작점)에서부터의 방향, 최대 거리, layer
            
        //if(isHit) Debug.Log("name : " + loadHit.collider.name); adjancency area가 출력 
        if (isHit && (loadHit.collider.tag == "PathPoint"))
        {
            
            Debug.Log("name : " + loadHit.collider.name);
            hitPoint = loadHit.transform;
        }

        //내가 클릭한 대상이 BridgeValve라면 -> 
        //여기를 클릭을 한번이상이라도 했을때 불값을 true로 만들고 마우스를 땠을때 false로 
        if (isHit && (loadHit.collider.tag == "BridgeValve"))
        {
            Debug.Log("회전");
            isValveOneThouch = true;
            //hit가 된 transfrom.rotation을 가지고와서 
            valve = loadHit.transform;
        }

    }

    void Update()
    {
        if(isValveOneThouch)
        {
            valve.Rotate(0f, -Input.GetAxis("Mouse X") * rotateSpeed, 0f);
        }

        
    }

    void AutoSetRotation()
    {
        //만약 지금 false인 상태로 버튼을 눌렀다면 실행 안함. 
        if (!isValveOneThouch) return;

        int index = 0;
        //valve.rotation.y-> 4차원 rotation 형태 -> 오일러 앵글로 바꿔야 함. 
        float valveAngle = valve.eulerAngles.y;

        for (index = 0; index < maxRange; index++)
        {

            Debug.Log("현재 각도" + valveAngle);
         
            if(Mathf.Abs(rangeAngle[index] - valveAngle)  <= 45f || 360 - Mathf.Abs(rangeAngle[index] - valveAngle) <= 45f)
            {
                targetRotation = Quaternion.Euler(0, rangeAngle[index], 0);
                Debug.Log("회전할 각도" + rangeAngle[index] + "<- " + valveAngle);

            }
        }
    }

    //모든 클릭된 정보를 보내기
    public Transform GetMouseHitTransform()
    {
        return hitPoint;
    }
}
