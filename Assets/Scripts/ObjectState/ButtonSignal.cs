using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSignal : MonoBehaviour
{
    //��ư�� ���������� ��ȣ ���� 

    [SerializeField]
    private GameObject targetRotateObject; //ȸ���� ������Ʈ ǥ��
    [SerializeField]
    private Quaternion tartgetStartPosition; //�ʱ� ��ġ 
    [SerializeField]
    private Quaternion tartgetEndPosition; //��ǥ ��ġ 

    // Start is called before the first frame update
    void Start()
    {
        //���� ��ġ ���� 
        targetRotateObject.transform.rotation = tartgetStartPosition; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    //��ư�� �������� �߻� -> GameManiger�� 
    void PressButton()
    {
        //���� �޴����� �޼ҵ带 �̿��ؼ� ������ ������. ������ ���� �޴������� �۵���. 
    }

}
