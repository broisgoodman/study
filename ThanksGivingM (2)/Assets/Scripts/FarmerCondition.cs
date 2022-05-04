using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmerCondition : MonoBehaviour
{
    public Buff buff;
    public FarmerController farmerController;

    public delegate void events();

    public GameObject reactSpeechBallon;
    public GameObject reactSpeechBallonPrefab;


    public GameObject staminaGauge;
    public Image staminaGaugeImage;

    public GameObject healedFX;
    public GameObject reactFX;
    public GameObject speedFX;

    public GameObject healdText;
    public GameObject healdTextPrefab;

    public Animator speedAnimator;

    public string farmer_Name;      //농부 이름
    public string family;           //농부 소속 패밀리
    public int level;               //농부 레벨   
    public string faverite_Type;    //농부 선호 작물 타입;
    public float full_stamina;      //농부 최대 체력
    public float stamina;           //농부 현재 체력
    public float harvest_Speed;     //수확 속도
    public float atk;               //농부 공격력
    public float heal_To_exp_weight = 0.5f; //힐량을 경험치로 바꾸는 것에 대한 가중치.
    public float weight_To_DecreaseStamina = 1;

    #region 호응관련 변수들
    public float reactGauge; //호응 게이지
    public const float MAXREACTGAUGE = 100; //최대 호응 게이지
    private float reactDuration = 3; //호응 게이지 지속시간
    private float reactTimer = 0f;
    private float reactSpan = 1f;
    private float increaseReactGaugeValue = 10; //호응 게이지 상승량
    private float decreaseReactGaugeValue = 5; //호응 게이지 하락량
    public bool isIncreasedSpeed = false;
    #endregion

    private bool isInBuffZone; //버프 존 안에 들어와있는가?
    


    events HealdEvent;
    events exhaustEvent;
    void Start()
    {
        HealdEvent = () => healdText = Instantiate(healdTextPrefab, GameObject.Find("WorldCanvas").transform); //탈진관련 연출
        HealdEvent += () => healdText.name = $"{this.name} healdText";
        HealdEvent += () => healdText.GetComponent<HealdText>().farmerTransform = gameObject.transform;
        HealdEvent += () => healdText.GetComponent<Text>().text = $"+{healvalue.ToString("F1")}";

        exhaustEvent = () => reactSpeechBallon = Instantiate(reactSpeechBallonPrefab, GameObject.Find("WorldCanvas").transform); //탈진관련 연출
        exhaustEvent += () => reactSpeechBallon.name = $"{this.name} reactBalloon";
        exhaustEvent += () => reactSpeechBallon.GetComponent<SpeechBalloon>().farmerTransform = gameObject.transform;


        staminaGauge = Instantiate(staminaGauge, GameObject.Find("WorldCanvas").transform);
        staminaGauge.name = $"{this.name} staminaGauge";
        staminaGauge.GetComponent<StaminaGauge>().farmerTransform = gameObject.transform;
        staminaGaugeImage = staminaGauge.GetComponentInChildren<Image>();

    }

    // Update is called once per frame
    void Update()
    {      
        ReactTimer(); //호응 관련 메소드              
    }

    public float value;
    public float healvalue;

    public void DecreaseStamina(params float[] values) //스태미나 감소
    {
        for (int i = 0; i < values.Length; i++)
        {
            value += values[i];
        }

        stamina -= value / weight_To_DecreaseStamina;
        staminaGauge.GetComponent<StaminaGauge>().UpdateStamina();    

        value = 0; //변수 초기화 
    }

    public void IncreaseStamina(params float[] values) //스태미나 회복
    {
        for (int i = 0; i < values.Length; i++)
        {
            value += values[i];
        }

        stamina += value; //스태미나 회복
        staminaGauge.GetComponent<StaminaGauge>().UpdateStamina();

        value = 0; //변수 초기화 
        if(stamina > full_stamina)
            stamina = full_stamina;
    }

    public void FarmerLevelUp()
    {
        full_stamina += 10;
    }
    public void Healed(params float[] values) //힐 받기
    {
        for (int i = 0; i < values.Length; i++) 
        {
            healvalue += values[i];
        }

        

        stamina += healvalue; //스태미나 회복
        staminaGauge.GetComponent<StaminaGauge>().UpdateStamina();

        HealdEvent();

        GameManager.Instance.GainEXP(healvalue * heal_To_exp_weight); //힐량에 가중치 만큼 곱해서 경험치로 환산 후 게임매니저에게 넘겨줌.

        healvalue = 0; //변수 초기화

        healedFX.SetActive(true);
    }

    IEnumerator RewindReactBuff(float value)
    {
        yield return new WaitForSeconds(reactDuration);

        harvest_Speed /= value;
        isIncreasedSpeed = false;
        speedAnimator.SetBool("isIncreasedSpeed", isIncreasedSpeed);

    }
    private void ReactTimer()
    {
        reactTimer += Time.deltaTime;
        if (reactTimer > reactSpan) // 호응 타이머를 잰다.
        {
            if (isInBuffZone)
            {
                if(buff.isStop && buff.isImprovisation)
                reactGauge += increaseReactGaugeValue * buff.improvisation_value; //1초가 되었을 때 버프존에 있다면 증가.

                else
                    reactGauge += increaseReactGaugeValue; //1초가 되었을 때 버프존에 있다면 증가.
            }
            else
            {
                if(reactGauge > 0)
                reactGauge -= decreaseReactGaugeValue; //아니라면 감소.

            }

            reactTimer = 0;

            if (reactGauge > MAXREACTGAUGE) //호응 게이지가 꽉 차면
            {
                harvest_Speed *= 2f;
                speedFX.SetActive(true);
                isIncreasedSpeed = true;
                speedAnimator.SetBool("isIncreasedSpeed", isIncreasedSpeed);
                exhaustEvent();
                //reactFX.SetActive(true);
                StartCoroutine(RewindReactBuff(2f));
                StartCoroutine(buff.MoveStageReact()); //무대이동 관련 스크립트
                reactGauge = 0;
            }
            if (reactGauge < 0)
                reactGauge = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BuffZone")
        {
            isInBuffZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "BuffZone")
        {
            isInBuffZone = false;
        }
    }
}
