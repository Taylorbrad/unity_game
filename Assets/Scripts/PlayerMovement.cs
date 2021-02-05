using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public float moveSpeed;
    public SpriteAnimator spriteAnimator;
    public bool attacking;
    private float attackTime;
    private float attackTimeCounter;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVec = Vector2.zero;
        moveVec.x = Input.GetAxisRaw("Horizontal");
        moveVec.y = Input.GetAxisRaw("Vertical");

        if (!attacking) //Walk/Idle
        {

            if (moveVec.magnitude > 1)
            {
                moveVec = moveVec.normalized;
            }

            rb.velocity = moveVec * moveSpeed;

            if (moveVec != Vector2.zero)
            {
                if (moveVec[1] > 0)
                {
                  spriteAnimator.Play("WalkUp");
                }
                else if (moveVec[1] == 0)
                {
                  spriteAnimator.Play("WalkRL");
                }
                else
                {
                  spriteAnimator.Play("WalkDown");
                }
            }
            else
            {
                spriteAnimator.Play("Idle");
            }

            if (Input.GetKey(KeyCode.Space)) // Attack
            {
                attackTimeCounter = attackTime;
                attacking = true;
                rb.velocity = Vector2.zero;
                spriteAnimator.Play("Attack");
                GetComponent<PlayerCombat>().Attack();
            }
            if (Input.GetKey(KeyCode.Z)) //Roll
            {
              spriteAnimator.Play("Roll");
              

            }

            if (moveVec.x != 0)
            {
                spriteAnimator.FlipTo(moveVec.x);
            }

        }
        if (!spriteAnimator.isPlaying)
        {
            attacking = false;
        }
    }

}
