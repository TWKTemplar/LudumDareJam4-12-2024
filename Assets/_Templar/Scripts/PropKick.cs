using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropKick : TriggerDetector
{
    public EnemySpawner enemySpawner;
    private void OnValidate()
    {
        if (enemySpawner == null) enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TargetTagIsWithinRange = true;
            OnPlayerTriggerEnter.Invoke();
        }
        if (other.CompareTag("Player"))
        {
            TargetTagIsWithinRange = true;
        }
    }
    private void Update()
    {
        if (TargetTagIsWithinRange && Input.GetButtonDown("Jump"))
        {
            OnPlayerInteract.Invoke();
            Debug.Log("player kicked Me!");
            if (AudioSourcePrefabToSpawnInOnInteract != null) Instantiate(AudioSourcePrefabToSpawnInOnInteract, transform.position, Quaternion.identity);
            if (SpawnMeOnInteract != null) Instantiate(SpawnMeOnInteract, transform.position, Quaternion.identity);
            if (DestoryMeOnInteract != null) Destroy(DestoryMeOnInteract);
        }
    }
    public void KickAwayFromPlayer()
    {

    }
    public void KickAwayFromEnemy()
    {

    }
}
