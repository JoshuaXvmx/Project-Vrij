using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public int damage;
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
