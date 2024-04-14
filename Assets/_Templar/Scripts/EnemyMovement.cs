using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent myNavAgent;
    private void OnValidate()
    {
        if (myNavAgent == null) myNavAgent = GetComponent<NavMeshAgent>();
    }
}
