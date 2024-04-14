using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 1;
    public int MaxHealth = 10;
    public void Damage(int dmg = 1)
    {
        Health -= dmg;
        if(Health <= 0)
        {
            Die();
        }
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
