using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour
{
  public Transform blockTransform;

  //public bool isMoved;
  public int amtToMove;
  public bool moveYAxis;
  public bool move;
   //True means up/down, false means left/right. +- values define up/down or l/r
    // Start is called before the first frame update
    void Start()
    {
      move = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (amtToMove != 0 & move)
      {
        moveBlock();
      }
    }
    public void moveBlock()
    {
      float localX = blockTransform.localPosition.x;
      float localY = blockTransform.localPosition.y;

      if (moveYAxis)
      {
        if (amtToMove > 0)
        {
          blockTransform.localPosition = new Vector3(localX,(float)(localY+.02),-4);
          amtToMove -= 1;
        }
        if (amtToMove < 0)
        {
          blockTransform.localPosition = new Vector3(localX,(float)(localY-.02),-4);
          amtToMove += 1;
        }
      }
      else
      {
        if (amtToMove > 0)
        {
          blockTransform.localPosition = new Vector3((float)(localX+.02),localY,-4);
          amtToMove -= 1;
        }
        if (amtToMove < 0)
        {
          blockTransform.localPosition = new Vector3((float)(localX-.02),localY,-4);
          amtToMove += 1;
        }
      }
    }
}
