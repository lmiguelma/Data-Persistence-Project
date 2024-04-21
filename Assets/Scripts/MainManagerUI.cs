using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class MainManagerUI : MonoBehaviour
{

    public static MainManagerUI Instance;

    public int bestScore;
    public string usernameText;
    public string userNameTextActual;


    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string usernameText;
        public string userNameTextActual;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.usernameText = usernameText;
        data.userNameTextActual = userNameTextActual;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile3.json", json);
        
        

    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile3.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.bestScore;
            usernameText = data.usernameText;
            userNameTextActual = data.userNameTextActual;
        }
    }
}
