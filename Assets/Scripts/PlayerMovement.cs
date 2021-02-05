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
          string animDirection = GetAnimationDirection(spriteAnimator.GetCurrentAnimation().GetAnimationName());

            if (moveVec.magnitude > 1)
            {
                moveVec = moveVec.normalized;
            }

            rb.velocity = moveVec * moveSpeed;

            if (moveVec != Vector2.zero)
            {
              //Debug.Log(Input.GetKey());
                if (Input.GetKey(KeyCode.W))//(moveVec[1] > 0 && !Input.GetKey(KeyCode.A))
                {
                  spriteAnimator.Play("WalkUp");
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))//(moveVec[1] == 0)
                {
                  spriteAnimator.Play("WalkRL");
                }
                else if (Input.GetKey(KeyCode.S))
                {
                  spriteAnimator.Play("WalkDown");
                }
            }
            else
            {
              if (animDirection == "Down")
              {
                spriteAnimator.Play("IdleDown");
              }
              else if (animDirection == "Up")
              {
                spriteAnimator.Play("IdleUp");
              }
              else
              {
                spriteAnimator.Play("Idle");
              }
            }

            if (Input.GetKey(KeyCode.Space)) // Attack
            {
                attackTimeCounter = attackTime;
                attacking = true;
                rb.velocity = Vector2.zero;
                if (moveVec[1] > 0 || animDirection == "Up")
                {
                  spriteAnimator.Play("AttackUp");
                }
                else if (moveVec[1] == 0 && animDirection != "Down")
                {
                  spriteAnimator.Play("AttackRL");
                }
                else
                {
                  spriteAnimator.Play("AttackDown");
                }
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
    public string GetAnimationDirection(string inAnimName)
    {

      //string animDirection = "";
      string animDirection = "    ";
      //string inAnimName = "walkUp";
      int animDirectionSize = animDirection.Length;
      int animNameSize = (inAnimName.Length);

      for (int x = 0; x < 4; ++x)
      {
        int addToEndDirec = animDirectionSize - (x+1);
        //string toInsert = inAnimName[animNameSize - (x + 1)];
        string toInsert = inAnimName.Substring(animNameSize - (x + 1), 1);
        //cout << inAnimName.size();
        animDirection = animDirection.Remove(addToEndDirec, 1).Insert(addToEndDirec, toInsert);
        //animDirection[] = ;
        if ( animDirection == "  Up")
        {
          return "Up";
        }
        //Debug.Log(animDirection);
      }
      if (animDirection == "Down")
      {
        return "Down";
      }
      else
      {
        return "RL";
      }
    }



}
