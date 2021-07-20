using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
  public bool isSwitched;
  public SpriteRenderer leverSprite;
  public GameObject moveBlock;

    // Start is called before the first frame update
    void Start()
    {
      isSwitched = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void switchSwitch()
    {
      isSwitched = !isSwitched;
      leverSprite.flipX = !leverSprite.flipX;
      moveBlock.GetComponent<MoveableBlock>().move = true;
    }
}
