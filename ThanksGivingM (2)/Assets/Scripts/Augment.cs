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
 
    public void SetButton(int button_num, int aug_index) //Ư����ư�� ����� ������ �ִ� �Լ�.
    {    
            switch (button_num)
            {
            case 0: //��ư 1                                
                        aug_Button1.onClick.AddListener(augmentEventList[aug_index]); //��ư1�� ��Ŭ�� �̺�Ʈ�� �ش� Ư���� �޼ҵ带 ��´�.                   
                break;
            case 1: //��ư 2               
                        aug_Button2.onClick.AddListener(augmentEventList[aug_index]);                                 
                break;
            case 2: //��ư 3
                        aug_Button3.onClick.AddListener(augmentEventList[aug_index]);                                     
                break;

            }    

    }

    //������
    public void VolumeUp() //���� ������ �ø��ϴ�.
    {
        switch (DataManager.Instance.augMentsList[0].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[0].current_level++;
                buff.volumeUpBuffRange = DataManager.Instance.augMentsList[0].value_per_level[0]; //������ ��ġ�� �����Ͽ� ������ �ø�.
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
        GameManager.Instance.SelectAugDone(); //Ư���� ���õǾ����� �˸���  
        buff.SetBuffRange();
        Debug.Log(DataManager.Instance.augMentsList[0].aug_name + "���õ�");
    }


    //������
    public void UpTempo() // ���� �ӵ��� ��½�ŵ�ϴ�.
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
        Debug.Log(DataManager.Instance.augMentsList[1].aug_name + "���õ�");

    }

    //����
    public void Brilliant() // ȹ�� ����ġ ����
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
        Debug.Log(DataManager.Instance.augMentsList[2].aug_name + "���õ�");

    }

    //ũ������
    public void Cresendo() //"20�ʸ��� ���� ȸ������ 1% �þ�ϴ�(�ִ�100%)" //�̱���
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
        Debug.Log(DataManager.Instance.augMentsList[3].aug_name + "���õ�");

    }


    //����ī��
    public void Delicato() // ���� ������ ���̰� ȸ������ ��½�ŵ�ϴ�.
    {
        switch (DataManager.Instance.augMentsList[4].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[4].current_level++;
                buff.delicatoBuffRange = DataManager.Instance.augMentsList[4].value_per_level[0]; //������ ��ġ�� �����Ͽ� ������ ����.
                buff.delicato_HealValue = DataManager.Instance.augMentsList[4].value_per_level_2[0]; //ȸ���� ����
                break;

            case 1:
                DataManager.Instance.augMentsList[4].current_level++;
                buff.delicatoBuffRange = DataManager.Instance.augMentsList[4].value_per_level[1]; //������ ��ġ�� �����Ͽ� ������ ����.
                buff.delicato_HealValue = DataManager.Instance.augMentsList[4].value_per_level_2[1]; //ȸ���� ����
                break;

            case 2:
                DataManager.Instance.augMentsList[4].current_level++;
                buff.delicatoBuffRange = DataManager.Instance.augMentsList[4].value_per_level[2]; //������ ��ġ�� �����Ͽ� ������ ����.

                buff.delicato_HealValue = DataManager.Instance.augMentsList[4].value_per_level_2[2]; //ȸ���� ����
                break;

            case 3:
                return;


            default:
                break;
        }
        GameManager.Instance.SelectAugDone();
        buff.SetBuffRange();
        Debug.Log(DataManager.Instance.augMentsList[4].aug_name + "���õ�");

    }

    //���ü
    public void Feroce() // ȸ������ ���̰� ���� ������ �ø��ϴ�.
    {
        switch (DataManager.Instance.augMentsList[5].current_level)
        {
            case 0:
                DataManager.Instance.augMentsList[5].current_level++;
                buff.feroceBuffRange = DataManager.Instance.augMentsList[5].value_per_level[0]; //������ ��ġ�� �����Ͽ� ������ ����
                buff.feroce_HealValue = DataManager.Instance.augMentsList[5].value_per_level_2[0]; //ȸ���� ����
                break;

            case 1:
                DataManager.Instance.augMentsList[5].current_level++;
                buff.feroceBuffRange = DataManager.Instance.augMentsList[5].value_per_level[1];
                buff.feroce_HealValue = DataManager.Instance.augMentsList[5].value_per_level_2[1]; //ȸ���� ����
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
        Debug.Log(DataManager.Instance.augMentsList[5].aug_name + "���õ�");

    }

    //������
    public void Relax() // ���� ���� �� ��ε��� ���¹̳� �Ҹ��� �����մϴ�.
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
        Debug.Log(DataManager.Instance.augMentsList[6].aug_name + "���õ�");

    }

    //�࿧
    public void Duet() // ���� �� ��ΰ� 1���̸� ���� ȸ������ ������Ű�� ���� ��½�ŵ�ϴ�.
    {
        DataManager.Instance.augMentsList[7].current_level++;

        buff.duet_HealValue = DataManager.Instance.augMentsList[7].value_per_level[0];
        buff.duet_ATKValue = DataManager.Instance.augMentsList[7].value_per_level_2[0];
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[7].aug_name + "���õ�");

    }

    //���￬��
    public void Improvisation() // ���������� �̵����� ���� �� ����� ȣ���� �� ���� ����ϴ�.
    {
        DataManager.Instance.augMentsList[8].current_level++;
        buff.isImprovisation = true;
        buff.improvisation_value = DataManager.Instance.augMentsList[8].value_per_level[0];
                 
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[8].aug_name + "���õ�");

    }

    //�Ǽ�Ʈ:ȸ��
    public void Accent_recovery() // 5�� ������ �� ��� ����� ���׹̳��� ���� ȸ����ŵ�ϴ�.
    {
        if(!buff.isBuffCountOn) //ī��Ʈ��� ����
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
        Debug.Log(DataManager.Instance.augMentsList[9].aug_name + "���õ�");

    }

    //�����̵�
    public void MoveStage() // ȣ���� ������ �̵� �ӵ��� ��� ����մϴ�.
    {

       DataManager.Instance.augMentsList[10].current_level++;
       buff.isMoveStageOn = true;              
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[10].aug_name + "���õ�");

    }

    //���߿���
    public void Concentrate() // ȹ���ϴ� ����ġ�� ����մϴ�.
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
        Debug.Log(DataManager.Instance.augMentsList[11].aug_name + "���õ�");

    }

    //��������
    public void Fairy() // �翡�� �����ϴ� ������ �ҷ����ϴ�. ��ο��� �ٿ��ָ� ��θ� ȸ����ŵ�ϴ�. //�̱���
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
        Debug.Log(DataManager.Instance.augMentsList[12].aug_name + "���õ�");
    }

    //����
    public void RapidPlay() // ü���� 30% ������ ��ΰ� ������ �̵� �ӵ��� ����մϴ�.
    {
       
        DataManager.Instance.augMentsList[13].current_level++;
        buff.isRapidPlayActivate = true;
        buff.rapidPlayValue = 1.5f;
               
        GameManager.Instance.SelectAugDone();
        Debug.Log(DataManager.Instance.augMentsList[13].aug_name + "���õ�");
    }

    //�Ǽ�Ʈ:�踮��
    public void Accent_barrier() //10�� ������ �� ���� �� ��ε鿡�� ��ȣ���� �����ݴϴ�. �̱���
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
        Debug.Log(DataManager.Instance.augMentsList[14].aug_name + "���õ�");
    }

    //�ٶ��뷡
    public void WindSong() //10�� ������ �� ���� �� ��ε��� ȸ�������ִ� ������ ��ġ�մϴ�. �̱���
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
        Debug.Log(DataManager.Instance.augMentsList[15].aug_name + "���õ�");
    }

    //�����뷡
    public void TrollSong() //���ַ� ȸ�� ȿ���� ���� ��ΰ� �� ȿ���� ���ϰ� �ٽ� �����մϴ�.// �̱���
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
        Debug.Log(DataManager.Instance.augMentsList[16].aug_name + "���õ�");
    }

    //����
    public void Fury() //��ΰ� Ż������ ȸ���ϸ� �����ð� ��Ȯ�ӵ��� ����մϴ�.// �̱���
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
        Debug.Log(DataManager.Instance.augMentsList[17].aug_name + "���õ�");
    }
}
