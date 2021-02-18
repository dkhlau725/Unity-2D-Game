using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveArrow : Collectable
{
    public int arrowAmount = 2;
    public BowArrow bow;

    protected override void OnCollect()
    {
        if (!collected)
        {
            if (bow.arrowCount == bow.arrowCapacity)
            {
                GameManager.instance.ShowText("Ammo Full!", 20, Color.red, transform.position, Vector3.up * 50, 3.0f);
            }

            else if (bow.arrowCount == bow.arrowCapacity - arrowAmount)
            {
                collected = true;
                bow.arrowCount = bow.arrowCapacity;
                GameManager.instance.ShowText("Ammo Refilled!", 20, Color.green, transform.position, Vector3.up * 50, 3.0f);
                Destroy(gameObject);
                GameManager.instance.arrowCounter.text = bow.arrowCount.ToString() + " / " + bow.arrowCapacity.ToString();
            }

            else if (bow.arrowCount < bow.arrowCapacity)
            {
                collected = true;
                bow.arrowCount += arrowAmount;
                if (bow.arrowCount > bow.arrowCapacity)
                {
                    bow.arrowCount = bow.arrowCapacity;
                    GameManager.instance.ShowText("Ammo Refilled!", 20, Color.green, transform.position, Vector3.up * 50, 3.0f);
                }
                else
                {
                    GameManager.instance.ShowText("+" + arrowAmount.ToString() + " arrows", 20, Color.green, transform.position, Vector3.up * 50, 3.0f);
                }
                Destroy(gameObject);
                GameManager.instance.arrowCounter.text = bow.arrowCount.ToString() + " / " + bow.arrowCapacity.ToString();
            }

        }

    }
}
