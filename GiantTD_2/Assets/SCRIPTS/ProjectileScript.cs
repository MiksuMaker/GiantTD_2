using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ProjectileScript : MonoBehaviour
{
	[Tooltip("Position we want to hit")]
	public Vector3 targetPos;
	private GameObject targetObject;

	[Tooltip("Horizontal speed, in units/sec")]
	public float speed = 6;

	[Tooltip("How high the arc should be, in units")]
	public float arcHeight = 0.5f;

	float Animation;

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



		//// Compute the next position with an arc
		//      float x0 = startPos.x;
		//      float x1 = targetPos.x;
		//      float z0 = startPos.z;
		//      float z1 = targetPos.z;


		//float common0 = Mathf.Sqrt((x0 * x0)+(z0 * z0));
		//float common1 = Mathf.Sqrt((x1 * x1)+(z1 * z1));
		////float dist = Vector3.Distance(startPos, targetPos);
		//float distX = x1 - x0;
		//float distZ = z1 - z0;
		//float distC = Mathf.Sqrt((distX * distX) + (distZ * distZ));

		//float next_X_Pos = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
		//float next_Z_Pos = Mathf.MoveTowards(transform.position.z, z1, speed * Time.deltaTime);

		//float nextCommonPos = Mathf.Sqrt((next_X_Pos * next_X_Pos) + (next_Z_Pos * next_Z_Pos));


		//float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextCommonPos - common0) / distC);
		//float arc = arcHeight * (nextCommonPos - common0) * (nextCommonPos - common1) / (-0.25f * distC * distC);

		//nextPos = new Vector3(next_X_Pos, baseY + arc, next_Z_Pos);




		// Compute the next position -- straight flight
		Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);



		// Rotate to face the next position, and then move there
		transform.position = nextPos;
        transform.LookAt(targetPos);

        // Do something when we reach the target
        if (nextPos == targetPos) Arrived();





		//// My go at creating a fiiiine curve

		//// Have the arrow lerp up unitl the halfway point, then start heading to target y pos

		//Vector3 StartPos_WithoutY = new Vector3(startPos.x, 0, startPos.z);
		//Vector3 TargetPos_WithoutY = new Vector3(targetPos.x, 0, targetPos.z);

		//Vector3 Halfway = StartPos_WithoutY - TargetPos_WithoutY;
		//Vector3 HeightStartPos = new Vector3(0, startPos.y, 0);
		//Vector3 HeightMaxPos = new Vector3(0, startPos.y + arcHeight, 0);
		//Vector3 HeightTargetPos = new Vector3(0, startPos.y, 0);

		//// Calculate the next position

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

}
