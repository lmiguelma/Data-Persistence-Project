using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text ScoreTextOne;
    public GameObject GameOverText;

    public GameObject newGameButton;
    public GameObject restartGameButton;
    //public GameObject rankingGameButton;

    public bool m_Started = false;
    public int m_Points;
    public int intialPoints = 0;
    public string playerData;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        VisibleButtons(false);
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        MainManagerUI.Instance.LoadScore();
        ScoreText.text = $"Score : {MainManagerUI.Instance.userNameTextActual} Points: {intialPoints}";
        ScoreTextOne.text = $"Best Score : {MainManagerUI.Instance.usernameText} Points: {MainManagerUI.Instance.bestScore}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {


            Debug.Log("Press Buttons for new or restart the game");

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }*/
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        playerData = MainManagerUI.Instance.userNameTextActual;
        ScoreText.text = $"Score : {playerData} Points: {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        VisibleButtons(true);
        if (m_Points > MainManagerUI.Instance.bestScore)
        {
            MainManagerUI.Instance.usernameText = playerData;
            MainManagerUI.Instance.bestScore = m_Points;
            MainManagerUI.Instance.SaveScore();
            //Debug.Log($"El nombre de PlayerData es {playerData} y el nombre guardado es {MainManagerUI.Instance.usernameText}");
            ScoreTextOne.text = $"Best Score: {MainManagerUI.Instance.usernameText} Points: {MainManagerUI.Instance.bestScore}";

        }
    }

    private void VisibleButtons(bool active)
    {
        newGameButton.SetActive(active);
        restartGameButton.gameObject.SetActive(active);
        //rankingGameButton.gameObject.SetActive(active);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Start_Scene");
    }
}
