using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;
    public float stage_Level;
    public int player_Level; //�÷��̾� ���� ����
    private const int MAX_PLAYER_LEVEL = 30; //�÷��̾� �ִ� ����


    public float default_exp_weight_value = 1; //�⺻ ����ġ ����ġ
    public float exp_weight_value = 1; //����ġ ����ġ

    public float tension; //�ټǰ�����
    public float maxTension = 100; //�ټǰ����� �ִ�ġ


    #region ����ġ ���� ����
    public float exp { get; private set;} //���� ����ġ. ���ӸŴ��� ���� ��ġ�� ������ �� �ִ�.
    public float requiredexp { get; private set;} //�ʿ� ����ġ. ���ӸŴ��� ���� ��ġ�� ������ �� �ִ�.
    private float additionalRequiredExp = 30; //���� ��¿� ���� �߰� �ʿ����ġ ��.
    private const float defaultRequiredExp = 50; //�ʿ� ����ġ �⺻ ��.
    #endregion

    public List<FarmerCondition> farmers = new List<FarmerCondition>();

    public Crop crop;
    public Augment augment;
    
    public FieldMaker FM;

    public GameObject augSelect;
    public GameObject camera1;
    public Vector3 targetPos;

    public Text current_harvest;
    public float total_yield;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;
        isGameOver = false;
        player_Level = 1;
        tension = maxTension;
        stage_Level = 1.0f;
        FM = GameObject.Find("FieldMaker").GetComponent<FieldMaker>();
        SetRequiredEXP();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W)) //�׽�Ʈ
        {
            Time.timeScale += 5;
        }
        if (Input.GetKeyDown(KeyCode.Q)) //�׽�Ʈ
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.E)) //�׽�Ʈ
        {
            LevelUp();
        }
        if (tension <= 0)
        {
            GameOver();
        }


        if (exp > requiredexp)
        {
            LevelUp();
        }

        if (FM.isCamera)
        {
            if (camera1.transform.position == targetPos)
            {
                FM.isCamera = false;
            }
            camera1.transform.position = Vector3.MoveTowards(camera1.transform.position, targetPos, 0.05f);
        }
        else
        {
            targetPos = camera1.transform.position + new Vector3(2f, 0, 0);
        }

        current_harvest.text = total_yield.ToString();

    }

    public void SetRequiredEXP()
    {
        requiredexp = (player_Level * additionalRequiredExp) + defaultRequiredExp; //(���� * �߰� �ʿ����ġ) + �⺻ �ʿ����ġ
        Debug.Log($"{player_Level}������ �ʿ� ����ġ�� {requiredexp}�Դϴ�.");
    }

    public void GainEXP(float gaind_Exp) //����ġ ���� �޼ҵ�
    {
        if (player_Level >= MAX_PLAYER_LEVEL) //�ִ뷹���̸� ����
            return;

        exp += gaind_Exp * exp_weight_value;
        UIManager.instance.UpdateExpGauge();
        //UIManager.instance.PrintGainExpText(gaind_Exp * exp_weight_value);
        Debug.Log($"������� ���� ����ġ��{exp}�Դϴ�.");

    }

    public void GameOver()
    {
        isGameOver = true;
        UIManager.instance.GameOverUI();
    }


    public void LevelUp()
    {
        if (player_Level >= MAX_PLAYER_LEVEL) //�ִ뷹���̸� ����
            return;

        player_Level++;
        exp = 0;
        //Ư������Ʈ++;
        for(int i = 0; i < farmers.Count; i++)
        {
            farmers[i].FarmerLevelUp();
        }

        SetRequiredEXP();
        UIManager.instance.UpdatePlayerLevel();
        UIManager.instance.UpdateExpGauge();
        ActiveAugSelect();



        var (first, second, third) = ChooseAugments();

        UIManager.instance.UpdateAugmentSelectData(first, second, third);

    }

    static (int First, int Second, int Third) ChooseAugments()
    {
        int first;
        int second;
        int third;

        List<int> list = new List<int>();

        first = Random.Range(0, DataManager.Instance.augMentsList.Count);
        for (int i = 0; i < DataManager.Instance.augMentsList.Count;)
        {
            if (DataManager.Instance.augMentsList[first].max_level <= DataManager.Instance.augMentsList[first].current_level)//�ִ뷹���̸� �ٽ� �̱�
            {                                                           //Ư���� ���� ������ �ȶ�
                first = Random.Range(0, DataManager.Instance.augMentsList.Count);
            }
            else
            {
                list.Add(first);
                i++;
                break;
            }
        }

        second = Random.Range(0, DataManager.Instance.augMentsList.Count);
        for (int i = 0; i < DataManager.Instance.augMentsList.Count;)
        {
            if (list.Contains(second) || DataManager.Instance.augMentsList[second].max_level <= DataManager.Instance.augMentsList[second].current_level)
            {
                second = Random.Range(0, DataManager.Instance.augMentsList.Count);
            }
            else
            {
                list.Add(second);
                i++;
                break;

            }
        }

        third = Random.Range(0, DataManager.Instance.augMentsList.Count);
        for (int i = 0; i < DataManager.Instance.augMentsList.Count;)
        {
            if (list.Contains(third) || DataManager.Instance.augMentsList[third].max_level <= DataManager.Instance.augMentsList[third].current_level)
            {
                third = Random.Range(0, DataManager.Instance.augMentsList.Count);
            }
            else
            {
                list.Add(third);
                i++;
                break;

            }
        }

        var First = first;
        var Second = second;
        var Third = third;

        return (First, Second, Third);
    }

    //private int ChooseAugment()
    //{
    //    int first;
    //    int second;
    //    int third;

    //    List<int> list = new List<int>();

    //    first = Random.Range(0, DataManager.Instance.augMentsList.Count);
    //    for(int i = 0; i < DataManager.Instance.augMentsList.Count;)
    //    {
    //        if (DataManager.Instance.augMentsList[first].max_level <= DataManager.Instance.augMentsList[first].current_level)//�ִ뷹���̸� �ٽ� �̱�
    //        {                                                           //Ư���� ���� ������ �ȶ�
    //            first = Random.Range(0, DataManager.Instance.augMentsList.Count);
    //        }
    //        else
    //        {
    //            list.Add(first);
    //            i++;
    //            break;
    //        }
    //    }

    //    second = Random.Range(0, DataManager.Instance.augMentsList.Count);
    //    for (int i = 0; i < DataManager.Instance.augMentsList.Count;)
    //    {
    //        if (list.Contains(second) || DataManager.Instance.augMentsList[second].max_level <= DataManager.Instance.augMentsList[second].current_level)
    //        {
    //            second = Random.Range(0, DataManager.Instance.augMentsList.Count);
    //        }
    //        else
    //        {
    //            list.Add(second);
    //            i++;
    //            break;

    //        }
    //    }

    //    third = Random.Range(0, DataManager.Instance.augMentsList.Count);
    //    for (int i = 0; i < DataManager.Instance.augMentsList.Count;)
    //    {
    //        if (list.Contains(third) || DataManager.Instance.augMentsList[third].max_level <= DataManager.Instance.augMentsList[third].current_level)
    //        {
    //            third = Random.Range(0, DataManager.Instance.augMentsList.Count);
    //        }
    //        else
    //        {
    //            list.Add(third);
    //            i++;
    //            break;

    //        }
    //    }

    //    return first; 
       

    //    Debug.Log(first);
    //    Debug.Log(second);
    //    Debug.Log(third);

    //}

    private void ActiveAugSelect()
    {
        Time.timeScale = 0;
        augSelect.SetActive(true);
    }
    public void SelectAugDone()
    {
        Time.timeScale = 1;
        augment.aug_Button1.onClick.RemoveAllListeners();//Ư�� ��ư�鿡 ����ִ� OnClick�̺�Ʈ���� �ʱ�ȭ.
        augment.aug_Button2.onClick.RemoveAllListeners();
        augment.aug_Button3.onClick.RemoveAllListeners();
        augSelect.SetActive(false);

    }
}
