using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSpawner : MonoBehaviour
{
    public Player player;
    public Transform[] SpawnPoints;
    public List<Summon> AllSummonsInMap;
    public Summon SummonPrefab;
    public GameManager gameManager;
    public void GetAllSummons()
    {
        AllSummonsInMap.Clear();
        var arrayOfEnemies = FindObjectsOfType<Summon>();
        for (int i = 0; i < arrayOfEnemies.Length; i++)
        {
            AllSummonsInMap.Add(arrayOfEnemies[i]);
        }
    }
    private void OnValidate()
    {
        GetPlayerIfNull();
        GetAllSummons();    
    }
    private void Start()
    {
        GetPlayerIfNull();
        GetAllSummons();
        OnSummonsChange();
    }
    public void GetPlayerIfNull()
    {
        if (player == null) player = FindObjectOfType<Player>();
    }
    public void GetSpawnPoints()
    {
        SpawnPoints = GetComponentsInChildren<Transform>();
    }
    [ContextMenu("Spawn Summon")]
    public void SpawnSummon()
    {
        if (SummonPrefab == null)
        {
            Debug.LogError("No Summon Prefab to spawn");
            return;
        }
        var spawnPoint = GetRandomSpawnPoint();
        var summon = Instantiate(SummonPrefab, spawnPoint, Quaternion.identity);
        AllSummonsInMap.Add(summon);
        OnSummonsChange();
    }
    public void OnSummonsChange()
    {
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        gameManager.PlayerHasSummons = (AllSummonsInMap.Count > 0);
        gameManager.UpdateMusic();
    }
    public void RemoveSummon(Summon summon)
    {
        OnSummonsChange();
        AllSummonsInMap.Remove(summon);
        Destroy(summon.gameObject);
    }
    public Summon GetClosestSummon(float sightRange = 7)
    {
        float closestDistance = float.MaxValue;
        Summon closestSummon = null;

        foreach (Summon summon in AllSummonsInMap)
        {
            float distance = Vector3.Distance(transform.position, summon.transform.position);
            if (distance <= sightRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestSummon = summon;
            }
        }

        return closestSummon;
    }
    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 SpawnPoint;
        int randomID = Random.Range(1, SpawnPoints.Length);
        SpawnPoint = SpawnPoints[randomID].transform.position;
        return SpawnPoint;
    }
}
