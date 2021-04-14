using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public bool isDead;
    public SpriteAnimator spriteAnimator;
    public int attackPower = 50;
    public int invincibilityFrames;
    public bool isInvincible;
    public SpriteRenderer enemySprite;

    public HealthBar healthBar;

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
      if (invincibilityFrames > 0)
      {
        invincibilityFrames--;
        if (invincibilityFrames % 45 == 0)
        {
          enemySprite.enabled = !enemySprite.enabled;
        }
      }
      else
      {
        isInvincible = false;
        enemySprite.enabled = true;
      }

      if (!spriteAnimator.isPlaying && !isDead)
      {
        spriteAnimator.Play("Idle");
      }

    }

    public void TakeDamage(int damage)
    {
      if (!isInvincible)
      {
        spriteAnimator.Play("GetHit");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        invincibilityFrames = 200;
        isInvincible = true;
        //Debug.Log("Enemy Health: " + currentHealth);
        if(currentHealth <= 0 && !isDead)
        {
            Die();
        }
      }
    }
    void Die()
    {
        Debug.Log("Enemy died!");
        isDead = true;
        spriteAnimator.Play("Die");
    }

}
