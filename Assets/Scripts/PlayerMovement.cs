using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject m_bulletPrefab, m_bulletPrefabPower, m_bulletPrefabUltra;

    [SerializeField]
    private GameObject m_bulletSpawnPoint;

    [SerializeField]
    public GameObject m_health1, m_health2, m_health3;

    [SerializeField]
    private GameObject m_pauseScreen;

    [SerializeField]
    private TextMeshProUGUI m_scoreText;

    private PlayerControls m_playerControls;
    //<---  Movement    --->
    private Rigidbody2D m_rb;

    //<-- Changeable variables -->
    public int m_Health = 3;
    public int m_bulletDamage;
    public bool m_bulletPowered, m_bulletUltra;
    public float m_Speed;
#endregion
    #region Start Game
    private void Awake()
    {
        m_playerControls = new PlayerControls();
        m_playerControls.PlayerMovement.Fire.Enable();
        m_playerControls.PlayerMovement.SpecialFire.Enable();
        m_playerControls.PlayerMovement.Movement.Enable();

        m_playerControls.PlayerMovement.Pause.Enable();
        m_playerControls.PlayerMovement.Pause.performed += OnPause;
        m_playerControls.PlayerMovement.Fire.performed += OnFire;
        m_playerControls.PlayerMovement.SpecialFire.performed += OnSpecialFire;

        m_playerControls.PlayerMovement.Movement.performed += OnMove;

        m_playerControls.PlayerMovement.Movement.canceled += OnMoveStop;

        m_pauseScreen.SetActive(false);
    }
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        m_health1.SetActive(true); 
        m_health2.SetActive(true);
        m_health3.SetActive(true);
    }
    #endregion
    #region Inputs
    private void OnMove(InputAction.CallbackContext _context)
    {
        if(m_rb != null)
        {
            Vector2 data = _context.ReadValue<Vector2>();
            m_rb.velocity = new Vector2(data.x * m_Speed, data.y);
        }
    }

    private void OnMoveStop(InputAction.CallbackContext context)
    {
        if(m_rb != null)
        {
            m_rb.velocity = Vector2.zero;
        }
    }

    private void OnFire(InputAction.CallbackContext _context)
    {
        if(m_bulletSpawnPoint != null)
        {

            if (m_bulletPowered == true)
            {
                //Schiet een kogel af
                Instantiate(m_bulletPrefabPower, m_bulletSpawnPoint.transform.position, m_bulletSpawnPoint.transform.rotation);
            }
            else if (m_bulletUltra == true)
            {
                //Schiet een kogel af
                Instantiate(m_bulletPrefabUltra, m_bulletSpawnPoint.transform.position, m_bulletSpawnPoint.transform.rotation);
            }
            else
            {
                //Schiet een kogel af
                Instantiate(m_bulletPrefab, m_bulletSpawnPoint.transform.position, m_bulletSpawnPoint.transform.rotation);
            }
        }
    }

    private void OnPause(InputAction.CallbackContext _context)
    {
        Time.timeScale = 0;
        m_pauseScreen.SetActive(true);
    }

    private void OnSpecialFire(InputAction.CallbackContext _context)
    {
        Debug.Log("Special shot fired");
    }
    #endregion
    #region Health & PowerUps
    public void AddHealth()
    {
        m_Health = 3; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_Health -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Haal de PickUp class op van het object waartegen het botst
        PowerUpBase powerUp = collision.GetComponent<PowerUpBase>();

        //Als deze bestaat.
        if (powerUp)
        {
            //voer de functie uit
            powerUp.Activate();
        }
    }
    #endregion

    void Update()
    {
        //Get the screen position of object in Pixels
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        
        // Get the right side of the screen in world units
        float rightSideofScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x;

        
        if(screenPos.x <= 0 && m_rb.velocity.x < 0)
        {
            transform.position = new Vector2(rightSideofScreenInWorld, transform.position.y);
        }

        else if (screenPos.x >= Screen.width && m_rb.velocity.x > 0)
        {
            transform.position = new Vector2(leftSideOfScreenInWorld, transform.position.y);
        }

        else if (m_Health == 2)
        {
            m_health3.SetActive(false);
        }

        else if (m_Health == 1)
        {
            m_health2.SetActive(false);
        }

        else if (m_Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Death Scene");
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        m_pauseScreen.SetActive(false);
    }

    public void AddScore(int score)
    {
        PlayerSettings.Instance.m_score += score;
        if (PlayerSettings.Instance.m_score < 1000)
        {
            m_scoreText.text = "0" + PlayerSettings.Instance.m_score.ToString();
        }
        else
        {
            m_scoreText.text = PlayerSettings.Instance.m_score.ToString();
        }
    }
}
