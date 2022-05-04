using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;
    public float stage_Level;
    public int player_Level; //플레이어 현재 레벨
    private const int MAX_PLAYER_LEVEL = 30; //플레이어 최대 레벨


    public float default_exp_weight_value = 1; //기본 경험치 가중치
    public float exp_weight_value = 1; //경험치 가중치

    public float tension; //텐션게이지
    public float maxTension = 100; //텐션게이지 최대치


    #region 경험치 관련 변수
    public float exp { get; private set;} //현재 경험치. 게임매니저 만이 수치를 조정할 수 있다.
    public float requiredexp { get; private set;} //필요 경험치. 게임매니저 만이 수치를 조정할 수 있다.
    private float additionalRequiredExp = 30; //레벨 상승에 따른 추가 필요경험치 값.
    private const float defaultRequiredExp = 50; //필요 경험치 기본 값.
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

        if (Input.GetKeyDown(KeyCode.W)) //테스트
        {
            Time.timeScale += 5;
        }
        if (Input.GetKeyDown(KeyCode.Q)) //테스트
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.E)) //테스트
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
        requiredexp = (player_Level * additionalRequiredExp) + defaultRequiredExp; //(레벨 * 추가 필요경험치) + 기본 필요경험치
        Debug.Log($"{player_Level}레벨의 필요 경험치는 {requiredexp}입니다.");
    }

    public void GainEXP(float gaind_Exp) //경험치 증가 메소드
    {
        if (player_Level >= MAX_PLAYER_LEVEL) //최대레벨이면 리턴
            return;

        exp += gaind_Exp * exp_weight_value;
        UIManager.instance.UpdateExpGauge();
        //UIManager.instance.PrintGainExpText(gaind_Exp * exp_weight_value);
        Debug.Log($"현재까지 얻은 경험치는{exp}입니다.");

    }

    public void GameOver()
    {
        isGameOver = true;
        UIManager.instance.GameOverUI();
    }


    public void LevelUp()
    {
        if (player_Level >= MAX_PLAYER_LEVEL) //최대레벨이면 리턴
            return;

        player_Level++;
        exp = 0;
        //특성포인트++;
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
            if (DataManager.Instance.augMentsList[first].max_level <= DataManager.Instance.augMentsList[first].current_level)//최대레벨이면 다시 뽑기
            {                                                           //특성의 현재 레벨이 안뜸
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
    //        if (DataManager.Instance.augMentsList[first].max_level <= DataManager.Instance.augMentsList[first].current_level)//최대레벨이면 다시 뽑기
    //        {                                                           //특성의 현재 레벨이 안뜸
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
        augment.aug_Button1.onClick.RemoveAllListeners();//특성 버튼들에 들어있는 OnClick이벤트들을 초기화.
        augment.aug_Button2.onClick.RemoveAllListeners();
        augment.aug_Button3.onClick.RemoveAllListeners();
        augSelect.SetActive(false);

    }
}
