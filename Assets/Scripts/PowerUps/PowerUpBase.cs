using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    protected PlayerMovement m_player;


    void Start()
    {
        m_player = FindObjectOfType<PlayerMovement>();
    }


    public virtual void Activate()
    {
        Destroy(gameObject);
    }
}
