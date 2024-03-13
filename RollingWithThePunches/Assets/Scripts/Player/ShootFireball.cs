using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour, IPlayerCommand
{
    [SerializeField] private GameObject fireballPrefab; 
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private float spawnHeight = 0.5f;

    public void Execute(GameObject gameObject)
    {
        Vector3 spawnPosition = gameObject.transform.position + gameObject.transform.right * spawnDistance + new Vector3(0, spawnHeight, 0);
        Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
    }
}
