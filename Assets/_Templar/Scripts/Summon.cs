using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Summon : MonoBehaviour
{
    public enum summonState { Idle, ReturnToPlayer, AttackNearEnemy};
    [Header("Settings")]
    public int HP = 10;
    public float PushForce = 25;
    public summonState CurrentState = summonState.Idle;
    [Range(2, 20)] public float SightRange = 5;
    [Header("ref")]
    public Player player;
    public Enemy closestEnemy;
    public EnemySpawner enemySpawner;
    public SummonSpawner summonSpawner;
    public SummonMovement summonMovement;
    public Rigidbody rb;
    private void OnValidate()
    {
        if (player == null) player = FindObjectOfType<Player>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (enemySpawner == null) enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void PushAway(Vector3 EnemyPos, float PushForce)
    {
        var forceDir = (transform.position - EnemyPos).normalized * PushForce;
        forceDir.y = 5;
        rb.AddForce(forceDir, ForceMode.Impulse);
    }
    public void Damage(int damage = 1)
    {
        HP -= damage;
        if (HP <= 0) Die();
    }
    public void Die()
    {
        if (summonSpawner == null) summonSpawner = FindObjectOfType<SummonSpawner>();
        summonSpawner.RemoveSummon(this);
    }
    private void Start()
    {
        if (player == null) player = FindObjectOfType<Player>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (enemySpawner == null) enemySpawner = FindObjectOfType<EnemySpawner>();
        //InvokeRepeating("SlowUpdate", Random.value+1, 1);
        ApplyRandomPositionBias();
    }
    private void ApplyRandomPositionBias()
    {
        SightRange = Random.Range(SightRange, SightRange+2);
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
    public void Spawn(PlayerMovement player, Vector3 spawnPoint, summonState summonState)
    {
        transform.position = spawnPoint;
        CurrentState = summonState;
    }

    public void Update()
    {
        if(CurrentState == summonState.ReturnToPlayer)
        {
            if (IsPlayerWithinSight())
            {
                SetSummonStateToIdle();
            }
            else if(!IsPlayerWithinSight(SightRange*2f))
            {
                Debug.Log("Too far.. Teleporting");
                transform.position = Vector3.Lerp(player.transform.position,transform.position,0.5f);
            }
        }
        else if (CurrentState == summonState.AttackNearEnemy)
        {
            if (!IsPlayerWithinSight())
            {
                SetSummonStateToIdle();
            }
            
        }
        else if (CurrentState == summonState.Idle)
        {
            if (IsPlayerWithinSight())
            {
                closestEnemy = enemySpawner.GetClosestEnemy(transform.position,SightRange);
                if (closestEnemy != null)
                {
                    SetSummonStateToAttack();
                }
            }
            else
            {
                SetSummonStateToReturnToPlayer();
            }
            
        }
    }
    public void SetSummonStateToReturnToPlayer()
    {
        Debug.Log("Return to player");
        CurrentState = summonState.ReturnToPlayer;
        summonMovement.UpdateMovement();
    }
    public void SetSummonStateToIdle()
    {
        Debug.Log("Idle");
        CurrentState = summonState.Idle;
        summonMovement.UpdateMovement();
    }
    public void SetSummonStateToAttack()
    {
        Debug.Log("attacking");
        CurrentState = summonState.AttackNearEnemy;
        summonMovement.UpdateMovement();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Summon Collided With Enemy");
            var enim = other.gameObject.GetComponent<Enemy>();
            enim.PushAway(transform.position, PushForce);
            enim.SummonDamage(10);
            Damage(8);
        }
    }
    public bool IsPlayerWithinSight(float range = 0)
    {
        if (range == 0) range = SightRange;
        return GetPlayerDist() < range;
    }
    public float GetPlayerDist()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
}
