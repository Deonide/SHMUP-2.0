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


    //<---  Movement    --->
    private Rigidbody2D m_rb;

    //<-- Changeable variables -->
    private int m_Health = 3;
    public float m_Speed;


    private void Awake()
    {
        m_playerControls = new PlayerControls();
        m_playerControls.PlayerMovement.Fire.Enable();
        m_playerControls.PlayerMovement.SpecialFire.Enable();

        m_playerControls.PlayerMovement.Movement.Enable();

        m_playerControls.PlayerMovement.Fire.performed += OnFire;
        m_playerControls.PlayerMovement.SpecialFire.performed += OnSpecialFire;

        m_playerControls.PlayerMovement.Movement.performed += OnMove;

        m_playerControls.PlayerMovement.Movement.canceled += OnMoveStop;

    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
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

    private void OnSpecialFire(InputAction.CallbackContext _context)
    {
        Debug.Log("Special shot");
    }

    public void AddHealth()
    {
        m_Health = 3; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        m_Health -= 1;
        print(m_Health);
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

        if (m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
    
        
    }
}
