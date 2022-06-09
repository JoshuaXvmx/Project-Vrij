using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int speed = 20;
    public int turnSpeed;
    public float horizontalInput;

    public int SnoreRadius;
    public int SnoreDamage;
    public int SnoreCooldown;
    private SphereCollider SnoreAttack;

    public GameObject Enemyhandler;
 


    public LayerMask Enemies;

    private List<GameObject> EnemiesInrange = new List<GameObject>();

    IEnumerator DamageRoutine()
    {
        Snore();

        yield return new WaitForSeconds(SnoreCooldown); //amount of seconds to wait before next damage
    }

    void Start()
    {
        UpdateRadius();
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward* Time.deltaTime *speed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DamageRoutine());

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(DamageRoutine());
        }
    }

    void UpdateRadius()
    {
        SnoreAttack.radius = SnoreRadius;
    }

    void Snore()
    {
        Enemyhandler.GetComponent<enemySpawner>().UpdateEnemies();

        Collider[] EnemiesInRange = Physics.OverlapSphere(this.transform.position, SnoreRadius, Enemies);
        foreach (var hitCollider in EnemiesInRange)
        {
            hitCollider.GetComponentInParent<EnemyScript>().TakeDamage(SnoreDamage);

        }
    }
}
