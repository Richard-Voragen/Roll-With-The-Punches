using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    public GameObject enemyPrefab;
    public Vector2 spawnPosition;
    public bool positionIsOffset;
    public float spawnRate;

    [HideInInspector] public float currentTime;
    [HideInInspector] public bool canSpawn;
}