using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public bool vertical;


    Rigidbody2D rigidbody2D;
    float timer;
    bool broken = true;
    public GameObject lootDrop;

    //
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject blicky;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        //
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }



    void Update()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }


        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && (Vector2.Distance(transform.position, player.position) > retreatDistance))
        {
            transform.position = this.transform.position;
        }

        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(blicky, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }

        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        RubyControllaByDrake player = other.gameObject.GetComponent<RubyControllaByDrake>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }


    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        Debug.Log("FIXED");
        broken = false;
        rigidbody2D.simulated = false;

        Instantiate(lootDrop, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }
}
