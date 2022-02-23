using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ProjectileScript_2 : MonoBehaviour
{
    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;
    private GameObject targetObject;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 6;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 0.2f;


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

    void Update()
    {

        // Compute the next position -- straight flight
        Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

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
