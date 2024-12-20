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
    private TextMeshProUGUI m_groupCounter;

    [SerializeField]
    private bool m_canSpawn = true;

    public List<GameObject> m_spawnedEnemies = new List<GameObject>();
    public List<GameObject> m_spawnedBoss = new List<GameObject>();

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
            if (m_spawnedEnemies.Count == 0 && m_spawnedBoss.Count == 0)
            {
                GameManager.Instance.m_currentGroup++;
                if(GameManager.Instance.m_currentGroup % 4 == 0)
                {
                    GroupCounter();
                    BossSpawner();
                }

                else
                {
                    GroupCounter();
                    m_spawnedEnemiesLoop = 0;
                    m_amountOfEnemies = 3 + Mathf.Round(GameManager.Instance.m_currentWave / 16);
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
        m_spawnedBoss.Add(bossSpawned);
    }

    public void RemoveFromList(GameObject enemy)
    {
        m_spawnedEnemies.Remove(enemy);
    }

    public void RemoveBossFromList(GameObject enemy)
    {
        m_spawnedBoss.Remove(enemy);
        GameManager.Instance.m_currentGroup = 0;
        GameManager.Instance.m_currentWave++;
        WaveCounter();
        GroupCounter();
    }

    public void WaveCounter()
    {
        m_waveCounter.text = "Wave : " + GameManager.Instance.m_currentWave;
    }

    public void GroupCounter()
    {
        m_groupCounter.text = "Group : " + GameManager.Instance.m_currentGroup;
    }
}
