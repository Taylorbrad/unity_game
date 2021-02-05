using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;


    void Update()
    {
    }

    public void Attack()
    {
        // Play Attack Animation

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<Enemy>().isDead) // ie, if enemy is not dead, do this. otherwise skip it
            {
              enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmoSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
