using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//high stage 전용
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    protected List<Transform> spawnPoints;
    [SerializeField]
    protected List<GameObject> prefabs;

    private void Update()
    {
        if(EnemyManager.enemies.Count <= 0)
        {
            Spawn();
        }
    }

    protected void Spawn()
    {
        GameObject enemy;
        for(int i = 0; i < spawnPoints.Count; i++)
        {
            enemy = Instantiate(prefabs[Random.Range(0, prefabs.Count)], spawnPoints[i]);
            EnemyManager.AddEnemy(enemy);
        }
    }
}
