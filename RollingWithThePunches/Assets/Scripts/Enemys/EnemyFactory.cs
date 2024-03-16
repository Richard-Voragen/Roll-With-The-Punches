using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private List<EnemySpawn> enemies;

    private void Start()
    {
        foreach(EnemySpawn enemy in enemies)
        {
            enemy.canSpawn = true;
            enemy.currentTime = 0.0f;
        }
    }

    private void Update()
    {
        foreach(EnemySpawn enemy in enemies)
        {
            if (enemy.canSpawn) continue;
            enemy.currentTime += Time.deltaTime;
            if (enemy.currentTime > enemy.spawnRate)
            {
                enemy.currentTime = 0.0f;
                enemy.canSpawn = true;
            }
        }
    }

    private void Spawn(EnemySpawn enemy)
    {
        if (!enemy.canSpawn){
            return;
        }

        GameObject currentEnemy;
        if (enemy.positionIsOffset)
        {
            currentEnemy = Instantiate(enemy.enemyPrefab, (Vector2)this.transform.position + enemy.spawnPosition, Quaternion.identity);
        }
        else 
        {
            currentEnemy = Instantiate(enemy.enemyPrefab, enemy.spawnPosition, Quaternion.identity);
        }
        currentEnemy.GetComponent<IEnemyController>().SetUpProcess(this.target);
        enemy.canSpawn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(EnemySpawn enemy in enemies)
            {
                Spawn(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
        }
    }
}
