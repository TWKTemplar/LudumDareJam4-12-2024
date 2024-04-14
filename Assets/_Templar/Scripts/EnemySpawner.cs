using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool SpawnEnemiesOnTimer = false;
    public float SpawnEnemyTime = 10;
    [SerializeField] private float TempSpawnEnemyTime = 10;
    public Player player;
    public Transform[] SpawnPoints;
    public Enemy BaseEnemyPrefab;
    public List<Enemy> AllEnemiesInMap;
    public Enemy EnemyPrefab;
    public void AggroAllEnemies()
    {
        foreach (var enim in AllEnemiesInMap)
        {
            enim.ForceEnemyStateToRun();
        }
    }
    public void GetAllEnemies()
    {
        AllEnemiesInMap.Clear();
        var arrayOfEnemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < arrayOfEnemies.Length; i++)
        {
            AllEnemiesInMap.Add(arrayOfEnemies[i]);
        }
    }
    private void Update()
    {
        if(SpawnEnemiesOnTimer)
        {
            TempSpawnEnemyTime -= Time.deltaTime;
            if(TempSpawnEnemyTime <= 0)
            {
                SpawnEnemy();
                TempSpawnEnemyTime = SpawnEnemyTime;
            }
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
    public void RemoveEnemy(Enemy enemy)
    {
        AllEnemiesInMap.Remove(enemy);
        Destroy(enemy);
    }
    public Enemy GetClosestEnemy(Vector3 position,float sightRange = 7)
    {
        float closestDistance = float.MaxValue;
        Enemy closestEnemy = null;

        foreach (Enemy enemy in AllEnemiesInMap)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance <= sightRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 SpawnPoint;
        int randomID = Random.Range(1, SpawnPoints.Length);
        SpawnPoint = SpawnPoints[randomID].transform.position;
        return SpawnPoint;
    }
}
