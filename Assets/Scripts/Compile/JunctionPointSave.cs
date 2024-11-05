using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JunctionPointSave : MonoBehaviour
{
    //���� ���պ� Json���� ���� 
    public void SaveToJson()
    {
        string junctionData = JsonUtility.ToJson("[������ ��ü �־�� ��] ������ �ƴ�");
        string filePath = Application.persistentDataPath + "junctionData.json";

        System.IO.File.WriteAllText(filePath, junctionData);
    }
}
