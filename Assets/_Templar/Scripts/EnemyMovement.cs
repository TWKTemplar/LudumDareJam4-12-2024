using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public Enemy enemy;
    public NavMeshAgent myNavAgent;
    public float EnemyWalkSpeed = 4;
    public float EnemyRunSpeed = 10;
    private void OnValidate()
    {
        if (myNavAgent == null) myNavAgent = GetComponent<NavMeshAgent>();
    }
    public void UpdateMovement()
    {
        if(enemy.CurrentState == Enemy.EnemyState.Idle)
        {

        }
        else if (enemy.CurrentState == Enemy.EnemyState.Walk)
        {
            myNavAgent.speed = EnemyWalkSpeed;
        }
        else if (enemy.CurrentState == Enemy.EnemyState.Run)
        {
            myNavAgent.speed = EnemyRunSpeed;
        }
        else if (enemy.CurrentState == Enemy.EnemyState.RunAway)
        {
            myNavAgent.speed = EnemyRunSpeed;
        }
    }
    private void Update()
    {
        if (enemy.CurrentState == Enemy.EnemyState.Idle)
        {

        }
        else if (enemy.CurrentState == Enemy.EnemyState.Walk)
        {
            myNavAgent.SetDestination(enemy.player.transform.position);
        }
        else if (enemy.CurrentState == Enemy.EnemyState.Run)
        {
            myNavAgent.SetDestination(enemy.player.transform.position);
        }
        else if (enemy.CurrentState == Enemy.EnemyState.RunAway)
        {
            myNavAgent.SetDestination(enemy.player.transform.position + (transform.position - enemy.player.transform.position).normalized * 10);
        }
    }
}
