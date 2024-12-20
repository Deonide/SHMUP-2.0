using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_LeaderBoard;
    Canvas m_Canvas;
    List<string> m_NamesList = new List<string>();
    List<int> m_ScoreList = new List<int>();

    private int m_placement = 375;

    private void Start()
    {
        m_Canvas = GetComponent<Canvas>();
        LoadData();
        UpdateScore();
    }

    private void UpdateScore()
    {

        for (int i = m_NamesList.Count - 1; i >= 0; i--)
        {
            if (string.IsNullOrEmpty(m_NamesList[i]))
            {
                m_NamesList.RemoveAt(i);
                m_ScoreList.RemoveAt(i);
            }
        }

        if (!m_NamesList.Contains(GameManager.Instance.m_IGN))
        {
            if (!string.IsNullOrEmpty(GameManager.Instance.m_IGN))
            {
                m_NamesList.Add(GameManager.Instance.m_IGN);
                m_ScoreList.Add(GameManager.Instance.m_score);
                CheckForRanking();
            }
        }

        else
        {
            int _currentName = m_NamesList.IndexOf(GameManager.Instance.m_IGN);
            if (GameManager.Instance.m_score > m_ScoreList[_currentName])
            {
                m_ScoreList[_currentName] = GameManager.Instance.m_score;
                CheckForRanking();
            }
        }

        foreach (Transform child in m_Canvas.transform)
        {
            Destroy(child.gameObject);
        }

        int displayCount = Mathf.Min(10, m_NamesList.Count);
        for (int i = 0; i < displayCount; i++)
        {
            int _rank = i + 1;
            TextMeshProUGUI _newText = Instantiate(m_LeaderBoard, m_Canvas.transform);
            RectTransform _rectTransform = _newText.GetComponent<RectTransform>();
            if (_rank > 5)
            {
                _rectTransform.anchoredPosition = new Vector2(425, 220 - i * 75 + m_placement);
                _newText.text = "#" + _rank + " - " + m_ScoreList[i] + ": " + m_NamesList[i];
            }
            else
            {
                _rectTransform.anchoredPosition = new Vector2(-290, 220 - i * 75);
                _newText.text = "#" + _rank + " - " + m_ScoreList[i] + ": " + m_NamesList[i];
            }
        }

        //Voor wanneer de speler niet in de top 10 eindigt.
        int playerRank = m_NamesList.IndexOf(GameManager.Instance.m_IGN) + 1;
        if (playerRank > 10)
        {
            TextMeshProUGUI playerText = Instantiate(m_LeaderBoard, m_Canvas.transform);
            RectTransform playerRectTransform = playerText.GetComponent<RectTransform>();
            playerRectTransform.anchoredPosition = new Vector2(110, -175);
            playerText.text = "#" + playerRank + " - " + GameManager.Instance.m_score + ": " + GameManager.Instance.m_IGN;
        }

        SaveData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/leaderboard.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);

            m_NamesList = data.NamesList;
            m_ScoreList = data.ScoreList;

            for (int i = m_NamesList.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(m_NamesList[i]))
                {
                    m_NamesList.RemoveAt(i);
                    m_ScoreList.RemoveAt(i);
                }
            }
        }
    }

    private void CheckForRanking()
    {
        List<(string name, int score)> combinedList = new List<(string, int)>();

        for (int i = 0; i < m_NamesList.Count; i++)
        {
            combinedList.Add((m_NamesList[i], m_ScoreList[i]));
        }

        combinedList.Sort((a, b) => b.score.CompareTo(a.score));

        m_NamesList.Clear();
        m_ScoreList.Clear();

        foreach (var item in combinedList)
        {
            m_NamesList.Add(item.name);
            m_ScoreList.Add(item.score);
        }
    }

    public void SaveData()
    {
        LeaderboardData data = new LeaderboardData();
        data.NamesList = m_NamesList;
        data.ScoreList = m_ScoreList;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/leaderboard.json", json);
    }
}

[System.Serializable]
public class LeaderboardData
{
    public List<string> NamesList;
    public List<int> ScoreList;
}
