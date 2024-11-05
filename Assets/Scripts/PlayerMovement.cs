using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Bullets")]
    [SerializeField]
    private GameObject m_bulletSpawnPoint;

    [SerializeField]
    private GameObject m_bulletPrefab, m_bulletPrefabPower, m_bulletPrefabUltra;

    public int m_bulletDamage;
    public bool m_bulletPowered, m_bulletUltra;

    [Header("Health")]

    [SerializeField]
    private GameObject m_health1;
    [SerializeField]
    private GameObject m_health2, m_health3;
    [SerializeField]
    private int m_Health = 3;

    [Header("Shields")]
    [SerializeField]
    private GameObject m_shield;
    [SerializeField]
    private GameObject m_shield2, m_shield3;
    [SerializeField] 
    private int m_shieldsAmount;

    private bool m_shielded;

    [Header("Special Attack")]
    public GameObject m_Special;
    public GameObject m_Special2, m_Special3, m_Special4;
    public int m_specialAttackCount;

    [Header("Other")]
    [SerializeField]
    private GameObject m_pauseScreen;

    [SerializeField]
    private TextMeshProUGUI m_scoreText, m_moneyText;

    [SerializeField] 
    private FuelBar m_fuelBar;

    private PlayerControls m_playerControls;
    private WaveManager m_waveManager;

    //<---  Movement    --->
    private Rigidbody2D m_rb;

    //<-- Changeable variables -->
    public float m_fuel;
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
        GameManager.Instance.LoadSaveData();
    }
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_waveManager = FindObjectOfType<WaveManager>();
        GetMoneyText();
        GameManager.Instance.MaxFuel();
        m_fuel = GameManager.Instance.m_maxFuel;
        m_fuelBar.SetMaxFuel(GameManager.Instance.m_maxFuel);
        GameManager.Instance.m_currentGroup = 0;
        GameManager.Instance.m_score = 0;


        m_health1.SetActive(true); 
        m_health2.SetActive(true);
        m_health3.SetActive(true);

        //Shields gaan pas aan met de power up
        m_shield.SetActive(false);
        m_shield2.SetActive(false);
        m_shield3.SetActive(false);

        //Zelfde voor de special attack
        m_Special.SetActive(false); 
        m_Special2.SetActive(false);
        m_Special3.SetActive(false);
        m_Special4.SetActive(false);
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
        if(m_specialAttackCount >= 4)
        {
            if(m_waveManager.m_spawnedBoss.Count == 0)
            {
                foreach (GameObject enemy in m_waveManager.m_spawnedEnemies)
                {
                    Destroy(enemy);
                }
            }
        }
    }

    #endregion
    #region Health & PowerUps
    public void AddHealth()
    {
        if (GameManager.Instance.m_healthMax == 1)
        {
            m_Health = 3;
        }
        else
        {
            m_Health += 1;
        }
    }

    public void AddShield()
    {
        m_shielded = true;
        if (GameManager.Instance.m_shieldPower == 1)
        {
            m_shieldsAmount = 2;
        }

        else if (GameManager.Instance.m_shieldPower == 2)
        {
            m_shieldsAmount = 3;
        }

        else
        {
            m_shieldsAmount += 1;
        }
    }

    public void AddSpecial()
    {
        if(GameManager.Instance.m_specialPower == 1)
        {
            m_specialAttackCount += 2;
        }

        else if (GameManager.Instance.m_specialPower == 2)
        {
            m_specialAttackCount += 3;
        }
        else
        {
            m_specialAttackCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!m_shielded)
            {
                m_Health -= 1;
                StartCoroutine(HealthFlash());
            }
            else
            {
                ShieldDown();
            }
        }
    }

    private IEnumerator HealthFlash()
    {
        for( int i = 0; i < 10 ; i++ )
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().enabled = enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void ShieldDown()
    {
        if (m_shielded)
        {
            m_shieldsAmount--;
            if(m_shieldsAmount <= 0)
            {
                m_shielded = false;
            }
        }
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

        if (m_bulletPowered != true)
        {
            m_bulletDamage = 1;
        }
        else if (m_bulletPowered == true && !m_bulletUltra)
        {
            m_bulletDamage = 2;
        }
        else if (m_bulletUltra == true)
        {
            m_bulletDamage = 3;
        }

        #region Screenwrapper
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
        #endregion
        #region Fuel
        m_fuel -= Time.deltaTime;
        m_fuelBar.SetFuel(m_fuel);
        if (m_fuel > 100)
        {
            m_fuel = 100;
        }

        else if (m_fuel <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Death Scene");
        }
        #endregion
        #region Health and Shield
        if (m_shieldsAmount == 2)
        {
            m_shield3.SetActive(false);
        }

        else if (m_shieldsAmount == 1)
        {
            m_shield2.SetActive(false);
        }

        else if (m_shieldsAmount == 0)
        {
            m_shield.SetActive(false);
        }


        if (m_Health == 2)
        {
            m_health3.SetActive(false);
        }

        else if (m_Health == 1)
        {
            m_health2.SetActive(false);
        }

        else if (m_Health <= 0)
        {
            GameManager.Instance.SaveGame();
            Destroy(gameObject);
            SceneManager.LoadScene("Death Scene");
        }
        #endregion
        #region Special

        if(m_specialAttackCount > 4)
        {
            m_specialAttackCount = 4;
        }


        if (m_specialAttackCount > 0)
        {
            m_Special.SetActive(true);
        }

        else if (m_specialAttackCount == 2)
        {
            m_Special2.SetActive(true);
        }

        else if (m_specialAttackCount == 3)
        {
            m_Special3.SetActive(true);
        }

        else if (m_specialAttackCount == 4)
        {
            m_Special4.SetActive(true);
        }
        #endregion
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        m_pauseScreen.SetActive(false);
    }

    public void AddScore(int score)
    {
        GameManager.Instance.m_score += score;
        if (GameManager.Instance.m_score < 1000)
        {
            m_scoreText.text = "Score: 0" + GameManager.Instance.m_score.ToString();
        }
        else
        {
            m_scoreText.text = "Score: " + GameManager.Instance.m_score.ToString();
        }
    }

    public void GetMoney(int money)
    {
        GameManager.Instance.m_money += money;

        GetMoneyText();
    }

    public void GetMoneyText()
    {

        if (GameManager.Instance.m_money < 1000)
        {
            m_moneyText.text = "Money: " + GameManager.Instance.m_money.ToString();
        }
        else
        {
            m_moneyText.text = "Money: " + GameManager.Instance.m_money.ToString();
        }
    }
}
