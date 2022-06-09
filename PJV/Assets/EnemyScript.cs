using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public int speed; //shoudl slow down when being snored on, maybe merge this script and followerscript?

    private void Update()
    {
        if (health <= 0)
        {
            Despawn();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Despawn()
    {
        Destroy(this.gameObject);
    }
}
