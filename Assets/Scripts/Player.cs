using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public HealthBar healthBar;
  int currentHealth;
  public int maxHealth;

  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);
  }

  public void TakeDamage(int damage)
  {

  }
}
