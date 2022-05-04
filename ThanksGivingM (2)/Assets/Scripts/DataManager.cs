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
        //string jsonData = new UTF8Encoding(false).GetString(data); //BOM�� ��ŷ���� �ʰ� false�� �ߴ�.

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
        ("C:/Users/D/Desktop/ThanksGivingM (2)/Assets/Data", "AugmentData"); //path �κ��� ���� �ذ��ϰ� �ٲٰڽ��ϴ�!

    //���� �����ؼ� ���ϱ��� �Ǵµ� �迭�� ������ �ʴ´�. 2022.04.19
    //�ذ�!
    public List<AugMentData> augMentsList;

    private void Start()
    {
        augMentsList = augments.ToList();
       

        //TextAsset text = Resources.Load<TextAsset>("AugmentData");
        //string path = Application.dataPath + "/AugmentData.json";
        //string json = File.ReadAllText(path);

        //AugMents augments = JsonUtility.FromJson<AugMents>(json);
        //text.text = $"{augments[0].max_level}";
        //Debug.Log(Encoding.UTF8.GetBytes(augments[0].aug_name)); // �ѱ� ���� ����. UTF8�� ���ڵ��ϸ� �Ľ����� ���� �ʴ´�.
        //Debug.Log(augments[0].aug_description);
        //Debug.Log($"{augments[0].max_level}");
        //Debug.Log(augments[0].value_per_level);

        //������ �Ľ��� �ȵ�
    }
   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(augMentsList[0].aug_name);
            Debug.Log(augMentsList[0].aug_description);
        }
    }


    //1. UTF8�� ���ڵ� �Ͽ� �Ľ��ϸ� �Ľ̿��� ArgumentException: JSON parse error: Invalid value. ������ ���.
    //2. �ٸ� �������� ���ڵ� �Ͽ� ����ϸ� �Ľ��� ���������� �ǳ�, �ѱ��� ������. (�迭�� ����� �ʴ� �̽��� ���к��� �ø�������� ���� �����̾���.)
    //3. �ѱ��� �� ���ڿ� 3Byte�� ����ϱ� �����̶�� �Ѵ�.
    //4. UTF8�� BOM���� �ѱ۱��� �̽��� �ִٰ� ������ ���� 10�� �� �� ����ε� �ϴ�..(�����ؼ� ������ ���Ҵµ� �ذ���� ����)
    //5. UTF8������ Wrapper�� �� �޾Ƶ��δ�..(��?..)
    //5-2 ����� ���ϱ� UTF8�� ���ڵ��� �����ʹ� �ùٸ� ���̽� ������ �ƴ϶�� ���. (���������� ���Ⱑ ���� �ǽɽ�����)
    //BOM�� ���̾���!!! �޸������� ��� BOM �����ִ� UTF8�� ���ڵ� �ؾ߸� �Ѵ�!!!
}