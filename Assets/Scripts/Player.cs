using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public HealthBar healthBar;
  int currentHealth;
  public int maxHealth;
  public Transform player;
  public LayerMask enemyLayers;
  public bool isInvincible;
  public int invincibilityFrames;
  public SpriteRenderer playerSprite;
  //public SpriteAnimator spriteAnimator;


  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);
  }
  void Update()
  {
    if (invincibilityFrames > 0)
    {
      --invincibilityFrames;
      if ((invincibilityFrames % 45) == 0)
      {
        playerSprite.enabled = !playerSprite.enabled;
      }
    }
    else
    {
      isInvincible = false;
      playerSprite.enabled = true;
    }

    Collider2D[] getHit = Physics2D.OverlapCircleAll(player.position, 0.5f, enemyLayers);


    // Damage them
    foreach(Collider2D enemy in getHit)
    {
        if (!enemy.GetComponent<Enemy>().isDead && !isInvincible) // ie, if enemy is not dead, do this. otherwise skip it
        {
          TakeDamage(enemy.GetComponent<Enemy>().attackPower);
          invincibilityFrames = 500;
          isInvincible = true;
        }
    }
  }

  public void TakeDamage(int damage)
  {
    //spriteAnimator.Play("GetHit");
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
      Debug.Log("Player Health: " + currentHealth);
      if(currentHealth <= 0)// && !isDead)
      {
          Die();
      }
  }
  void Die()
  {
      Debug.Log("Player died!");
      //isDead = true;
      //spriteAnimator.Play("Die");
  }
}
