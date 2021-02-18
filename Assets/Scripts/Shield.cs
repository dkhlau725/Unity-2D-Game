using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Collidable
{
    //damage struct
    public int[] blockPoint = { 0, 2, 3, 4, 5, 6 };
    public float[] pushForce = { 4.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.5f };

    //upgrade
    public int shieldLevel = 0;
    private SpriteRenderer spriteRenderer;

    private Animator anim;
    private float cooldown = 1.0f;
    private float lastBlock;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start() 
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void OnCollide(Collider2D col) 
    {
        if (col.tag == "Fighter")
        {
            if (col.name == "Player")
            {
                return;
            }

            Damage dmg = new Damage
            {
                damageAmount = blockPoint[shieldLevel],
                origin = transform.position,
                pushForce = pushForce[shieldLevel],
            };

            if (col.name != "weapon1_0")
            {
                col.SendMessage("ReceiveDamage", dmg);
            }

            //GameManager.instance.ShowText("BLOCKED", 25, Color.white, transform.position, Vector3.zero, 0.5f);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time - lastBlock > cooldown)
            {
                lastBlock = Time.time;
                anim.SetTrigger("Block");
            }        
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            anim.SetTrigger("holdBlock");
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            anim.SetTrigger("Retract");
        }
        else
        {
            anim.SetTrigger("Idle");
        }
    }

}
