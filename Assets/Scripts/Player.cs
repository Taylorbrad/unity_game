using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public HealthBar healthBar;
  public HealthBar manaBar;
  int currentHealth;
  int currentMana;
  public bool isManaEmpty;
  public bool isDead;
  public int maxHealth;
  public int maxMana;
  public Transform player;
  public LayerMask enemyLayers;
  public bool isInvincible;
  public int invincibilityFrames;
  public bool flashWhileInvincible;
  public SpriteRenderer playerSprite;
  //public SpriteAnimator spriteAnimator;


  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);

    currentMana = maxMana;
    manaBar.SetMaxHealth(maxMana);

    isManaEmpty = false;
  }
  void Update()
  {
    if (!isDead)
    {
      if (invincibilityFrames > 0)
      {
        --invincibilityFrames;
        if (((invincibilityFrames % 45) == 0) && flashWhileInvincible)
        {
          playerSprite.enabled = !playerSprite.enabled;
        }
      }
      else
      {
        isInvincible = false;
        playerSprite.enabled = true;
      }

      Collider2D[] getHit = Physics2D.OverlapCircleAll(player.position, 0.5f, enemyLayers); //Detect enemies in my own hitbox. If they exist, take damage

      foreach(Collider2D enemy in getHit) //If you get hit by 2 enemies on the same frame, you could be damaaged by both
      {
          if (!enemy.GetComponent<Enemy>().isDead && !isInvincible) // ie, if enemy is not dead, do this. otherwise skip it
          {
            TakeDamage(enemy.GetComponent<Enemy>().attackPower);
          }
      }
    }

  }

  public void TakeDamage(int damage)
  {
    //spriteAnimator.Play("GetHit");
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
      //Debug.Log("Player Health: " + currentHealth);
      MakeInvincible(500, true);
      if(currentHealth <= 0)// && !isDead)
      {
          Die();
      }
  }
  public void ReduceMana(int manaCost)
  {
    currentMana -= manaCost;
    manaBar.SetHealth(currentMana);
    if (currentMana <= 0)
    {
      isManaEmpty = true;
    }
  }
  void Die()
  {
      //Debug.Log("Player died!");
      isDead = true;
  }
  public void MakeInvincible(int framesOfInvincibility, bool flash)
  {
    invincibilityFrames += framesOfInvincibility;
    isInvincible = true;
    flashWhileInvincible = flash;
  }
}
