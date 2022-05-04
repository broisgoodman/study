using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScnenManager : MonoBehaviour
{
    public static ScnenManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this.gameObject)
            Instance = null;
    }
    
    public void LoadScene(string scene)
    {
        switch (scene)
        {
            case "InGame":
                    SceneManager.LoadScene("GameScene");
                break;
            case "Title":
                SceneManager.LoadScene("Title");
                break;
            default:
                break;

        }
    }


}
