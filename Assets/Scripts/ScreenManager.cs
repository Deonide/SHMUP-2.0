using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_pauseScreen;

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        m_pauseScreen.SetActive(false);
    }
}
