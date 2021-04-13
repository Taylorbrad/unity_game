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
    public Transform attackPoint;
    public Transform player;


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
          //Debug.Log("Player direction: " + animDirection);
            if (moveVec.magnitude > 1)
            {
                moveVec = moveVec.normalized;
            }

            rb.velocity = moveVec * moveSpeed;

            if (moveVec != Vector2.zero && !Input.GetKey(KeyCode.LeftShift))
            {
              //Debug.Log(Input.GetKey());
                if (Input.GetKey(KeyCode.W))//(moveVec[1] > 0 && !Input.GetKey(KeyCode.A))
                {
                  spriteAnimator.Play("WalkUp");

                  attackPoint.position = player.position + new Vector3(0,1,0);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                  spriteAnimator.Play("WalkL");
                  attackPoint.position = player.position + new Vector3(-1,0,0);
                }
                else if (Input.GetKey(KeyCode.D))//(moveVec[1] == 0)
                {
                  spriteAnimator.Play("WalkR");
                  attackPoint.position = player.position + new Vector3(1,0,0);
                }

                else if (Input.GetKey(KeyCode.S))
                {
                  spriteAnimator.Play("WalkDown");

                  attackPoint.position = player.position + new Vector3(0,-1,0);
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
              else if (animDirection == "R")
              {
                spriteAnimator.Play("IdleR");
              }
              else
              {
                spriteAnimator.Play("IdleL");
              }
            }

            if (Input.GetKey(KeyCode.Space)) // Attack
            {
                attackTimeCounter = attackTime;
                attacking = true;
                //rb.velocity = Vector2.zero;
                if (animDirection == "Up")
                {
                  spriteAnimator.Play("AttackUp");
                }
                else if (animDirection == "R")
                {
                  spriteAnimator.Play("AttackR");
                }
                else if (animDirection == "L")
                {
                  spriteAnimator.Play("AttackL");
                }
                else
                {
                  spriteAnimator.Play("AttackDown");
                }
                GetComponent<PlayerCombat>().Attack();
            }
            if (Input.GetKey(KeyCode.Mouse0)) //Lightning Ranged attack
            {
                attacking = true;
                spriteAnimator.Play("Invincible");
                GetComponent<PlayerCombat>().LightningAttack();
            }
            if (Input.GetKey(KeyCode.Z)) //Roll
            {
              spriteAnimator.Play("Roll");


            }

            //if (moveVec.x != 0)
            {
                //spriteAnimator.FlipTo(moveVec.x);
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
        else if (animDirection == "   R")
        {
          return "R";
        }
        //Debug.Log(animDirection);
      }


      if (animDirection == "Down")
      {
        return "Down";
      }
      else
      {
        return "L";
      }
    }



}
