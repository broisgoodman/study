using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Augment : MonoBehaviour
{
    public UnityAction unityAction;
    public UnityEvent[] _event = new UnityEvent[18];

    public Buff buff;
    public Button aug_Button1;
    public Button aug_Button2;
    public Button aug_Button3;

    //List<AugmentEvent> augmentEventList;
    //List<UnityEngine.Events.UnityAction> augmentEventList;
    public UnityAction []augmentEventList = new UnityAction[18];


    private void Start()
    {
        augmentEventList[0] = new UnityAction(VolumeUp);
        augmentEventList[1] = new UnityAction(UpTempo);
        augmentEventList[2] = new UnityAction(Brilliant);
        augmentEventList[3] = new UnityAction(Cresendo);
        augmentEventList[4] = new UnityAction(Delicato);
        augmentEventList[5] = new UnityAction(Feroce);
        augmentEventList[6] = new UnityAction(Relax);
        augmentEventList[7] = new UnityAction(Duet);
        augmentEventList[8] = new UnityAction(Improvisation);
        augmentEventList[9] = new UnityAction(Accent_recovery);
        augmentEventList[10] = new UnityAction(MoveStage);
        augmentEventList[11] = new UnityAction(Concentrate);
        augmentEventList[12] = new UnityAction(Fairy);
        augmentEventList[13] = new UnityAction(RapidPlay);
        augmentEventList[14] = new UnityAction(Accent_barrier);
        augmentEventList[15] = new UnityAction(WindSong);
        augmentEventList[16] = new UnityAction(TrollSong);
        augmentEventList[17] = new UnityAction(Fury);

    }
 
    public void SetButton(int button_num, int aug_index) //특성버튼에 기능을 연결해 주는 함수.
    {    
            switch (button_num)
            {
            case 0: //버튼 1                                
                        aug_Button1.onClick.AddListener(augmentEventList[aug_index]); //버튼1의 온클릭 이벤트에 해당 특성의 메소드를 담는다.                   
                break;
            case 1: //버튼 2               
                        aug_Button2.onClick.AddListener(augmentEventList[aug_index]);                                 
                break;
            case 2: //버튼 3
                        aug_Button3.onClick.AddListener(augmentEventList[aug_index]);                                     
                break;

            }    

    }

    //볼륨업
    public void VolumeUp() //연주 범위를 늘립니다.
    {
        switch (DataManager.Instance.augMentsList[0].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[0].current_level++;
                buff.volumeUpBuffRange = DataManager.Instance.augMentsList[0].value_per_level[0]; //레벨당 수치를 참조하여 범위를 늘림.
                break;

            case 1:
                DataManager.Instance.augMentsList[0].current_level++;
                buff.volumeUpBuffRange = DataManager.Instance.augMentsList[0].value_per_level[0];
                break;

                case 2:
                return;
               

            default:
                break;
        }
        GameManager.Instance.SelectAugDone(); //특성이 선택되었음을 알리고  
        buff.SetBuffRange();
        Debug.Log(DataManager.Instance.augMentsList[0].aug_name + "선택됨");
    }


    //업템포
    public void UpTempo() // 연주 속도를 상승시킵니다.
    {
        switch (DataManager.Instance.augMentsList[1].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[1].current_level++;
                buff.buffSpan = buff.defaultBuffSpan / DataManager.Instance.augMentsList[1].value_per_level[0];
                break;
             
            case 1:
                DataManager.Instance.augMentsList[1].current_level++;
                buff.buffSpan = buff.defaultBuffSpan / DataManager.Instance.augMentsList[1].value_per_level[1];
                break;

            case 2:
                DataManager.Instance.augMentsList[1].current_level++;
                buff.buffSpan = buff.defaultBuffSpan / DataManager.Instance.augMentsList[1].value_per_level[2];
                break;

            case 3:
                return;
                

            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[1].aug_name + "선택됨");

    }

    //명석함
    public void Brilliant() // 획득 경험치 증가
    {
        switch (DataManager.Instance.augMentsList[2].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[2].current_level++;
                GameManager.Instance.exp_weight_value = DataManager.Instance.augMentsList[2].value_per_level[0];
                break;

            case 1:
                DataManager.Instance.augMentsList[2].current_level++;
                GameManager.Instance.exp_weight_value = DataManager.Instance.augMentsList[2].value_per_level[1];
                break;

            case 2:
                DataManager.Instance.augMentsList[2].current_level++;
                GameManager.Instance.exp_weight_value = DataManager.Instance.augMentsList[2].value_per_level[2];
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[2].aug_name + "선택됨");

    }

    //크레센도
    public void Cresendo() //"20초마다 연주 회복량이 1% 늘어납니다(최대100%)" //미구현
    {
        //switch (DataManager.Instance.augMentsList[3].current_level)
        //{
        //    case 0:
        //        DataManager.Instance.augMentsList[3].current_level++;
               
        //        break;

        //    case 1:
        //        DataManager.Instance.augMentsList[3].current_level++;
               
        //        break;

        //    case 2:
        //        DataManager.Instance.augMentsList[3].current_level++;
                
        //        break;

        //    case 3:
        //        return;


        //    default:
        //        break;
        //}
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[3].aug_name + "선택됨");

    }


    //델리카토
    public void Delicato() // 연주 범위를 줄이고 회복량을 상승시킵니다.
    {
        switch (DataManager.Instance.augMentsList[4].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[4].current_level++;
                buff.delicatoBuffRange = DataManager.Instance.augMentsList[4].value_per_level[0]; //레벨당 수치를 참조하여 범위를 좁힘.
                buff.delicato_HealValue = DataManager.Instance.augMentsList[4].value_per_level_2[0]; //회복량 증가
                break;

            case 1:
                DataManager.Instance.augMentsList[4].current_level++;
                buff.delicatoBuffRange = DataManager.Instance.augMentsList[4].value_per_level[1]; //레벨당 수치를 참조하여 범위를 좁힘.
                buff.delicato_HealValue = DataManager.Instance.augMentsList[4].value_per_level_2[1]; //회복량 증가
                break;

            case 2:
                DataManager.Instance.augMentsList[4].current_level++;
                buff.delicatoBuffRange = DataManager.Instance.augMentsList[4].value_per_level[2]; //레벨당 수치를 참조하여 범위를 좁힘.

                buff.delicato_HealValue = DataManager.Instance.augMentsList[4].value_per_level_2[2]; //회복량 증가
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        buff.SetBuffRange();
        Debug.Log(DataManager.Instance.augMentsList[4].aug_name + "선택됨");

    }

    //페로체
    public void Feroce() // 회복량을 줄이고 연주 범위를 늘립니다.
    {
        switch (DataManager.Instance.augMentsList[5].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[5].current_level++;
                buff.feroceBuffRange = DataManager.Instance.augMentsList[5].value_per_level[0]; //레벨당 수치를 참조하여 범위를 조정
                buff.feroce_HealValue = DataManager.Instance.augMentsList[5].value_per_level_2[0]; //회복량 증가
                break;

            case 1:
                DataManager.Instance.augMentsList[5].current_level++;
                buff.feroceBuffRange = DataManager.Instance.augMentsList[5].value_per_level[1];
                buff.feroce_HealValue = DataManager.Instance.augMentsList[5].value_per_level_2[1]; //회복량 조정
                break;

            case 2:
                DataManager.Instance.augMentsList[5].current_level++;
                buff.feroceBuffRange = DataManager.Instance.augMentsList[5].value_per_level[2]; 
                buff.feroce_HealValue = DataManager.Instance.augMentsList[5].value_per_level_2[2]; 
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        buff.SetBuffRange();
        Debug.Log(DataManager.Instance.augMentsList[5].aug_name + "선택됨");

    }

    //릴렉스
    public void Relax() // 연주 범위 내 농부들의 스태미나 소모량이 감소합니다.
    {
        switch (DataManager.Instance.augMentsList[6].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[6].current_level++;
                buff.Relax_value = DataManager.Instance.augMentsList[6].value_per_level[0];            
                break;

            case 1:
                DataManager.Instance.augMentsList[6].current_level++;
                buff.Relax_value = DataManager.Instance.augMentsList[6].value_per_level[1];
                break;

            case 2:
                DataManager.Instance.augMentsList[6].current_level++;
                buff.Relax_value = DataManager.Instance.augMentsList[6].value_per_level[2];
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[6].aug_name + "선택됨");

    }

    //듀엣
    public void Duet() // 범위 내 농부가 1명이면 연주 회복량을 증가시키고 힘을 상승시킵니다.
    {
        DataManager.Instance.augMentsList[7].current_level++;

        buff.duet_HealValue = DataManager.Instance.augMentsList[7].value_per_level[0];
        buff.duet_ATKValue = DataManager.Instance.augMentsList[7].value_per_level_2[0];
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[7].aug_name + "선택됨");

    }

    //즉흥연주
    public void Improvisation() // 음유시인이 이동하지 않을 때 농부의 호응을 더 빨리 얻습니다.
    {
        DataManager.Instance.augMentsList[8].current_level++;
        buff.isImprovisation = true;
        buff.improvisation_value = DataManager.Instance.augMentsList[8].value_per_level[0];
                 
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[8].aug_name + "선택됨");

    }

    //악센트:회복
    public void Accent_recovery() // 5번 연주한 뒤 모든 농부의 스테미나를 조금 회복시킵니다.
    {
        if(!buff.isBuffCountOn) //카운트재기 시작
        buff.isBuffCountOn = true;

        switch (DataManager.Instance.augMentsList[9].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[9].current_level++;
                buff.accent_recovery_count = DataManager.Instance.augMentsList[9].value_per_level[0];
                buff.accent_recovery_value = DataManager.Instance.augMentsList[9].value_per_level_2[0];
                break;

            case 1:
                DataManager.Instance.augMentsList[9].current_level++;
                buff.accent_recovery_value = DataManager.Instance.augMentsList[9].value_per_level_2[1];
                break;

            case 2:
                DataManager.Instance.augMentsList[9].current_level++;
                buff.accent_recovery_value = DataManager.Instance.augMentsList[9].value_per_level_2[2];
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[9].aug_name + "선택됨");

    }

    //무대이동
    public void MoveStage() // 호응을 받으면 이동 속도가 잠시 상승합니다.
    {

       DataManager.Instance.augMentsList[10].current_level++;
       buff.isMoveStageOn = true;              
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[10].aug_name + "선택됨");

    }

    //집중연주
    public void Concentrate() // 획득하는 경험치가 상승합니다.
    {
        switch (DataManager.Instance.augMentsList[11].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[11].current_level++;
                buff.stopBuffValue = 1.5f;                
                break;

            case 1:
                DataManager.Instance.augMentsList[11].current_level++;
                buff.stopBuffValue = 2f;
                break;

            case 2:
                DataManager.Instance.augMentsList[11].current_level++;
                buff.stopBuffValue = 2.5f;
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[11].aug_name + "선택됨");

    }

    //꼬마요정
    public void Fairy() // 곁에서 연주하는 요정을 불러냅니다. 농부에게 붙여주면 농부를 회복시킵니다. //미구현
    {
        switch (DataManager.Instance.augMentsList[12].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[12].current_level++;
                buff.stopBuffValue = 1.5f;
                break;

            case 1:
                DataManager.Instance.augMentsList[12].current_level++;
                buff.stopBuffValue = 2f;
                break;

            case 2:
                DataManager.Instance.augMentsList[12].current_level++;
                buff.stopBuffValue = 2.5f;
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[12].aug_name + "선택됨");
    }

    //속주
    public void RapidPlay() // 체력이 30% 이하인 농부가 있을때 이동 속도가 상승합니다.
    {
       
        DataManager.Instance.augMentsList[13].current_level++;
        buff.isRapidPlayActivate = true;
        buff.rapidPlayValue = 1.5f;
               
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[13].aug_name + "선택됨");
    }

    //악센트:배리어
    public void Accent_barrier() //10번 연주한 뒤 범위 내 농부들에게 보호막을 씌워줍니다. 미구현
    {
        //switch (DataManager.Instance.augMentsList[11].current_level)
        //{
        //    case 0:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 1.5f;
        //        break;

        //    case 1:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2f;
        //        break;

        //    case 2:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2.5f;
        //        break;

        //    case 3:
        //        return;


        //    default:
        //        break;
        //}
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[14].aug_name + "선택됨");
    }

    //바람노래
    public void WindSong() //10번 연주한 뒤 범위 내 농부들을 회복시켜주는 장판을 설치합니다. 미구현
    {
        //switch (DataManager.Instance.augMentsList[11].current_level)
        //{
        //    case 0:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 1.5f;
        //        break;

        //    case 1:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2f;
        //        break;

        //    case 2:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2.5f;
        //        break;

        //    case 3:
        //        return;


        //    default:
        //        break;
        //}
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[15].aug_name + "선택됨");
    }

    //돌림노래
    public void TrollSong() //연주로 회복 효과를 받은 농부가 그 효과를 약하게 다시 발휘합니다.// 미구현
    {
        //switch (DataManager.Instance.augMentsList[11].current_level)
        //{
        //    case 0:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 1.5f;
        //        break;

        //    case 1:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2f;
        //        break;

        //    case 2:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2.5f;
        //        break;

        //    case 3:
        //        return;


        //    default:
        //        break;
        //}
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[16].aug_name + "선택됨");
    }

    //오기
    public void Fury() //농부가 탈진에서 회복하면 일정시간 수확속도가 상승합니다.// 미구현
    {
        //switch (DataManager.Instance.augMentsList[11].current_level)
        //{
        //    case 0:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 1.5f;
        //        break;

        //    case 1:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2f;
        //        break;

        //    case 2:
        //        DataManager.Instance.augMentsList[11].current_level++;
        //        buff.stopBuffValue = 2.5f;
        //        break;

        //    case 3:
        //        return;


        //    default:
        //        break;
        //}
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[17].aug_name + "선택됨");
    }
}
