using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private float m_destroyTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_destroyTimer -= Time.deltaTime;

        if (m_destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
