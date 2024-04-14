using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SummonMovement : MonoBehaviour
{
    public Summon summon;
    public NavMeshAgent myNavAgent;
    private void OnValidate()
    {
        if (myNavAgent == null) myNavAgent = GetComponent<NavMeshAgent>();
    }
    public void UpdateMovement()
    {
        if (summon.CurrentState == Summon.summonState.Idle)
        {
            myNavAgent.SetDestination(transform.position);//stop basicly
        }
    }

    private void Update()
    {
        
        if (summon.CurrentState == Summon.summonState.ReturnToPlayer)
        {
            myNavAgent.SetDestination(summon.player.transform.position);
        }
        else if (summon.CurrentState == Summon.summonState.AttackNearEnemy)
        {
            if(summon.closestEnemy != null)
            {
                myNavAgent.SetDestination(summon.closestEnemy.transform.position);
            }
            else
            {
                summon.SetSummonStateToIdle();
            }
        }
    }
}
