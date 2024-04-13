using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState { Attack, RunAway, Idle};
    [Header("Settings")]
    public EnemyState CurrentState = EnemyState.Idle;
    [Range(2,20)] public float SightRange = 7;
    [Header("ref")]
    public Player player;
    private void OnValidate()
    {
        GetPlayerIfNull();
    }
    public void GetPlayerIfNull()
    {
        if (player == null) player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        GetPlayerIfNull();
    }
    private void OnDrawGizmos()
    {
        if (IsPlayerWithinSight())
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * GetPlayerDist());

        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * SightRange);

        }
    }
    public void Spawn(PlayerMovement player, Vector3 spawnPoint, EnemyState enemyState)
    {
        transform.position = spawnPoint;
        CurrentState = enemyState;
    }
    public void Update()
    {

    }
    public bool IsPlayerWithinSight()
    {
        return GetPlayerDist() < SightRange;
    }
    public float GetPlayerDist()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
}
