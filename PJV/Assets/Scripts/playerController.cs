using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int speed = 20;
    public int turnSpeed;
    public float horizontalInput;

    public int SnoreRadius;
    public int SmallSnoreDamage;
    public int BigSnoreDamage;
    public int SnoreCooldown;
    public AudioSource PlayerAudio;
    public AudioClip SmallSnore;
    public AudioClip BigSnore;
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

    void Start()
    {
        PlayerAudio.clip = SmallSnore;
        Counting = false;
        CooldownTime = Time.time;
        PressTime = 0;
    }


    void Update() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward* Time.deltaTime *speed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

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

        if(InCooldown == false)
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
    }

    void UpdateRadius()
    {
        SnoreAttack.radius = SnoreRadius;
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
       // Enemyhandler.GetComponent<enemySpawner>().UpdateEnemies();

        Collider[] EnemiesInRange = Physics.OverlapSphere(this.transform.position, SnoreRadius, Enemies);
        foreach (var hitCollider in EnemiesInRange)
        {
            hitCollider.GetComponentInParent<EnemyScript>().TakeDamage(BigSnoreDamage);

        }
    }
}
