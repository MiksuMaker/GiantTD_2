using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileScript : MonoBehaviour
{
	[Tooltip("Position we want to hit")]
	public Vector3 targetPos;
	private GameObject targetObject;

	[Tooltip("Horizontal speed, in units/sec")]
	public float speed = 10;

	Vector3 startPos;

	private int damage;

	void Start()
	{
		// Cache our start position, which is really the only thing we need
		// (in addition to our current position, and the target).
		startPos = transform.position;
	}

	public void SetUpProjectile(GameObject _target, Vector3 _startPos, int _damage)
    {
		startPos = _startPos;

		// Set target position
		targetPos = _target.transform.position;

		// Set target
		targetObject = _target;

		damage = _damage;
	}

	//public void SetTarget(GameObject _target)
	//{                       // Possibly need to change to GameObject to predict future position
	//	// Set target position
	//	targetPos = _target.transform.position;

	//	// Set target
	//	targetObject = _target;
	//}

	void Update()
	{
		// Compute the next position -- straight flight
		Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

		// Rotate to face the next position, and then move there
		//transform.rotation = LookAt2D(nextPos - transform.position); Figure out rotation later!
		transform.position = nextPos;

		// Do something when we reach the target
		if (nextPos == targetPos) Arrived();
	}

	void Arrived()
	{
		// Deal damage to the target
		if (targetObject) // ..exists
		{
			targetObject.GetComponent<HealthScript>().DealDamage(damage);
		}	

		Destroy(gameObject);
	}

	/// 
	/// This is a 2D version of Quaternion.LookAt; it returns a quaternion
	/// that makes the local +X axis point in the given forward direction.
	/// 
	/// forward direction
	/// Quaternion that rotates +X to align with forward
	//static Quaternion LookAt2D(Vector2 forward)
	//{
	//	return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	//}

	//static LookAt3D(Vector3 forward)
 //   {
	//	return Quaternion.Euler()
 //   }
}
