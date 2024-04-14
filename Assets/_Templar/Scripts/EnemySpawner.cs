using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Player player;
    public Transform[] SpawnPoints;
    public EnemyMovement BaseEnemyPrefab;
    public List<EnemyMovement> AllEnemiesInMap;
    public EnemyMovement EnemyPrefab;
    public void GetAllEnemies()
    {
        AllEnemiesInMap.Clear();
        var arrayOfEnemies = FindObjectsOfType<EnemyMovement>();
        for (int i = 0; i < arrayOfEnemies.Length; i++)
        {
            AllEnemiesInMap.Add(arrayOfEnemies[i]);
        }
    }
    private void OnValidate()
    {
        GetPlayerIfNull();
        GetAllEnemies();
    }
    private void Start()
    {
        GetPlayerIfNull();
        GetAllEnemies();
    }
    public void GetPlayerIfNull()
    {
        if (player == null) player = FindObjectOfType<Player>();
    }
    public void GetSpawnPoints()
    {
        SpawnPoints = GetComponentsInChildren<Transform>();
    }
    public void SpawnEnemy()
    {
        if(EnemyPrefab == null)
        {
            Debug.LogError("No Enemy Prefab to spawn");
            return;
        }
        var spawnPoint = GetRandomSpawnPoint();
        var enemy = Instantiate(EnemyPrefab, spawnPoint,Quaternion.identity);
        AllEnemiesInMap.Add(enemy);
    }
    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 SpawnPoint;
        int randomID = Random.Range(1, SpawnPoints.Length);
        SpawnPoint = SpawnPoints[randomID].transform.position;
        return SpawnPoint;
    }
}
