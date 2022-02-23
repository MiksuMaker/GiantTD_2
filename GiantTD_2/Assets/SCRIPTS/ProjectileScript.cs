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
	public float arcHeight = 0.2f;

    //[SerializeField] private AnimationCurve _flightCurve;
    //private float _traveled = 0;

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
        //// 2D ARCING (WITHOUT THE Z -AXIS)
        //// Compute the next position, with arc added in
        //float x0 = startPos.x;
        //float x1 = targetPos.x;
        //float dist = x1 - x0;
        //float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        //float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        //float arc = this.arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        //nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        //// Rotate to face the next position, and then move there
        ////transform.rotation = LookAt(nextPos - transform.position);
        //transform.LookAt(nextPos);
        //transform.position = nextPos;
        ////transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //// Do something when we reach the target
        //if (nextPos == targetPos) Arrived();
        ////if (nextPos == new Vector3(targetPos.x, 0, targetPos.z)) Arrived();


        //Vector3 x0 = startPos;
        //Vector3 x1 = targetPos;
        //Vector3 dist = x1 - x0;
        //      float nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
        //      float nextZ = Mathf.MoveTowards(transform.position.z, targetPos.z, speed * Time.deltaTime);

        //float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0))




        //// Compute the next position, with arc added in
        //float x0 = startPos.x;
        //float x1 = targetPos.x;
        //float dist = x1 - x0;
        //float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        //float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        //float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        //nextPos = new Vector3(nextX, baseY + arc, transform.position.z);


        //// Rotate to face the next position, and then move there
        //transform.rotation = LookAt2D(nextPos - transform.position);
        //transform.position = nextPos;

        //// Do something when we reach the target
        //if (nextPos == targetPos) Arrived();


        //// DIFFERENT SOURCE; ANIMATION CURVE
        //_traveled += Time.deltaTime * speed;
        //float arcHeight = _flightCurve.Evaluate(_traveled);
        //Vector3 originWithHeight = new Vector3(startPos.x, startPos.y + arcHeight, startPos.z);
        //Vector3 targetWithHeight = new Vector3(targetPos.x, targetPos.y + arcHeight, startPos.z);
        //transform.position = Vector3.Lerp(originWithHeight, targetWithHeight, _traveled);







		// Compute the next position -- straight flight
		Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Compute the next position with an arc
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float z0 = startPos.z;
        float z1 = targetPos.z;

        float common0 = Mathf.Sqrt((x0 * x0) + (z0 * z0));
        float common1 = Mathf.Sqrt((x1 * x1) + (z1 * z1));
        //float dist = Vector3.Distance(startPos, targetPos);
        float distX = x1 - x0;
        float distZ = z1 - z0;
        float distC = Mathf.Sqrt((distX * distX) + (distZ * distZ));

        // Speeds
        float Xspeed = speed * Mathf.Abs(distX / distZ);
        float Zspeed = speed * Mathf.Abs(distZ / distX);
        //float Xspeed = Mathf.Abs(distX / distZ);
        //float Zspeed = Mathf.Abs(distZ / distX);
        //Debug.Log("X speed is: " + Xspeed);
        //Debug.Log("Z speed is: " + Zspeed);

        // Position modifiers
        //float Xmod = (distX / distZ);
        //float Zmod = (distZ / distX);


        //float next_X_Pos = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);               // <----- NORMAALI
        //float next_Z_Pos = Mathf.MoveTowards(transform.position.z, z1, speed * Time.deltaTime);
        //float nextCommonPos = Mathf.Sqrt((next_X_Pos * next_X_Pos) + (next_Z_Pos * next_Z_Pos));

        float next_X_Pos = Mathf.MoveTowards(transform.position.x, x1, Xspeed * Time.deltaTime);              // <----- TOIMII
        float next_Z_Pos = Mathf.MoveTowards(transform.position.z, z1, speed * Time.deltaTime);                       //... tosin nuolen nopeus vaihtelee
        float nextCommonPos = Mathf.Sqrt((next_X_Pos * next_X_Pos) + (next_Z_Pos * next_Z_Pos));

        //float next_X_Pos = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        //float next_Z_Pos = Mathf.MoveTowards(transform.position.z, z1, speed * Time.deltaTime);
        //float nextCommonPos = Mathf.Sqrt((next_X_Pos * next_X_Pos) + (next_Z_Pos * next_Z_Pos));

        // DEBUGGERS
        //Debug.Log("Next X Pos: " + next_X_Pos);


        float differenceBetweenNextAndStart = Vector3.Distance(new Vector3(next_X_Pos, targetPos.y, next_Z_Pos),
                                                                startPos);

        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (/*(nextPos - startPos) /*/ differenceBetweenNextAndStart)
                                                                                                            / distC);

        float arc = arcHeight * (nextCommonPos - common0) * (nextCommonPos - common1) / (-0.25f * distC * distC);


        // Calculate the distance between start x and target x
        Debug.Log("X Answer is: " + Mathf.Abs(distX / distZ));
        Debug.Log("Z Answer is: " + Mathf.Abs(distZ / distX));
        //Debug.Log("X Position is: " + next_X_Pos * Mathf.Abs(distX / distZ));
        //Debug.Log("Z Position is: " + next_Z_Pos * Mathf.Abs(distZ / distX));
        //Debug.Log("X Position is: " + next_X_Pos);
        //Debug.Log("Z Position is: " + next_Z_Pos);
        //Debug.Log("X Relative Speed is: " + Mathf.Abs(distX / distZ) * speed);
        //Debug.Log("Z Relative Speed is: " + Mathf.Abs(distZ / distX) * speed);


        nextPos = new Vector3(next_X_Pos, baseY + arc, next_Z_Pos);



        // Rotate to face the next position, and then move there
        transform.LookAt(nextPos);
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

}
