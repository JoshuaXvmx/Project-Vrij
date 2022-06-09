using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public int speed; //shoudl slow down when being snored on, maybe merge this script and followerscript?

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Hit, health =" + health.ToString());
    }
}
