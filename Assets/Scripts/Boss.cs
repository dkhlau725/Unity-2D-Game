﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Boss : Enemy
{
    public float[] fireballSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public Transform[] fireballs;

    //public RectTransform bossHitpointBar;
    //public Boss boss;

    private void Update() {
        for (int i = 0; i < fireballs.Length; i++) {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
    }
    protected override void ReceiveDamage(Damage dmg) {
        base.ReceiveDamage(dmg);
        float ratio = (float)this.hitpoint / (float)this.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    } 

}
