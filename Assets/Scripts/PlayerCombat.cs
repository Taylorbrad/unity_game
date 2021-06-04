using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public Transform rangedAttackPoint;
    public Rigidbody2D LightningRB;
    public BoxCollider2D LightningBC;
    int lightningExistTime = 0;

    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public int lightningDamage = 20;
    Vector3 mousePos;
    private Camera cam;

    void Start()
    {
      cam = Camera.main;
    }
    void Update()
    {
      mousePos = Input.mousePosition;
      if (lightningExistTime > 0)
      {
        --lightningExistTime;
        rangedAttackPoint.Translate(-Time.deltaTime*10, 0, 0);
        Collider2D[] hitEnemies = DetectLayerCollision(rangedAttackPoint, (float).5, enemyLayers);//Physics2D.OverlapCircleAll(rangedAttackPoint.position, (float).5, enemyLayers);
        DamageEnemies(hitEnemies, lightningDamage);
      }
      else
      {
        rangedAttackPoint.SetPositionAndRotation(GetComponent<Player>().transform.position, rangedAttackPoint.rotation);
      }
    }

    public void Attack()
    {
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
    public void LightningAttack()
    {
      int manaCost = 15;
      //int damage = 20;
      if (lightningExistTime == 0 && !GetComponent<Player>().isManaEmpty)
      {
        GetComponent<Player>().AdjustMana(manaCost);
        float angle = GetAngleFromPlayerToMouse();
        //Debug.Log(angle);
        //Debug.Log(rangedAttackPoint.rotation.eulerAngles);
        rangedAttackPoint.Rotate(0,0,-(rangedAttackPoint.rotation.eulerAngles[2]) + -(angle) -90);
        lightningExistTime = 121;
      }



    }
    public float GetAngleFromPlayerToMouse()
    {
      Vector3 mousePosWorld = cam.ScreenToWorldPoint(new Vector3(mousePos[0],mousePos[1], cam.nearClipPlane));
      Vector2 playerPos = GetComponent<PlayerMovement>().player.position;
      float deltaX = mousePosWorld[0] - playerPos[0];
      float deltaY = mousePosWorld[1] - playerPos[1];
      return (Mathf.Atan2(deltaX, deltaY) * 180/Mathf.PI);
    }
    Collider2D[] DetectLayerCollision(Transform collisionPoint, float collisionRadius, LayerMask layerToCollide)
    {
      return Physics2D.OverlapCircleAll(rangedAttackPoint.position, (float).5, enemyLayers);
    }

    void DamageEnemies(Collider2D[] enemiesToDamage, int damage)
    {
      foreach (Collider2D enemy in enemiesToDamage)
      {
        if (!enemy.GetComponent<Enemy>().isDead)
        {
          enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
      }
    }
/*
    void OnDrawGizmoSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    */
}
