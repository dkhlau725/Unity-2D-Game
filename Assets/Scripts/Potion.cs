using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Collectable
{
    public int healingAmount = 20;

    protected override void OnCollect()
    {
        if (!collected)
        {
            if (GameManager.instance.player.hitpoint == GameManager.instance.player.maxHitpoint)
            {
                GameManager.instance.ShowText("Health Full!", 20, Color.red, transform.position, Vector3.up * 50, 3.0f);
            }
            else if (GameManager.instance.player.hitpoint < GameManager.instance.player.maxHitpoint)
            {
                collected = true;
                GameManager.instance.player.Heal(healingAmount);          
                Destroy(gameObject);
            }
        }
    }
}
