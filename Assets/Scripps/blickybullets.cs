using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blickybullets : MonoBehaviour
{
    float speed = 3.0f;
    public float displayTime = 5.0f;
    float timerDisplay;
    private Transform player;
    private Vector2 target;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        timerDisplay = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timerDisplay -= Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);


        if (timerDisplay < 0)
        {
            Destroy(gameObject);
        }

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyControllaByDrake player = other.gameObject.GetComponent<RubyControllaByDrake>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }

        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
