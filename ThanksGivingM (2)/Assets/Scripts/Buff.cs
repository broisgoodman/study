using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType
    {
        Heal, //ȸ��
        Speed //��������
    }
    //2022.04.19 �ִ�ġ �̻����� �� x
    public PlayerController playerController;

    public BuffType bufftype;
    public List<GameObject> farmersInBuffZone; //���� ������ �����ִ� ��ε��� ��� ���� ����.

    public FarmerCondition[] allTheFarmers = new FarmerCondition[5]; //��� ���

    public Transform buffRange;
    public Vector3 defaultBuffRange;
    public float volumeUpBuffRange=1;
    public float delicatoBuffRange=1;
    public float feroceBuffRange=1;


    public float stopBuffValue = 1; //���߿��� ���� ���߿� ����

    public float defaultBuffSpan = 1; //�����ֱ�

    public float buffSpan = 1; //�����ֱ�
    private float buffTimer;

    
    public float cresendo_HealValue = 1;  //���� ����.
    public float default_HealValue = 1;  //�⺻ ����.
    public float delicato_HealValue = 1;  //�⺻ ����.
    public float feroce_HealValue = 1;  //�⺻ ����.

    public float Relax_value = 1;

    public float improvisation_value = 1;//���￬�� ����ġ
    public bool isImprovisation = false;

    public bool isStop;
    public float duet_HealValue = 1; //�࿧ ����
    public float duet_ATKValue = 1; //�࿧ ����

    public float accent_recovery_count = 0;
    public float accent_recovery_value = 0;

    public int buffCount = 0;
    public bool isBuffCountOn = false;
    public bool isMoveStageOn = false; //�����̵��� Ȱ��ȭ �Ǿ��°�?
    
    public bool isRapidPlayOn = false; //������ ������ ���� �Ǿ��°�?(30�� ������ ��ΰ� �ִ°�?)
    public bool isRapidPlayActivate = false;//���� Ư���� Ȱ��ȭ �Ǿ��°�?
    public float rapidPlayValue = 0f;//���� Ư�� ��

    //���� �ֱ�, �������� �� Ư���� ��ġ�� �־���� �ҵ� �ϴ�.

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

        


        BuffTimer(); //�ֱ� ���� ������ ����(?)


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
  


    #region ��� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Farmer")
        {              
            farmersInBuffZone.Add(collision.gameObject);
            collision.GetComponent<FarmerCondition>().weight_To_DecreaseStamina = 1 / Relax_value;
               Debug.Log($"{farmersInBuffZone.Count}���� ��ΰ� ���� �ȿ� ����.");
        }       

        if(farmersInBuffZone.Count == 1) //������ ��ΰ� �� ���̸� ���� ������Ų��.
        {
            farmersInBuffZone[0].GetComponent<FarmerCondition>().atk *= duet_ATKValue;
        }
        else
        {
            for (int i = 0; i < farmersInBuffZone.Count; i++) //������ ��ΰ� �� ���� �ƴϸ� ���� �ʱⰪ���� �����..
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

            Debug.Log($"��ΰ� ���� ���� ����. �� {farmersInBuffZone.Count}���� ��ΰ� ������ ����.");
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


    #region ������

    private void BuffTimer()
    {
        buffTimer += Time.deltaTime; //Ÿ�̸Ӹ� ���.

        if (buffTimer > buffSpan) //���� �ֱ⸦ �Ѿ�� ���� �޼ҵ� ����.
        {
            switch (isStop)
            {
                case true: //��ž
                    Debug.Log("��ž���� ����");
                    HealBuffStop();
                    break;

                case false: //�� ����
                    Debug.Log("�̵����� ����");
                    HealBuff();
                    break;
              
            }

            buffTimer = 0;         
            buffAnimator.SetTrigger("Buffing");


            if (isBuffCountOn)
            {
                buffCount++;

                if (buffCount % accent_recovery_count == 0)//5�� ���� �Ŀ�
                {
                    for (int i = 0; i < allTheFarmers.Length; i++)
                    {                     
                        allTheFarmers[i].GetComponent<FarmerCondition>().Healed(accent_recovery_value);
                        //��� ��ε鿡��  ���¹̳� ȸ��.
                    }
                    
                }
                if (buffCount % 10 == 0)//10�� ���� �Ŀ�
                {

                }
            }
            
        }
    }



    private void HealBuff()
    {

        if (farmersInBuffZone == null) return; //�����ȿ� ��ΰ� ������ ����.

        if(farmersInBuffZone.Count == 1)
        {
            farmersInBuffZone[0].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue * duet_HealValue);
        }

        else
            for (int i = 0; i < farmersInBuffZone.Count; i++)
            {              
                farmersInBuffZone[i].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue);
                //�������� ��ε鿡�� �⺻������ŭ ���¹̳� ȸ��.
            }

    }

    private void HealBuffStop()
    {

        if (farmersInBuffZone == null) return; //�����ȿ� ��ΰ� ������ ����.

        if (farmersInBuffZone.Count == 1)
        {
            farmersInBuffZone[0].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue * stopBuffValue * duet_HealValue);
        }

        else
            for (int i = 0; i < farmersInBuffZone.Count; i++)
        {
            farmersInBuffZone[i].GetComponent<FarmerCondition>().Healed(default_HealValue * cresendo_HealValue * delicato_HealValue / feroce_HealValue * stopBuffValue);
            //�������� ��ε鿡�� �⺻������ŭ ���¹̳� ȸ��.
        }

    }
    #endregion

    public void SetBuffRange()
    {
        buffRange.localScale = defaultBuffRange / delicatoBuffRange * volumeUpBuffRange * feroceBuffRange;
    }


    public IEnumerator  MoveStageReact() //�����̵� Ư���� ����� ���� ȣ�� �޼ҵ�
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
