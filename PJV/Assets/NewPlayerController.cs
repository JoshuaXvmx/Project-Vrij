using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class NewPlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmootVelocity;

    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    bool onGround;

    public int SnoreRadius;
    public int SmallSnoreDamage;
    public int BigSnoreDamage;
    public int SnoreCooldown;
    public AudioSource PlayerAudio;
    public AudioClip SmallSnore;
    public AudioClip BigSnore;
    public AudioClip HitSound;
    private SphereCollider SnoreAttack;

    public GameObject Enemyhandler;
    public LayerMask Enemies;

    private List<GameObject> EnemiesInrange = new List<GameObject>();

    private bool InCooldown;
    private float StartTime;
    public float CooldownTime;
    public float CooldownLength;
    private bool Counting;
    private bool ReadyForBigSnore;
    public float PressTime;
    public bool showBar;

    public int health;
    public float Hitcooldown;
    private float HitTime;
    private bool inHitCooldown;

    public GameObject LoseMenu;
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI EnemiesBeaten;

    void Start()
    {
        PlayerAudio.clip = SmallSnore;
        Counting = false;
        CooldownTime = Time.time;
        PressTime = 0;
    }

    private void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmootVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            controller.Move(direction * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Time.time - CooldownTime >= CooldownLength)
        {
            InCooldown = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressTime = Time.time;
            PlayerAudio.clip = SmallSnore;
            if (InCooldown == false)
            {
                Debug.Log("cooldown = false, should work");
                showBar = true;
                if (Counting == false)
                {
                    StartTime = Time.time;
                    Counting = true;
                }

            }

        }

        if (InCooldown == false)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time - StartTime >= 1.5f)   //This is the time needed 
            {
                ReadyForBigSnore = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            showBar = false;
            if (ReadyForBigSnore == false)
            {
                Counting = false;
                PlayerAudio.clip = SmallSnore;
                SmallSnoreAttack();
            }
            else
            {
                PlayerAudio.clip = BigSnore;
                BigSnoreAttack();
                CooldownTime = Time.time;
                InCooldown = true;
                Counting = false;
                ReadyForBigSnore = false;
            }
        }

        if (Time.time - HitTime >= Hitcooldown)
        {
            inHitCooldown = false;
        }

        if (health <= 0)
        {
            LoseLevel();
        }
    }

    void SmallSnoreAttack()
    {
        PlayerAudio.Play();
        //Enemyhandler.GetComponent<enemySpawner>().UpdateEnemies();

        Collider[] EnemiesInRange = Physics.OverlapSphere(this.transform.position, SnoreRadius, Enemies);
        foreach (var hitCollider in EnemiesInRange)
        {
            hitCollider.GetComponentInParent<EnemyScript>().TakeDamage(SmallSnoreDamage);

        }
    }

    void BigSnoreAttack()
    {
        PlayerAudio.Play();
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        // Enemyhandler.GetComponent<enemySpawner>().UpdateEnemies();

        Collider[] EnemiesInRange = Physics.OverlapSphere(this.transform.position, SnoreRadius, Enemies);
        foreach (var hitCollider in EnemiesInRange)
        {
            hitCollider.GetComponentInParent<EnemyScript>().TakeDamage(BigSnoreDamage);

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (inHitCooldown == false)
            {
                PlayerAudio.clip = HitSound;
                PlayerAudio.Play();
                health -= collision.gameObject.GetComponent<EnemyScript>().damage;
                Debug.Log(health);
                HitTime = Time.time;
                inHitCooldown = true;
            }
        }
    }
    
   private void LoseLevel()
    {
        float EndTime = Time.time;
        Debug.Log(EndTime);
        Time.timeScale = 0;

        LoseMenu.SetActive(true);
        EnemiesBeaten.text = enemySpawner.AmountofKills.ToString();
        Timer.text = EndTime.ToString();
    }

}
