using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RubyControllaByDrake : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    private int maxCogcount = 3;

    public GameObject projectilePrefab;

    public AudioClip throwSound;
    public AudioClip hitSound;
    public AudioClip coinSound;

    public int health { get { return currentHealth; } }
    public int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public ParticleSystem damageEffect;
    public ParticleSystem healthEffect;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    public static int coincount;
    public TextMeshProUGUI coinText;
    public static int cogcount;
    public TextMeshProUGUI cogsleft;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        cogcount = maxCogcount;

        coincount = 0;

        audioSource = GetComponent<AudioSource>();

        SetCoinText();
        SetCogsLeft();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                nonplayerchara character = hit.collider.GetComponent<nonplayerchara>();
                if (character != null)
                {
                    character.DisplayDialog();
                }

                signpost sign = hit.collider.GetComponent<signpost>();
                if (sign != null)
                {
                    sign.DisplayDialog();
                }

                

                lootbox loot = hit.collider.GetComponent<lootbox>();
                if (loot != null)
                {
                    loot.LootDrop();
                }
            }

            RaycastHit2D knock = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("boss"));
            if (knock.collider != null)
            {
                bigbothead bigheadedbwoi = knock.collider.GetComponent<bigbothead>();
                if (bigheadedbwoi != null)
                {
                    bigheadedbwoi.DisplayDialog();
                }  
            }
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("Main");
        }
        

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            damageEffect.Play();

            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

        }

        if (amount > 0)
        {
            healthEffect.Play();
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealfBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        BULLETBULLET projectile = projectileObject.GetComponent<BULLETBULLET>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        cogcount = cogcount - 1;
        SetCogsLeft();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);

            coincount = coincount + 1;
            PlaySound(coinSound);
            SetCoinText();
        }

        if (other.gameObject.CompareTag("reload"))
        {
            other.gameObject.SetActive(false);

            cogcount = cogcount + 2;

            SetCogsLeft();
        }

    }

    void SetCoinText()
    {
        coinText.text = "Coins: " + coincount.ToString() + "/4";
    }

    void SetCogsLeft()
    {
        cogsleft.text = " " + cogcount.ToString();
    }

}

