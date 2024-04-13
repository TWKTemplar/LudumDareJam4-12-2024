using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Player player;
    public Transform[] SpawnPoints;
    public EnemyMovement BaseEnemyPrefab;
    private void OnValidate()
    {
        GetPlayerIfNull();
    }
    private void Start()
    {
        GetPlayerIfNull();
    }
    public void GetPlayerIfNull()
    {
        if (player == null) player = FindObjectOfType<Player>();
    }
    public void GetSpawnPoints()
    {
        SpawnPoints = GetComponentsInChildren<Transform>();
    }
    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 SpawnPoint;
        int randomID = Random.Range(1, SpawnPoints.Length);
        SpawnPoint = SpawnPoints[randomID].transform.position;
        return SpawnPoint;
    }
}
