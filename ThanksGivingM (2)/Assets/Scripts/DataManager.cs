using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    [Serializable]
    public class AugMentData
    {
        public string ID;
        public string aug_type;
        public string unlock_info;
        public string aug_name;
        public string aug_description;
        public int max_level;
        public int current_level;
        public float[] value_per_level;
        public float[] value_per_level_2;
        public float[] value_per_level_3;
    }


    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    };

    static public T[] LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(
            string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.Default.GetString(data);
        //string jsonData = new UTF8Encoding(false).GetString(data); //BOM을 마킹하지 않게 false로 했다.

        return JsonHelper.FromJson<T>("{\"Items\":" + jsonData + "}");
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this.gameObject)
            Instance = null;
    }

    AugMentData[] augments = LoadJsonFile<AugMentData>
        ("C:/Users/D/Desktop/ThanksGivingM (2)/Assets/Data", "AugmentData"); //path 부분은 문제 해결하고 바꾸겠습니다!

    //이제 래핑해서 리턴까지 되는데 배열에 들어가지지 않는다. 2022.04.19
    //해결!
    public List<AugMentData> augMentsList;

    private void Start()
    {
        augMentsList = augments.ToList();
       

        //TextAsset text = Resources.Load<TextAsset>("AugmentData");
        //string path = Application.dataPath + "/AugmentData.json";
        //string json = File.ReadAllText(path);

        //AugMents augments = JsonUtility.FromJson<AugMents>(json);
        //text.text = $"{augments[0].max_level}";
        //Debug.Log(Encoding.UTF8.GetBytes(augments[0].aug_name)); // 한글 깨짐 현상. UTF8로 인코딩하면 파싱조차 되지 않는다.
        //Debug.Log(augments[0].aug_description);
        //Debug.Log($"{augments[0].max_level}");
        //Debug.Log(augments[0].value_per_level);

        //왜인지 파싱이 안됨
    }
   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(augMentsList[0].aug_name);
            Debug.Log(augMentsList[0].aug_description);
        }
    }


    //1. UTF8로 인코딩 하여 파싱하면 파싱에서 ArgumentException: JSON parse error: Invalid value. 오류가 뜬다.
    //2. 다른 형식으로 인코딩 하여 사용하면 파싱은 정상적으로 되나, 한글이 깨진다. (배열에 담기지 않던 이슈는 무분별한 시리얼라이즈 남발 때문이었다.)
    //3. 한글은 한 글자에 3Byte씩 사용하기 때문이라고 한다.
    //4. UTF8은 BOM관련 한글깨짐 이슈가 있다고 하지만 전부 10년 전 쯤 얘기인듯 하다..(관련해서 적용해 보았는데 해결되지 않음)
    //5. UTF8형식을 Wrapper가 못 받아들인다..(왜?..)
    //5-2 디버그 찍어보니까 UTF8로 인코딩한 데이터는 올바른 제이슨 구조가 아니라고 뜬다. (개인적으로 여기가 가장 의심스럽다)
    //BOM이 답이었다!!! 메모장으로 열어서 BOM 빠져있는 UTF8로 인코딩 해야만 한다!!!
}