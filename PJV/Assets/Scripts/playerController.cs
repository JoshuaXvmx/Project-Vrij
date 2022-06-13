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
    private float CooldownTime;
    public float CooldownLength;
    private bool Counting;

    void Start()
    {
        PlayerAudio.clip = SmallSnore;
        Counting = false;
        CooldownTime = Time.time;
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
            if (InCooldown != true)
            {
                if (Counting == false)
                {
                    StartTime = Time.time;
                    Counting = true;
                }
                
                

                if(Input.GetKeyDown(KeyCode.Space) && Time.time - StartTime >= 2f)   //This one doesnt work yet
                {
                    Debug.Log("BigSnore");
                    PlayerAudio.clip = BigSnore;
                    BigSnoreAttack();
                    CooldownTime = Time.time;
                    Counting = false;
                    InCooldown = true;
                }
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(InCooldown != true)
            {
                Counting = false;
                PlayerAudio.clip = SmallSnore;
                SmallSnoreAttack();
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
        Enemyhandler.GetComponent<enemySpawner>().UpdateEnemies();

        Collider[] EnemiesInRange = Physics.OverlapSphere(this.transform.position, SnoreRadius, Enemies);
        foreach (var hitCollider in EnemiesInRange)
        {
            hitCollider.GetComponentInParent<EnemyScript>().TakeDamage(SmallSnoreDamage);

        }
    }

    void BigSnoreAttack()
    {
        PlayerAudio.Play();
        Enemyhandler.GetComponent<enemySpawner>().UpdateEnemies();

        Collider[] EnemiesInRange = Physics.OverlapSphere(this.transform.position, SnoreRadius, Enemies);
        foreach (var hitCollider in EnemiesInRange)
        {
            hitCollider.GetComponentInParent<EnemyScript>().TakeDamage(BigSnoreDamage);

        }
    }
}
