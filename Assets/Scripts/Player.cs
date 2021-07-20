using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

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

  int gotItemTimer;
  string gotItemName;
  public Transform player;
  public Grid pickupGrid;

  public LayerMask enemyLayers;
  public bool isInvincible;
  public int invincibilityFrames;
  public bool flashWhileInvincible;
  public SpriteRenderer playerSprite;
  public Rigidbody2D rb;

  public Tilemap pickupMap;
  List<string> allItemTypes;
  List<string> allPickupTypes; //I dont think this is a good idea, its not necessary
  Dictionary<string,int> inventory = new Dictionary<string, int>();
  GUIStyle style;
  public GameObject gotItemSprite;
  SpriteRenderer gotItemRender;
  Transform gotItemTransform;

  //Vector3 gotItemOrigin;
  public GameObject batteryUI; //consolidate these 4  into a single UI element
  public GameObject resistorUI;
  public GameObject copperCableUI;
  public GameObject capacitorUI;
  public Sprite batterySprite;
  public Sprite resistorSprite;
  public Sprite copperCableSprite;
  public Sprite capacitorSprite;
  //public SpriteAnimator spriteAnimator;
  public bool nextToLever;
  public Lever lever;
  public int leverCooldown;
  //public GameObject lever;


  // Start is called before the first frame update
  void Start()
  {

    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);

    currentMana = maxMana;
    manaBar.SetMaxHealth(maxMana);

    isManaEmpty = false;
    nextToLever = false;

    allItemTypes = new List<string>();

    allPickupTypes = new List<string>(); //Probably dont do this
    allPickupTypes.Add("health");
    allPickupTypes.Add("mana");

    inventory.Add("battery",0);
    inventory.Add("resistor",0);
    inventory.Add("copperCable",0);
    inventory.Add("capacitor",0);

    gotItemRender = gotItemSprite.GetComponent<SpriteRenderer>();
    gotItemTransform = gotItemSprite.GetComponent<Transform>();
    //gotItemOrigin = gotItemTransform.position;


    for (int i = allItemTypes.Count - 1; i >= 0; --i)
    {
      inventory.Add(allItemTypes[i], 0);
    }
  }
  void Update()
  {
    if (leverCooldown > 0)
    {
      leverCooldown -= 1;
    }

    if (nextToLever & Input.GetKey(KeyCode.E) & leverCooldown == 0)
    {
      lever.switchSwitch();
      leverCooldown = 150;
    }

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

      if (gotItemTimer > 0)
      {
        Vector3 gotItemOrigin = new Vector3(0,(float)1.3,0) + player.position; //the origin of the object in the gameworld shows as (0,1.3,0), relative to the player, so it gives us grief in the script. Hence this weird line.

        --gotItemTimer;

        float sinNum = (gotItemTimer * Mathf.PI * 5) / 125;
        float waveNum = Mathf.Sin(sinNum) / 5;
        gotItemTransform.position = gotItemOrigin + new Vector3(0,waveNum,0); //(gotItemOrigin - player.position) + new Vector3(0,waveNum,0);
        Debug.Log(gotItemTransform.position);
      }
      else
      {
        gotItemSprite.SetActive(false);
        batteryUI.SetActive(false);
        resistorUI.SetActive(false);
        copperCableUI.SetActive(false);
        capacitorUI.SetActive(false);
      }

      Collider2D[] getHit = Physics2D.OverlapCircleAll(player.position, 0.5f, enemyLayers); //Detect enemies in my own hitbox. If they exist, take damage

      foreach(Collider2D enemy in getHit) //If you get hit by 2 enemies on the same frame, you could be damaaged by both
      {
          if (!enemy.GetComponent<Enemy>().isDead && !isInvincible) // ie, if enemy is not dead, do this. otherwise skip it
          {
            AdjustHealth(enemy.GetComponent<Enemy>().attackPower);
          }
      }
    }

  }

  public void AdjustHealth(int damage)
  {
    //spriteAnimator.Play("GetHit");
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
      //Debug.Log(" Health: " + currentHealth);
      if (damage > 0)
      {
        MakeInvincible(500, true);
      }

      if(currentHealth <= 0)// && !isDead)
      {
          Die();
      }
      if (currentHealth > maxHealth)
      {
        currentHealth = maxHealth;
      }
  }
  public void AdjustMana(int manaCost)
  {
    currentMana -= manaCost;
    manaBar.SetHealth(currentMana);
    if (currentMana <= 0)
    {
      isManaEmpty = true;
    }
    else
    {
      isManaEmpty = false;
    }

    if (currentMana > maxMana)
    {
      currentMana = maxMana;
    }
  }
  void Die()
  {
      //Debug.Log(" died!");
      isDead = true;
  }
  public void MakeInvincible(int framesOfInvincibility, bool flash)
  {
    invincibilityFrames += framesOfInvincibility;
    isInvincible = true;
    flashWhileInvincible = flash;
  }
  void OnTriggerEnter2D(Collider2D collidedWith)
  {
    if (collidedWith.CompareTag("Collectable"))
    {
      string itemType = collidedWith.gameObject.GetComponent<Collectable>().itemType;
      AddToInventory(itemType);
      //Debug.Log("You got a " + itemType + "! You have: " + inventory[itemType]);
      Destroy(collidedWith.gameObject);
    }
    else if (collidedWith.CompareTag("Pickup"))
    {
      string pickupType = collidedWith.gameObject.GetComponent<Pickup>().pickupType;
      //Debug.Log("we got a " + pickupType + " pickup ");

      switch(pickupType)
      {
        case "health":
          AdjustHealth(-60);
          break;
        case "mana":
          AdjustMana(-60);
          break;
      }
      //GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
      //Vector3Int tilePos = pickupGrid.WorldToCell(player.position);
      //pickupMap.SetTile(tilePos,null);
      Vector3Int tilePos = pickupGrid.WorldToCell(player.position);
      pickupMap.SetTile(tilePos,null);
      //Debug.Log(tilePos);
      //Destroy(collidedWith.gameObject);
    }
    else if (collidedWith.CompareTag("Lever"))
    {
      lever = collidedWith.gameObject.GetComponent<Lever>();
      nextToLever = !nextToLever;
    }

  }
  void OnTriggerExit2D(Collider2D collidedWith)
  {
    if (collidedWith.CompareTag("Lever"))
    {
      lever = null;
      nextToLever = !nextToLever;
    }
  }


  void AddToInventory(string addItem)
  {
    gotItem(addItem);
    inventory[addItem] += 1;
  }
  void gotItem(string inItem)
  {
    gotItemName = inItem;
    gotItemTimer = 90;
    gotItemSprite.SetActive(true);
    batteryUI.SetActive(false);
    resistorUI.SetActive(false);
    copperCableUI.SetActive(false);
    capacitorUI.SetActive(false);

    //capacitorUI.Image;
  }
  void OnGUI()
  {
    if(PauseGame.isPaused)
    {
      GUI.Label(new Rect(550, 320, 100, 20), inventory["battery"].ToString());
      GUI.Label(new Rect(475, 320, 100, 20), inventory["resistor"].ToString());
      GUI.Label(new Rect(400 , 320, 100, 20), inventory["copperCable"].ToString());
      GUI.Label(new Rect(325, 320, 100, 20), inventory["capacitor"].ToString());
    }

    if (gotItemTimer > 0)
    {
      switch (gotItemName)
      {
        case "battery":
          batteryUI.SetActive(true);
          gotItemRender.sprite = batterySprite;
          GUI.Label(new Rect(550, 320, 100, 20), inventory["battery"].ToString());
        break;

        case "resistor":
          resistorUI.SetActive(true);
          gotItemRender.sprite = resistorSprite;
          GUI.Label(new Rect(475, 320, 100, 20), inventory["resistor"].ToString());
        break;

        case "copperCable":
          copperCableUI.SetActive(true);
          gotItemRender.sprite = copperCableSprite;
          GUI.Label(new Rect(400 , 320, 100, 20), inventory["copperCable"].ToString());
        break;

        case "capacitor":
          capacitorUI.SetActive(true);
          gotItemRender.sprite = capacitorSprite;
          GUI.Label(new Rect(325, 320, 100, 20), inventory["capacitor"].ToString());
        break;


      }


      //
    }
  }

}
