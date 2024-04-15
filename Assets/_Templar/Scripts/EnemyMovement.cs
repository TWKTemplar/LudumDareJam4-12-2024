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
    public SoundKit[] soundsForRunning;
    public SoundKit[] soundsForWalking;
    private void OnValidate()
    {
        if (myNavAgent == null) myNavAgent = GetComponent<NavMeshAgent>();
    }
    public void UpdateMovement()
    {
        if(enemy.CurrentState == Enemy.EnemyState.Idle)
        {
            Debug.Log("Stopping walk and run sound");
            foreach (var item in soundsForRunning) item.Stop();
            foreach (var item in soundsForWalking) item.Stop();
        }
        else if (enemy.CurrentState == Enemy.EnemyState.Walk)
        {
            Debug.Log(" walk  sound");
            foreach (var item in soundsForRunning) item.Stop();
            foreach (var item in soundsForWalking) item.PlayLoop();

            myNavAgent.speed = EnemyWalkSpeed;
        }
        else if (enemy.CurrentState == Enemy.EnemyState.Run)
        {
            Debug.Log(" run  sound");
            foreach (var item in soundsForRunning) item.PlayLoop();
            foreach (var item in soundsForWalking) item.Stop();

            myNavAgent.speed = EnemyRunSpeed;
        }
        else if (enemy.CurrentState == Enemy.EnemyState.RunAway)
        {
            Debug.Log(" run  sound2");
            foreach (var item in soundsForRunning) item.PlayLoop();
            foreach (var item in soundsForWalking) item.Stop();

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
