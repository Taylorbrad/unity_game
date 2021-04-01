using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public bool isDead;
    public SpriteAnimator spriteAnimator;

    public HealthBar healthBar;

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
      if (!spriteAnimator.isPlaying && !isDead)
      {
        spriteAnimator.Play("Idle");
      }

    }

    public void TakeDamage(int damage){
      spriteAnimator.Play("GetHit");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
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
        spriteAnimator.Play("Die");
    }
}
