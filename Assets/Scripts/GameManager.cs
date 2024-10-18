using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : Singleton<GameManager>
{
    public int m_currentWave;

    protected override void Awake()
    {
        Time.timeScale = 1.0f;

        base.Awake();
    }
}
