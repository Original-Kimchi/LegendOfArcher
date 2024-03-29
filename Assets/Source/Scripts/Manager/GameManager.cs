﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> EnemyList = new List<GameObject>();
    [SerializeField] private List<Transform> respawnPoint;
    [SerializeField] private Text enemy;
    [SerializeField] private Transform door;
    [SerializeField] private int addCnt;

    private void Start()
    {
        StartCoroutine(Manager());
    }

    private IEnumerator Manager()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.15f); // 안하면 EnemyList에 Transform 채워지기 전에 클리어 됨
        while (true)
        {
            enemy.text = EnemyList.Count.ToString();
            if(Player.Instance.hp <= 0)
            {
                GameOver();
            }
            else if(EnemyList.Count <= 0)
            {
                Debug.Log("GameManager| EnemyList Size: " + EnemyList.Count);
                if (addCnt <= 0)
                    GameClear();
                else
                    EnemySpawner.Spawn();
            }

            yield return wait;
        }
    }

    private void GameOver()
    {
        // Game Over, go to the Menu or show ScoreBoard
    }

    private void GameClear()
    {
        Debug.Log("GameClear");
        door.gameObject.SetActive(false);
        // Game Clear, go to the Next Stage
    }
}
