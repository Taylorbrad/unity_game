using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public HealthBar healthBar;
  public HealthBar manaBar;
  int currentHealth;
  int currentMana;
  public int maxHealth;
  public int maxMana;
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

    currentMana = maxMana;
    manaBar.SetMaxHealth(maxMana);
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
  public void ReduceMana(int manaCost)
  {
    currentMana -= manaCost;
    manaBar.SetHealth(currentMana);
  }
  void Die()
  {
      Debug.Log("Player died!");
      //isDead = true;
      //spriteAnimator.Play("Die");
  }
}
