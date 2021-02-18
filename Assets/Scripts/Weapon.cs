using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.5f };

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer; 

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start() {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void OnCollide(Collider2D col) {
        if (col.tag == "Fighter") {
            if (col.name == "Player") {
               return; 
            }

            //create new damage obj, then send to fighter hit
            Damage dmg = new Damage {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
            };
            if (col.name != "shield_0")
            {
                col.SendMessage("ReceiveDamage", dmg);
            }
     
        }      
    }

    protected override void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)) {
            if(Time.time - lastSwing > cooldown) {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void Swing() {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon() {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        //change stats
    }

    public void SetWeaponLevel(int level) {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

}
