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


    //angle�� ���ؼ�
    private Quaternion targetRotation;
    private float[] currentAngle;
    private float[] rangeAngle;//�Ӱ� ������ ���ؼ�  
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
            //������ ���� A->B ���� ��Ȯ�� ����, ���� �ð� ����, ����
            //������ damp A->B ���� ��Ȯ�� ����, ������ �ð� ����, ����/����
            valve.rotation = Quaternion.Slerp(valve.rotation, targetRotation, 5 * Time.fixedDeltaTime);
        }
        //���� ��� ��ġ�� ���ߴٸ� false�� �����. 
        if (Input.GetMouseButtonUp(0))
        {
            AutoSetRotation();
            isValveOneThouch = false;
        }

        //���콺 �� Ŭ���� ������ �ʾ�����   
        if (!Input.GetMouseButtonDown(0)) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isHit = Physics.Raycast(ray,out RaycastHit loadHit, Mathf.Infinity, layerMask);//origin: ��� ��ġ, ����(������)���������� ����, �ִ� �Ÿ�, layer
            
        //if(isHit) Debug.Log("name : " + loadHit.collider.name); adjancency area�� ��� 
        if (isHit && (loadHit.collider.tag == "PathPoint"))
        {
            
            Debug.Log("name : " + loadHit.collider.name);
            hitPoint = loadHit.transform;
        }

        //���� Ŭ���� ����� BridgeValve��� -> 
        //���⸦ Ŭ���� �ѹ��̻��̶� ������ �Ұ��� true�� ����� ���콺�� ������ false�� 
        if (isHit && (loadHit.collider.tag == "BridgeValve"))
        {
            Debug.Log("ȸ��");
            isValveOneThouch = true;
            //hit�� �� transfrom.rotation�� ������ͼ� 
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
        //���� ���� false�� ���·� ��ư�� �����ٸ� ���� ����. 
        if (!isValveOneThouch) return;

        int index = 0;
        //valve.rotation.y-> 4���� rotation ���� -> ���Ϸ� �ޱ۷� �ٲ�� ��. 
        float valveAngle = valve.eulerAngles.y;

        for (index = 0; index < maxRange; index++)
        {

            Debug.Log("���� ����" + valveAngle);
         
            if(Mathf.Abs(rangeAngle[index] - valveAngle)  <= 45f || 360 - Mathf.Abs(rangeAngle[index] - valveAngle) <= 45f)
            {
                targetRotation = Quaternion.Euler(0, rangeAngle[index], 0);
                Debug.Log("ȸ���� ����" + rangeAngle[index] + "<- " + valveAngle);

            }
        }
    }

    //��� Ŭ���� ������ ������
    public Transform GetMouseHitTransform()
    {
        return hitPoint;
    }
}
