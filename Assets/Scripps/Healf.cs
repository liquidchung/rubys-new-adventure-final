using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healf : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyControllaByDrake controller = other.GetComponent<RubyControllaByDrake>();

        if (controller != null)
        {
            if (controller.currentHealth < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);

                controller.PlaySound(collectedClip);

            }
        }
    }

}