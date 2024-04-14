using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int Health = 1;
    public int MaxHealth = 10;
    private void OnValidate()
    {
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
    }
    public void Damage(int dmg = 1)
    {
        Health -= dmg;
        if(Health <= 0)
        {
            Die();
        }
    }
    public void PushAway(Vector3 EnemyPos, float PushForce)
    {
        var forceDir = (transform.position - EnemyPos).normalized * PushForce;
        forceDir.y = 5;
        playerMovement.rb.AddForce(forceDir,ForceMode.Impulse);
    }
    public void Die()
    {
        Destroy(gameObject);
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
    }
}
