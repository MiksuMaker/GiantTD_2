using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ProjectileScript_2 : MonoBehaviour
{
    //Animator animator;
    [SerializeField] Animator animator;

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
        //animator = GetComponent<Animator>();
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


        // Set animation speed
        SetAnimationSpeed(CalculateDistance());


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

    float CalculateDistance()
    {
        float dist = Vector3.Distance(targetPos, transform.position);
        return dist;
    }

    void SetAnimationSpeed(float _dist)
    {
        if (_dist >= 1)
        {
            animator.speed = 1.5f / _dist;
        }
        else
        {
            animator.speed = (1 * _dist);
        }

        //animator.speed = _speed;
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
