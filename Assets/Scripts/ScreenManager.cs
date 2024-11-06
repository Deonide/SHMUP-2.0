using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro m_TextMeshPro;

    public void GoToGameScene()
    {
        if (GameManager.Instance.m_IGN == null)
        {
            GameManager.Instance.m_IGN = "Player";
        }

        SceneManager.LoadScene("Main");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
