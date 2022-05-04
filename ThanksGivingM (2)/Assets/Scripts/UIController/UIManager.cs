using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance //싱글톤.
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Augment augment;
    public Text aug_1_name_text;
    public Text aug_1_description_text;
    public Text aug_2_name_text;
    public Text aug_2_description_text;
    public Text aug_3_name_text;
    public Text aug_3_description_text;

    public Buff buff; 
    public Image tensionGauge;
    public Image expGauge;
    public Text levelText;


    public GameObject gameOverPanel;
    public GameObject PausePanel;
    public GameObject SoundPanel;
    public Text gameOverTotalYeildText;


    //public void PrintGainExpText(float exp)
    //{
    //     Text go = Instantiate(expText, GameObject.Find("Canvas").transform);
    //    go.GetComponent<Text>().text = $"+ {exp}exp";
    //}

    public void UpdateTensionGauge()
    {
        StartCoroutine(TensionDecreaseAnimation());
    }
    public void UpdatePlayerLevel()
    {
        levelText.text = ($"{GameManager.Instance.player_Level}");
    }
    public void UpdateExpGauge()
    {
        //StartCoroutine(EXPIncreaseAnimation());

        float a = (GameManager.Instance.exp * 100) / GameManager.Instance.requiredexp;
        expGauge.fillAmount = a * 0.01f;
        //Debug.Log(a);
    }

    public void UpdateAugmentSelectData(int first, int second, int third)//특성선택창 정보 갱신
    {
        aug_1_name_text.text = $"{DataManager.Instance.augMentsList[first].aug_name}{DataManager.Instance.augMentsList[first].current_level+1}";
        aug_2_name_text.text = $"{DataManager.Instance.augMentsList[second].aug_name}{DataManager.Instance.augMentsList[second].current_level + 1}";
        aug_3_name_text.text = $"{DataManager.Instance.augMentsList[third].aug_name}{DataManager.Instance.augMentsList[third].current_level + 1}";

        aug_1_description_text.text = $"{DataManager.Instance.augMentsList[first].aug_description}";
        aug_2_description_text.text = $"{DataManager.Instance.augMentsList[second].aug_description}";
        aug_3_description_text.text = $"{DataManager.Instance.augMentsList[third].aug_description}";

        augment.SetButton(0, first);
        augment.SetButton(1, second);
        augment.SetButton(2, third);

        if (DataManager.Instance.augMentsList[first].current_level >= DataManager.Instance.augMentsList[first].max_level)
        {
            aug_1_description_text.text = "최대레벨입니다.";
            aug_1_name_text.text = $"{DataManager.Instance.augMentsList[first].aug_name} Max";

        }
        if (DataManager.Instance.augMentsList[second].current_level >= DataManager.Instance.augMentsList[second].max_level)
        {
            aug_2_description_text.text = "최대레벨입니다.";
            aug_2_name_text.text = $"{DataManager.Instance.augMentsList[second].aug_name} Max";

        }
        if (DataManager.Instance.augMentsList[third].current_level >= DataManager.Instance.augMentsList[third].max_level)
        {
            aug_3_description_text.text = "최대레벨입니다.";
            aug_3_name_text.text = $"{DataManager.Instance.augMentsList[third].aug_name} Max";

        } 
    }

    IEnumerator TensionDecreaseAnimation() //게이지가 줄어드는 것에 대한 애니메이션.
    {
        for (float f = tensionGauge.fillAmount; f > GameManager.Instance.tension * .01f; f -= .01f)
        {
            tensionGauge.fillAmount -= .01f;
            yield return new WaitForSeconds(.01f);
        }
    }

    public void GameOverUI()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameOverTotalYeildText.text = $"{GameManager.Instance.total_yield}";
    }
    public void PauseUI()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);      
    } 
    public void UnPausingPauseUI()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);      
    }
    public void SoundUI()
    {
        PausePanel.SetActive(false);
        SoundPanel.SetActive(true);      
    }
    public void OffSoundUI()
    {
        PausePanel.SetActive(true);
        SoundPanel.SetActive(false);
    }
    public void ExitButtonInMenu()
    {
        ScnenManager.Instance.LoadScene("Title");
    }

    //IEnumerator EXPIncreaseAnimation() //경험치가 늘어나는 것에 대한 애니메이션.
    //{ //수정해야함! 2022.03.17 15:24
    //    float a = (GameManager.Instance.exp * 100) / GameManager.Instance.requiredexp;

    //    for (float f = expGauge.fillAmount; f < a; f += .01f)
    //    {
    //        tensionGauge.fillAmount += .01f;
    //        yield return new WaitForSeconds(.01f);
    //    }
    //}
}
