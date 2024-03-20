using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour, IEnemyController
{
    public GameObject player;
    public GameObject homingMissilePrefab;
    public float spawnInterval = 3f;
    public float spawnRadius = 5f;
    private bool isWithinTrigger = false;
    private bool stunned = false;

    public void Stun(bool stund)
    {
        this.stunned = stund;
    }

    public void SetUpProcess(GameObject targ)
    {
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < 40f && isWithinTrigger == false)
        {
            isWithinTrigger = true;
            StartCoroutine(SpawnMissiles());
        }
        else if (Vector2.Distance(player.transform.position, this.transform.position) > 40f)
        {
            isWithinTrigger = false;
        }
    }

    IEnumerator SpawnMissiles()
    {
        while (isWithinTrigger && !stunned)
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
