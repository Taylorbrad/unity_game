using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public bool isDead;

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);
        if(currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        isDead = true;
        //Die animation
    }
}
