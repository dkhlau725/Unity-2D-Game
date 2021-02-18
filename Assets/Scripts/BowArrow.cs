using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    //Upgrade
    public int bowLevel = 0;
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6 };
    public float[] arrowSpeed = { 1.5f, 2f, 2.5f, 3f, 3.5f, 4.0f };
    protected SpriteRenderer spriteRenderer;

    //Vector3 startPosition;
    public Transform bowPosition;
    public GameObject arrowPrefabRight;
    public GameObject arrowPrefabLeft;
    public Player player;
    public testArrow rightArrow;
    public testArrow2 leftArrow;

    private Animator anim;
    private float drawSpeed = 1.3f;
    private float lastShot = 0f;
    public int arrowCount = 10;
    public int arrowCapacity = 10;

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

     void Start()
    {
        anim = GetComponent<Animator>();
    }

     void Update()
    {
        if (arrowCount == 0)
        {
            anim.SetTrigger("fired");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Time.time - lastShot > drawSpeed && arrowCount != 0)
            {
                if (player.facingRight == true)
                {
                    anim.SetTrigger("fired");
                    GameObject a = Instantiate(arrowPrefabRight) as GameObject;
                    a.transform.position = bowPosition.transform.position + new Vector3(0.0423f, 0, 0);
                    
                }
                else
                {
                    anim.SetTrigger("fired");
                    GameObject a = Instantiate(arrowPrefabLeft) as GameObject;
                    a.transform.position = bowPosition.transform.position + new Vector3(-0.0423f, -0.008f, 0);
                    
                }
                lastShot = Time.time;
                arrowCount--;
                GameManager.instance.arrowCounter.text = arrowCount.ToString() + " / " + arrowCapacity.ToString();
            }
            else if (arrowCount == 0)
            {
                anim.SetTrigger("fired");
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (arrowCount == 0)
            {
                anim.SetTrigger("fired");
            }
            anim.SetTrigger("draw");
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            UnityEngine.Debug.Log("upgrade");
            UpgradeBow();           
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            UnityEngine.Debug.Log("downgrade");
            bowLevel--;
            rightArrow.arrowLvl--;
            leftArrow.arrowLvl--;
            spriteRenderer.sprite = GameManager.instance.bowSprites[bowLevel];
        }
    }
    public void UpgradeBow()
    {
        bowLevel++;
        rightArrow.arrowLvl++;
        leftArrow.arrowLvl++;

        spriteRenderer.sprite = GameManager.instance.bowSprites[bowLevel];

        //change stats
    }

    public void SetBowLevel(int level)
    {
        bowLevel = level;
        spriteRenderer.sprite = GameManager.instance.bowSprites[bowLevel];
    }

}
