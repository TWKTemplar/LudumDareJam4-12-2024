using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Walk, Run, Idle, RunAway };
    [Header("Settings")]
    public int HP = 50;
    public int EnemyDamage = 10;
    public float PushForce = 25;
    public EnemyState CurrentState = EnemyState.Idle;
    [Range(2, 20)] public float SightRange = 10;
    [Range(1, 20)] public float RunDurationInSeconds = 7;
    [Range(1, 20)] public float WalkDurationInSeconds = 7;
    [Header("ref")]
    public Player player;
    public EnemyMovement enemyMovement;
    public EnemySpawner enemySpawner;
    public Rigidbody rb;

    public void PushAway(Vector3 EnemyPos, float PushForce)
    {
        var forceDir = (transform.position - EnemyPos).normalized * PushForce;
        forceDir.y = 5;
        rb.AddForce(forceDir, ForceMode.Impulse);
    }
    public void Damage(int damage = 1)
    {
        HP -= damage;
        SetEnemyStateToRunAway();
        if (HP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if(enemySpawner == null) enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.RemoveEnemy(this);
    }

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (player == null) player = FindObjectOfType<Player>();
        if (enemySpawner == null) enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (player == null) player = FindObjectOfType<Player>();
        if (enemySpawner == null) enemySpawner = FindObjectOfType<EnemySpawner>();
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
        enemyMovement.UpdateMovement();
        Invoke("SetEnemyStateToRun", WalkDurationInSeconds);
    }
    public void SetEnemyStateToRun()
    {
        CurrentState = EnemyState.Run;
        enemyMovement.UpdateMovement();
        Invoke("SetEnemyStateToWalk", RunDurationInSeconds);
    }
    public void SetEnemyStateToRunAway()
    {
        CurrentState = EnemyState.RunAway;
        enemyMovement.UpdateMovement();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided With Player");
            SetEnemyStateToRunAway();
            player.PushAway(transform.position, PushForce);
            player.Damage(EnemyDamage);
        }
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
