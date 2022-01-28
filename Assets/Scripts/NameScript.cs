using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class NameScript : MonoBehaviour
{
    public static NameScript Instance;

    public TMP_InputField inputField;
    public TMP_Text currentBestScore;
    public string userName;
    public string bestPlayerName;
    public int bestScore;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadScore();
        if(bestScore > 0)
        {
            currentBestScore.text = "Best Score: " + bestPlayerName.ToString() + " " + bestScore.ToString();
        }

    }

    public void SetPlayerName()
    {
        userName = inputField.text;
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.bestPlayerName = bestPlayerName;

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

            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
        }
    }

}
