﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string message;

    private float cooldown = 1.5f;
    private float lastShout = -1.5f;

    protected override void OnCollide(Collider2D col) {
        if (Time.time - lastShout > cooldown) {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0,0.16f,0), Vector3.zero, cooldown);
        }
        
    }
}
