using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint = 100;
    public int maxHitpoint = 100;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    public bool isDead = false;

    //push
    protected Vector3 pushDirection;

    //all fighters can receive dmg / die
    protected virtual void ReceiveDamage(Damage dmg) {
        if (Time.time - lastImmune > immuneTime) {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            if (dmg.damageAmount != 0)
            {
                GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 30, 0.5f);
            }
           
            if (hitpoint <= 0) {
                hitpoint = 0;
                Death();
                isDead = true;
            }
        }


    }

    protected virtual void Death() {

    }
}
