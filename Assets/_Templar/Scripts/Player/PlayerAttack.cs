using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Settings")]
    [Range(2, 20)] public float SightRange = 7;
    public float PlayerAttackCoolDown = 1;
    public float PlayerHealDelay = 1;
    [SerializeField]private float TempPlayerAttackCoolDown;
    [Header("ref")]
    public Player player;
    public AnimatedBlood[] animatedBloods;
    public Transform[] PlayerBloodCursors;
    public EnemySpawner enemySpawner;
    [Header("Dynamic ref")]
    public Enemy ClosestEnemy;
    private void Start()
    {
        if (enemySpawner == null) enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    private void Update()
    {
        ClosestEnemy = enemySpawner.GetClosestEnemy(transform.position,SightRange);
        if(ClosestEnemy != null)
        {
            MoveCursors();
            AttackStep();
        }
    }
    private void AttackStep()
    {
        TempPlayerAttackCoolDown -= Time.deltaTime;
        if (TempPlayerAttackCoolDown < 0)
        {
            TempPlayerAttackCoolDown = PlayerAttackCoolDown;
            SpawnOrb();
            player.DelayedHeal(PlayerHealDelay);
        }
    }
    private void MoveCursors()
    {
        foreach (var item in PlayerBloodCursors) item.transform.position = ClosestEnemy.transform.position;
    }
    private int PrevOrbSpawn;
    public void SpawnOrb()
    {
        PrevOrbSpawn++;
        if (PrevOrbSpawn >= animatedBloods.Length) PrevOrbSpawn = 0;
        animatedBloods[PrevOrbSpawn].SpawnBlood();
    }
    
    private void OnDrawGizmos()
    {
        if (ClosestEnemy != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, ClosestEnemy.transform.position);
        }
    }
}
