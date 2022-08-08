using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULLETBULLET : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EVIL e = other.collider.GetComponent<EVIL>();
        if (e != null)
        {
            e.Fix();
        }

        Drone d = other.collider.GetComponent<Drone>();
        if (d != null)
        {
            d.Fix();
        }

        bigbothead player = other.gameObject.GetComponent<bigbothead>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

}
