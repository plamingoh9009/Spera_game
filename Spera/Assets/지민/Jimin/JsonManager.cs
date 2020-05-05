using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JsonManager : MonoBehaviour
{
    private void Awake()
    {
        //실행했는데 디렉토리에 json폴더가 없다면(최초실행시)
        if(!Directory.Exists(Application.persistentDataPath + "/Json/"))
        {
            //디렉토리에 폴더 생성해줌
            Directory.CreateDirectory(Application.persistentDataPath + "/Json/");
            Directory.CreateDirectory(Application.persistentDataPath + "/Json/Item/");
            Directory.CreateDirectory(Application.persistentDataPath + "/Json/InGameData/");
        }
        else
        {
            //json폴더가 있다면(재시작시) 승패기록을 초기화해줌
            ClearJsonFolder("InGameData");
        }
    }
    public static void ClearJsonFolder(string folderName)
    {
        ///Json/folderName 안의 모든 파일의 이름을 가져옴
        string[] allFile = Directory.GetFiles(Application.persistentDataPath + "/Json/" + folderName);

        for (int i = 0; i < allFile.Length; i++)
        {
            //다 지움
            File.Delete(allFile[i]);
        }
    }

    public static void SaveJsonData(object data, string folderName, string saveName)
    {
        //폴더가 없으면 폴더 생성
        if (!Directory.Exists(Application.persistentDataPath + "/Json/" + folderName))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Json/" + folderName);
        }

        //json데이터로 만듬
        string jsonData = JsonConvert.SerializeObject(data);
        //경로지정
        string path = Application.persistentDataPath + "/Json/"+ folderName + "/" + saveName + ".json";
        //파일생성
        File.WriteAllText(path, jsonData);
    }

    public static T LoadJsonData<T>(string folderName, string loadName)
    {
        //경로지정
        string path = Path.Combine(Application.persistentDataPath + "/Json/" + folderName, loadName + ".json");
        //경로에 있는 json데이터파일 가져옴
        string jsonData = File.ReadAllText(path);
        //json데이터 반환
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

}
