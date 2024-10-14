using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_bulletPrefab;

    [SerializeField]
    private GameObject m_bulletSpawnPoint;

    private PlayerControls m_playerControls;

    [SerializeField]
    private GameObject m_health1, m_health2, m_health3;

    //<---  Movement    --->
    private Rigidbody2D m_rb;

    //<-- Changeable variables -->
    private int m_Health = 3;
    public float m_Speed;

    #region Awake
    private void Awake()
    {
        m_playerControls = new PlayerControls();
        m_playerControls.PlayerMovement.Fire.Enable();
        m_playerControls.PlayerMovement.SpecialFire.Enable();
        m_playerControls.PlayerMovement.Movement.Enable();

        m_playerControls.PlayerMovement.SpecialFire.started += StartSpecialFire;
        m_playerControls.PlayerMovement.Fire.performed += OnFire;
        m_playerControls.PlayerMovement.SpecialFire.performed += OnSpecialFire;

        m_playerControls.PlayerMovement.Movement.performed += OnMove;

        m_playerControls.PlayerMovement.Movement.canceled += OnMoveStop;
        m_playerControls.PlayerMovement.SpecialFire.canceled += CancelSpecialFire;
    }
    #endregion
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_health1.SetActive(true); 
        m_health2.SetActive(true);
        m_health3.SetActive(true);
    }

    private void OnMove(InputAction.CallbackContext _context)
    {
        Vector2 data = _context.ReadValue<Vector2>();
        m_rb.velocity = new Vector2(data.x * m_Speed, data.y);
    }

    private void OnMoveStop(InputAction.CallbackContext context)
    {
        m_rb.velocity = Vector2.zero;
    }

    private void OnFire(InputAction.CallbackContext _context)
    {
        //Schiet een kogel af
        Instantiate(m_bulletPrefab, m_bulletSpawnPoint.transform.position, m_bulletSpawnPoint.transform.rotation);
    }

    private void StartSpecialFire(InputAction.CallbackContext _context)
    {
        Debug.Log("Special shot start");
    }

    private void OnSpecialFire(InputAction.CallbackContext _context)
    {
        Debug.Log("Special shot fired");
    }

    private void CancelSpecialFire(InputAction.CallbackContext _context)
    {
        Debug.Log("Special shot canceled");
    }

    public void AddHealth()
    {
        m_Health = 3; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_Health -= 1;

    }

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
            Destroy(this.gameObject);
        }
    
        
    }
}
