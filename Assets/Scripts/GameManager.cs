using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private GameObject m_pauseScreen;


    private PlayerControls m_playerControls;

    public int m_currentWave;

    protected override void Awake()
    {
        Time.timeScale = 1.0f;
        m_playerControls = new PlayerControls();
        m_playerControls.PlayerMovement.Pause.Enable();
        m_playerControls.PlayerMovement.Pause.performed += OnPause;

        m_pauseScreen.SetActive(false);
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnPause(InputAction.CallbackContext _context)
    {
        Time.timeScale = 0;
        m_pauseScreen.SetActive(true);
    }
}
