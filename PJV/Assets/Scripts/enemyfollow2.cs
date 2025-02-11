using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyfollow2 : MonoBehaviour
{
	public float lookRadius = 10f;  // Detection range for player

	Transform target;   // Reference to the player
	NavMeshAgent agent; // Reference to the NavMeshAgent

	void Start()
	{
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		// Distance to the target
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the lookRadius
		if (distance <= lookRadius)
		{
			// Move towards the target
			agent.SetDestination(target.position);

			// If within attacking distance
			if (distance <= agent.stoppingDistance)
			{
				// Attack the target
				FaceTarget();   // Make sure to face towards the target
			}
		}
	}

	// Rotate to face the target
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	// Show the lookRadius in editor
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
