using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{

    public TMP_Text textScore;
    public int actualPoints;
    public string playerName;
    public int newGamePoints = 0;
    public TMP_InputField userName;


    // Start is called before the first frame update
    void Start()
    {

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        MainManagerUI.Instance.LoadScore();
        //Debug.Log($"El nombre de usernameText es {MainManagerUI.Instance.usernameText}");
        actualPoints = MainManagerUI.Instance.bestScore;
        playerName = MainManagerUI.Instance.usernameText;
        textScore.text = $"Best Score: {playerName} {actualPoints}";
    }


    public void StartNew()
    {
        if (userName.text != "")
        {
            MainManagerUI.Instance.userNameTextActual = userName.text;
            Debug.Log($"El valor de usernameTextActual es: {MainManagerUI.Instance.userNameTextActual} {newGamePoints}");
            MainManagerUI.Instance.SaveScore();
            SceneManager.LoadScene("Game_Scene");
        }
        else
        {
            MainManagerUI.Instance.userNameTextActual = "Player_" + Random.Range(1, 99).ToString();
            MainManagerUI.Instance.SaveScore();
            SceneManager.LoadScene("Game_Scene");

            //Debug.Log("Introduce un nombre valido de usuario");
        }
        
    }

    /*public void Exit()
    {
        MainManagerUI.Instance.SaveScore();
        bool UNITY_EDITOR = true;

        if (UNITY_EDITOR)
        {
            EditorApplication.ExitPlaymode();
        }
        else
        {
            Application.Quit();
        }
    }*/
    public void Exit()
    {
        MainManagerUI.Instance.SaveScore();
        Application.Quit();
    }


}
