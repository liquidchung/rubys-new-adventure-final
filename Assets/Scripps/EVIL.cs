using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EVIL : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    public GameObject enemyprojectilePrefab;

    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;

    Animator animator;

    public GameObject lootDrop;

    public TextMeshProUGUI roboText;
    public static int roboCount;

    // Start is called before the first frame update
    void Start()
    {
        roboText = GameObject.Find("RobotsFixed").GetComponent<TextMeshProUGUI>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();

        roboCount = 0;
        SetRoboText();
    }



    void Update()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        RubyControllaByDrake player = other.gameObject.GetComponent<RubyControllaByDrake>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    void SetRoboText()
    {
        roboText.text = "Robots Fixed: " + roboCount.ToString() + "/4";
    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        Debug.Log("FIXED");
        broken = false;
        rigidbody2D.simulated = false;

        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        roboCount = roboCount + 1;

        SetRoboText();

        Instantiate(lootDrop, transform.position, Quaternion.identity);
    }
}