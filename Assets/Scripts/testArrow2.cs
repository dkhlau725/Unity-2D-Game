using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testArrow2 : BowArrow
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    public int arrowLvl = 0;

     void Start()
    {
        playerTransform = GameManager.instance.player.transform;
        rb = this.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(-arrowSpeed[arrowLvl], 0);

    }

     void Update()
    {
        if (Vector3.Distance(playerTransform.position, this.transform.position) > 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Player")
        {
            if (other.tag == "Fighter")
            {
                Damage dmg = new Damage
                {
                    damageAmount = damagePoint[arrowLvl],
                    origin = transform.position,
                    pushForce = 0,
                };
                other.SendMessage("ReceiveDamage", dmg);
                Destroy(gameObject);
            }


        }
    }
}
