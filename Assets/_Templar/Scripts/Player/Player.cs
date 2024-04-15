using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public int Health = 1;
    public int MaxHealth = 10;
    public GameManager gameManager;
    public Rigidbody playerCorpsePrefab;
    public SummonSpawner summonSpawner;
    private void OnValidate()
    {
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        if (summonSpawner == null) summonSpawner = GetComponent<SummonSpawner>();
    }
    private void Start()
    {
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        if (summonSpawner == null) summonSpawner = GetComponent<SummonSpawner>();
    }
    public void Damage(int dmg = 1)
    {
        Health -= dmg;
        OnHPChange();
        if (Health <= 0) Die();
    }
    public void OnHPChange()
    {
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        if (Health > 1) gameManager.PlayerHasMoreThan1HP = true;
        else gameManager.PlayerHasMoreThan1HP = false;
        gameManager.UpdateMusic();
        PlayerAttemptToSpawnSummon();
    }
    public void PushAway(Vector3 EnemyPos, float PushForce)
    {
        var forceDir = (transform.position - EnemyPos).normalized * PushForce;
        forceDir.y = 5;
        playerMovement.rb.AddForce(forceDir,ForceMode.Impulse);
    }
    public void Die()
    {
        FindObjectOfType<EnemySpawner>().AggroAllEnemies();
        FindObjectOfType<CameraFollowPlayer>().DoDeathAnim = true;
        playerAttack.ClearAllOrbs();
        var corpse =Instantiate(playerCorpsePrefab, transform.position, Quaternion.identity);
        corpse.velocity = playerMovement.rb.velocity + Vector3.up*5;
        transform.parent = corpse.transform;
        gameObject.SetActive(false);
    }
    public void PlayerAttemptToSpawnSummon()
    {
        if(MaxHealth == Health)
        {
            summonSpawner.SpawnSummon();
            Health = 1;
            OnHPChange();
        }
    }
    public void DelayedHeal(float Delay = 1)
    {
        Invoke("HealBy1", Delay);
    }
    public void HealBy1()
    {
        Heal();
    }
    public void Heal(int heal = 1)
    {
        Health = Mathf.Clamp( Health + heal,0, MaxHealth);
        OnHPChange();
    }
}
