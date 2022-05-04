using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType
    {
        Heal, //회복
        Speed //공속증가
    }
    //2022.04.19 최대치 이상으로 힐 x
    public PlayerController playerController;

    public BuffType bufftype;
    public List<GameObject> farmersInBuffZone; //버프 범위에 들어와있는 농부들을 담아 놓을 변수.

    public FarmerCondition[] allTheFarmers = new FarmerCondition[5]; //모든 농부

    public Transform buffRange;
    public Vector3 defaultBuffRange;
    public float volumeUpBuffRange=1;
    public float delicatoBuffRange=1;
    public float feroceBuffRange=1;


    public float stopBuffValue = 1; //집중연주 변수 나중에 삭제

    public float defaultBuffSpan = 1; //버프주기

    public float buffSpan = 1; //버프주기
    private float buffTimer;

    
    public float cresendo_HealValue = 1;  //현재 힐량.
    public float default_HealValue = 1;  //기본 힐량.
    public float delicato_HealValue = 1;  //기본 힐량.
    public float feroce_HealValue = 1;  //기본 힐량.

    public float Relax_value = 1;

    public float improvisation_value = 1;//즉흥연주 가중치
    public bool isImprovisation = false;

    public bool isStop;
    public float duet_HealValue = 1; //듀엣 힐량
    public float duet_ATKValue = 1; //듀엣 힐량

    public float accent_recovery_count = 0;
    public float accent_recovery_value = 0;

    public int buffCount = 0;
    public bool isBuffCountOn = false;
    public bool isMoveStageOn = false; //무대이동이 활성화 되었는가?
    
    public bool isRapidPlayOn = false; //속주의 조건이 만족 되었는가?(30퍼 이하인 농부가 있는가?)
    public bool isRapidPlayActivate = false;//속주 특성이 활성화 되었는가?
    public float rapidPlayValue = 0f;//속주 특성 값

    //버프 주기, 버프량에 각 특성의 수치를 넣어줘야 할듯 하다.

    public Animator buffAnimator;

 
    void Start()
    {
        defaultBuffRange = buffRange.localScale;
        bufftype = BuffType.Heal;
    
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.moveVec == Vector2.zero) isStop = true;
        else isStop = false;

        


        BuffTimer(); //주기 마다 버프를 방출(?)


        if (isRapidPlayActivate)
        {
           RapidPlay();
        }
       
    }
    private void RapidPlay()
    {
        if (allTheFarmers[0].stamina <= allTheFarmers[0].full_stamina * .3f || allTheFarmers[1].stamina <= allTheFarmers[1].full_stamina * .3f ||
          allTheFarmers[2].stamina <= allTheFarmers[2].full_stamina * .3f || allTheFarmers[3].stamina <= allTheFarmers[3].full_stamina * .3f || allTheFarmers[4].stamina <= allTheFarmers[4].full_stamina * .3f)
        {
            isRapidPlayOn = true;
        }

        else isRapidPlayOn = false;
    }
  


    #region 농부 감지

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Farmer")
        {              
            farmersInBuffZone.Add(collision.gameObject);
            collision.GetComponent<FarmerCondition>().weight_To_DecreaseStamina = 1 / Relax_value;
               Debug.Log($"{farmersInBuffZone.Count}명의 농부가 범위 안에 들어옴.");
        }       

        if(farmersInBuffZone.Count == 1) //범위내 농부가 한 명이면 힘을 증가시킨다.
        {
            farmersInBuffZone[0].GetComponent<FarmerCondition>().atk *= duet_ATKValue;
        }
        else
        {
            for (int i = 0; i < farmersInBuffZone.Count; i++) //범위내 농부가 한 명이 아니면 힘을 초기값으로 맞춘다..
            {
                farmersInBuffZone[0].GetComponent<FarmerCondition>().atk /= duet_ATKValue;               
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Farmer")
        {
            farmersInBuffZone.Remove(collision.gameObject);
            collision.GetComponent<FarmerCondition>().weight_To_DecreaseStamina = 1f;

            Debug.Log($"농부가 범위 에서 빠짐. 총 {farmersInBuffZone.Count}명의 농부가 범위에 남음.");
        }

        if (farmersInBuffZone.Count == 1)
        {
            farmersInBuffZone[0].GetComponent<FarmerCondition>().atk *= duet_ATKValue;
        }
        else
        {
            
                collision.GetComponent<FarmerCondition>().atk /= duet_ATKValue;
            
        }
    }

    #endregion


    #region 버프들

    private void BuffTimer()
    {
        buffTimer += Time.deltaTime; //타이머를 잰다.

        if (buffTimer > buffSpan) //버프 주기를 넘어서면 버프 메소드 실행.
        {
            switch (isStop)
            {
                case true: //스탑
                    Debug.Log("스탑버프 들어옴");
                    HealBuffStop();
                    break;

                case false: //힐 버프
                    Debug.Log("이동버프 들어옴");
                    HealBuff();
                    break;
              
            }

            buffTimer = 0;         
            buffAnimator.SetTrigger("Buffing");


            if (isBuffCountOn)
            {
                buffCount++;

                if (buffCount % accent_recovery_count == 0)//5번 연주 후에
                {
                    for (int i = 0; i < allTheFarmers.Length; i++)
                    {                     
                        allTheFarmers[i].GetComponent<FarmerCondition>().Healed(accent_recovery_value);
                        //모든 농부들에게  스태미나 회복.
                    }
                    
                }
                if (buffCount % 10 == 0)//10번 연주 후에
                {

                }
            }
            
        }
    }



    private void HealBuff()
    {

        if (farmersInBuffZone == null) return; //범위안에 농부가 없으면 리턴.

        if(farmersInBuffZone.Count == 1)
        {
            farmersInBuffZone[0].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue * duet_HealValue);
        }

        else
            for (int i = 0; i < farmersInBuffZone.Count; i++)
            {              
                farmersInBuffZone[i].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue);
                //범위안의 농부들에게 기본힐량만큼 스태미나 회복.
            }

    }

    private void HealBuffStop()
    {

        if (farmersInBuffZone == null) return; //범위안에 농부가 없으면 리턴.

        if (farmersInBuffZone.Count == 1)
        {
            farmersInBuffZone[0].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue * stopBuffValue * duet_HealValue);
        }

        else
            for (int i = 0; i < farmersInBuffZone.Count; i++)
        {
            farmersInBuffZone[i].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue * stopBuffValue);
            //범위안의 농부들에게 기본힐량만큼 스태미나 회복.
        }

    }
    #endregion

    public void SetBuffRange()
    {
        buffRange.localScale = defaultBuffRange / delicatoBuffRange * volumeUpBuffRange * feroceBuffRange;
    }


    public IEnumerator  MoveStageReact() //무대이동 특성을 찍었을 때의 호응 메소드
    {
        StopCoroutine(MoveStageReact());
        playerController.moveStageSpeed = 1f;
        playerController.moveStageSpeed *= DataManager.Instance.augMentsList[10].value_per_level_2[0];
        playerController.SetSpeed();
        yield return new WaitForSeconds(DataManager.Instance.augMentsList[10].value_per_level[0]);
        playerController.moveStageSpeed /= DataManager.Instance.augMentsList[10].value_per_level_2[0];
        playerController.SetSpeed();

    }
}
