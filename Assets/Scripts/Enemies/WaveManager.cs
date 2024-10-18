using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private float m_spawnrate = 1f;

    [SerializeField]
    private GameObject[] m_enemyPrefabs;

    [SerializeField]
    private GameObject m_boss;

    [SerializeField]
    private TextMeshProUGUI m_waveCounter;

    [SerializeField]
    private bool m_canSpawn = true;

    public List<GameObject> m_spawnedEnemies = new List<GameObject>();

    private int m_spawnedEnemiesLoop;
    private float m_amountOfEnemies = 3;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(m_spawnrate);
        while (true)
        {
            yield return (wait);
            if (m_spawnedEnemies.Count == 0)
            {
                GameManager.Instance.m_currentWave++;
                if(GameManager.Instance.m_currentWave % 4 == 0)
                {
                    BossSpawner();
                }

                else
                {
                    WaveCounter();

                    m_spawnedEnemiesLoop = 0;
                    m_amountOfEnemies = 3 + Mathf.Round(GameManager.Instance.m_currentWave / 8);
                    if(m_amountOfEnemies > 7)
                    {
                        m_amountOfEnemies = 7;
                    }

                    Spawner();
                }
            }
        }
    }

    private void Spawner()
    {
        for (m_spawnedEnemiesLoop = 0; m_spawnedEnemiesLoop < m_amountOfEnemies; m_spawnedEnemiesLoop++)
        {
            int randomSpawn = Random.Range(0, m_enemyPrefabs.Length);
            GameObject enemyToSpawn = m_enemyPrefabs[randomSpawn];
            GameObject enemySpawned = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            m_spawnedEnemies.Add(enemySpawned);
        }
    }

    private void BossSpawner()
    {
       GameObject bossSpawned = Instantiate(m_boss, transform.position, Quaternion.identity);
        m_spawnedEnemies.Add(bossSpawned);
    }

    public void RemoveFromList(GameObject enemy)
    {
        m_spawnedEnemies.Remove(enemy);
    }

    public void WaveCounter()
    {
        m_waveCounter.text = "Wave : " + GameManager.Instance.m_currentWave;
    }

    #region NotInfiniteSpawner
    /*    [SerializeField] 
        private float m_countdownTimer;

        [SerializeField] 
        private GameObject m_spawnPoint;

        [SerializeField]
        private float waveCountDown;

        public int m_groupIndex = 0;

        private bool m_readyToCountDown;
        private bool m_readyToSpawnWave;

        public wave[] waves;

        void Start()
        {
            m_readyToSpawnWave = true;
            m_readyToCountDown = true;

            for (int i = 0; i < waves.Length; i++)
            {
                for (int j = 0; j < waves[i].groups.Length; j++)
                {
                    waves[i].groups[j].enemiesleft = waves[i].groups[j].enemies.Length;
                }
            }
        }

        void Update()
        {
            if (GameManager.Instance.m_currentWave >= waves.Length)
            {
                return;
            }

            //Als er geen enemies meer over zijn begint de timer met aftellen.
            if (waves[GameManager.Instance.m_currentWave].groups[m_groupIndex].enemiesleft == 0)
            {
                m_readyToCountDown = true;
                m_groupIndex++;
            }

            //Timer die aftelt
            if (m_readyToCountDown == true)
            {
                m_countdownTimer -= Time.deltaTime;
            }

            //Als de timer gelijk is of kleiner dan 0 spawn de wave in. en zorgt ervoor dat de volgende wave niet gelijk komt.
            if (m_countdownTimer <= 0)
            {
                m_readyToCountDown = false;

                m_countdownTimer = waves[GameManager.Instance.m_currentWave].timeToNextEnemy;
                StartCoroutine(SpawnWave());
            }

            //Als de groupIndex gelijk is aan de currentwave.group.length dan word de groupIndex op 0 gezet en begint de volgende wave.
            if (m_groupIndex == waves[GameManager.Instance.m_currentWave].groups.Length)
            {
                m_readyToSpawnWave = true;
                m_groupIndex = 0;
                GameManager.Instance.m_currentWave++;
            }

            if (m_readyToSpawnWave == true)
            {
                waveCountDown -= Time.deltaTime;
            }

            if (waveCountDown <= 0)
            {
                m_readyToSpawnWave = false;

                waveCountDown = waves[GameManager.Instance.m_currentWave].timeToNextEnemy;
            }

        }

        IEnumerator SpawnWave()
        {
            if (GameManager.Instance.m_currentWave < waves.Length)
            {
                for (int i = 0; i < waves[GameManager.Instance.m_currentWave].groups[m_groupIndex].enemies.Length; i++)
                {
                    EnemyBase enemies = Instantiate(waves[GameManager.Instance.m_currentWave].groups[m_groupIndex].enemies[i], m_spawnPoint.transform.position, Quaternion.identity);

                    yield return new WaitForSeconds(waves[GameManager.Instance.m_currentWave].timeToNextEnemy);
                }
            }
        }*/

    /*    [System.Serializable]
        public class wave
        {
            public Groups[] groups;
            public float timeToNextEnemy;
            public float timeToNextWave;
        }

        [System.Serializable]
        public class Groups
        {
            public EnemyBase[] enemies;

            public int enemiesleft;
        }*/
    #endregion
}
