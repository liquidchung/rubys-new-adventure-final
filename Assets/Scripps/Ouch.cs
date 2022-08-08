using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouch : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        RubyControllaByDrake controller = other.GetComponent<RubyControllaByDrake>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

}
