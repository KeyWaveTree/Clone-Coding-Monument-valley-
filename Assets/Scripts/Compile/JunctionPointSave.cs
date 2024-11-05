using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JunctionPointSave : MonoBehaviour
{
    //도로 접합부 Json으로 저장 
    public void SaveToJson()
    {
        string junctionData = JsonUtility.ToJson("[저장할 객체 넣어야 함] 문장이 아님");
        string filePath = Application.persistentDataPath + "junctionData.json";

        System.IO.File.WriteAllText(filePath, junctionData);
    }
}
