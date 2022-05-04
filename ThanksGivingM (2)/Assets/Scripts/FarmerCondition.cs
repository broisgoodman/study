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

    public string farmer_Name;      //��� �̸�
    public string family;           //��� �Ҽ� �йи�
    public int level;               //��� ����   
    public string faverite_Type;    //��� ��ȣ �۹� Ÿ��;
    public float full_stamina;      //��� �ִ� ü��
    public float stamina;           //��� ���� ü��
    public float harvest_Speed;     //��Ȯ �ӵ�
    public float atk;               //��� ���ݷ�
    public float heal_To_exp_weight = 0.5f; //������ ����ġ�� �ٲٴ� �Ϳ� ���� ����ġ.
    public float weight_To_DecreaseStamina = 1;

    #region ȣ������ ������
    public float reactGauge; //ȣ�� ������
    public const float MAXREACTGAUGE = 100; //�ִ� ȣ�� ������
    private float reactDuration = 3; //ȣ�� ������ ���ӽð�
    private float reactTimer = 0f;
    private float reactSpan = 1f;
    private float increaseReactGaugeValue = 10; //ȣ�� ������ ��·�
    private float decreaseReactGaugeValue = 5; //ȣ�� ������ �϶���
    public bool isIncreasedSpeed = false;
    #endregion

    private bool isInBuffZone; //���� �� �ȿ� �����ִ°�?
    


    events HealdEvent;
    events exhaustEvent;
    void Start()
    {
        HealdEvent = () => healdText = Instantiate(healdTextPrefab, GameObject.Find("WorldCanvas").transform); //Ż������ ����
        HealdEvent += () => healdText.name = $"{this.name} healdText";
        HealdEvent += () => healdText.GetComponent<HealdText>().farmerTransform = gameObject.transform;
        HealdEvent += () => healdText.GetComponent<Text>().text = $"+{healvalue.ToString("F1")}";

        exhaustEvent = () => reactSpeechBallon = Instantiate(reactSpeechBallonPrefab, GameObject.Find("WorldCanvas").transform); //Ż������ ����
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
        ReactTimer(); //ȣ�� ���� �޼ҵ�              
    }

    public float value;
    public float healvalue;

    public void DecreaseStamina(params float[] values) //���¹̳� ����
    {
        for (int i = 0; i < values.Length; i++)
        {
            value += values[i];
        }

        stamina -= value / weight_To_DecreaseStamina;
        staminaGauge.GetComponent<StaminaGauge>().UpdateStamina();    

        value = 0; //���� �ʱ�ȭ 
    }

    public void IncreaseStamina(params float[] values) //���¹̳� ȸ��
    {
        for (int i = 0; i < values.Length; i++)
        {
            value += values[i];
        }

        stamina += value; //���¹̳� ȸ��
        staminaGauge.GetComponent<StaminaGauge>().UpdateStamina();

        value = 0; //���� �ʱ�ȭ 
        if(stamina > full_stamina)
            stamina = full_stamina;
    }

    public void FarmerLevelUp()
    {
        full_stamina += 10;
    }
    public void Healed(params float[] values) //�� �ޱ�
    {
        for (int i = 0; i < values.Length; i++) 
        {
            healvalue += values[i];
        }

        

        stamina += healvalue; //���¹̳� ȸ��
        staminaGauge.GetComponent<StaminaGauge>().UpdateStamina();

        HealdEvent();

        GameManager.Instance.GainEXP(healvalue * heal_To_exp_weight); //������ ����ġ ��ŭ ���ؼ� ����ġ�� ȯ�� �� ���ӸŴ������� �Ѱ���.

        healvalue = 0; //���� �ʱ�ȭ

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
        if (reactTimer > reactSpan) // ȣ�� Ÿ�̸Ӹ� ���.
        {
            if (isInBuffZone)
            {
                if(buff.isStop && buff.isImprovisation)
                reactGauge += increaseReactGaugeValue * buff.improvisation_value; //1�ʰ� �Ǿ��� �� �������� �ִٸ� ����.

                else
                    reactGauge += increaseReactGaugeValue; //1�ʰ� �Ǿ��� �� �������� �ִٸ� ����.
            }
            else
            {
                if(reactGauge > 0)
                reactGauge -= decreaseReactGaugeValue; //�ƴ϶�� ����.

            }

            reactTimer = 0;

            if (reactGauge > MAXREACTGAUGE) //ȣ�� �������� �� ����
            {
                harvest_Speed *= 2f;
                speedFX.SetActive(true);
                isIncreasedSpeed = true;
                speedAnimator.SetBool("isIncreasedSpeed", isIncreasedSpeed);
                exhaustEvent();
                //reactFX.SetActive(true);
                StartCoroutine(RewindReactBuff(2f));
                StartCoroutine(buff.MoveStageReact()); //�����̵� ���� ��ũ��Ʈ
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
