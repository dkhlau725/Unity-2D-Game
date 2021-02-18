using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public Boss boss;
    public GameObject potion;
    public Transform bossPosition;
    public bool bossDefeated = false;

    private Transform trans;

    public int generateNumber()
    {
        var r = new System.Random();
        int randomInt = r.Next(0, 100);
        return randomInt;
    }

    void FixedUpdate()
    {
        if (bossDefeated == false && boss.hitpoint == 0)
        {
            bossDefeated = true;
            //trans = bossPosition;
            dropLoot();
        }
    }

    public void dropLoot()
    {
        int dropChance = generateNumber();
        UnityEngine.Debug.Log(dropChance);
        if (dropChance > 20)
        {
            GameObject a = Instantiate(potion) as GameObject;
            //a.transform.position = trans.transform.position;
        }
    }
}
