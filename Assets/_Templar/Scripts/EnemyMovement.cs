using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState { Attack, RunAway, Idle};
    public EnemyState CurrentState = EnemyState.Idle;
    public PlayerMovement player;
    public float SightRange = 7;
    private void OnDrawGizmos()
    {
        if (player == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * SightRange);
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
