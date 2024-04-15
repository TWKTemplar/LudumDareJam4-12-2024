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
        
    }

    private void Update()
    {
        if (summon.CurrentState == Summon.summonState.Idle)
        {
        }
        else if (summon.CurrentState == Summon.summonState.ReturnToPlayer)
        {
            myNavAgent.SetDestination(Vector3.Lerp(summon.player.transform.position,transform.position,0.5f));
        }
        else if (summon.CurrentState == Summon.summonState.AttackNearEnemy)
        {
            if(summon.closestEnemy != null)
            {
                myNavAgent.SetDestination(summon.closestEnemy.transform.position);
            }
            else
            {
                Debug.Log("Enemy gone returning to idle");
                summon.SetSummonStateToIdle();
            }
        }
    }
}
