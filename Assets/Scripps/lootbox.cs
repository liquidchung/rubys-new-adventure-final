using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootbox : MonoBehaviour
{
    public GameObject loot;
    public GameObject stuff;
    public void LootDrop()
    {
        Destroy(gameObject);
        Instantiate(loot, transform.position, Quaternion.identity);
        Instantiate(stuff, transform.position, Quaternion.identity);
    }
}
