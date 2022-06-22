using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public int speed; //shoudl slow down when being snored on, maybe merge this script and followerscript?
    public AudioSource EnemySource;
    public AudioClip HitSound;

    private void Start()
    {
        EnemySource.clip = HitSound;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        EnemySource.Play();
    }

    private void Update()
    {
        if (health <= 0)
        {
            enemySpawner.AmountofKills++;
            Destroy(this.gameObject);
        }
    }
}
