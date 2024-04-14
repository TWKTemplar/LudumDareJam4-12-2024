using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public enum EnemyState { Walk, Run, Idle };
    [Header("Settings")]
    public EnemyState CurrentState = EnemyState.Idle;
    [Range(2, 20)] public float SightRange = 7;
    [Range(1, 20)] public float RunDurationInSeconds = 7;
    [Range(1, 20)] public float WalkDurationInSeconds = 7;
    [Header("ref")]
    public Player player;
    public EnemyMovement enemyMovement;
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
        if(CurrentState == EnemyState.Idle)
        {
            if (IsPlayerWithinSight())
            {
                CurrentState = EnemyState.Run;
                Invoke("SetEnemyStateToWalk", RunDurationInSeconds);
            }
        }
    }
    public void SetEnemyStateToWalk()
    {
        CurrentState = EnemyState.Walk;
        Invoke("SetEnemyStateToRun", WalkDurationInSeconds);
    }
    public void SetEnemyStateToRun()
    {
        CurrentState = EnemyState.Run;
        Invoke("SetEnemyStateToWalk", RunDurationInSeconds);
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
