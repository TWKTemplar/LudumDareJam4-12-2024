using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int Health = 1;
    public int MaxHealth = 10;
    public GameManager gameManager;
    public GameObject playerCorpsePrefab;
    private void OnValidate()
    {
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
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
    }
    public void PushAway(Vector3 EnemyPos, float PushForce)
    {
        var forceDir = (transform.position - EnemyPos).normalized * PushForce;
        forceDir.y = 5;
        playerMovement.rb.AddForce(forceDir,ForceMode.Impulse);
    }
    public void Die()
    {
        gameObject.SetActive(false);
        Instantiate(playerCorpsePrefab, transform.position, Quaternion.identity);
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
