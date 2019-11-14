﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    public static readonly List<GameObject> enemies = new List<GameObject>();
    public static Transform player;

    public static void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public static void RemoveEnemy(GameObject enemy)
    {
        // Debug.Log("Removed " + enemy.name + "from List");
        
        enemies.Remove(enemy);
    }

    public static void SohwanEnemy(GameObject enemy,Transform transform)
    {
        GameObject.Instantiate(enemy, transform);
        enemies.Add(enemy);
    }
}
