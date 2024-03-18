using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject homingMissilePrefab;
    [SerializeField] private float health = 100f; 
    public float spawnInterval = 3f;
    public float spawnRadius = 5f;
    private bool isWithinTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isWithinTrigger = true;

            StartCoroutine(SpawnMissiles());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isWithinTrigger = false;
        }
    }

    IEnumerator SpawnMissiles()
    {
        while (isWithinTrigger)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 randomOffset = Random.insideUnitCircle.normalized * spawnRadius;
                Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
                Instantiate(homingMissilePrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
