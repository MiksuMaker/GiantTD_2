using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    // References:
    // Target health
    HealthScript targetHealth;
    EnemyPathfinder targeting;



    private bool isAggroed;
    private bool currentlyAttacking = false;

    // Stats
    [SerializeField] int damage = 2;    // <- Default amount (for now)
    private float aggroTimer = 0.3f;
    [SerializeField] float attackTimer = 1.5f;

    private void Start()
    {
        //StartCoroutine(CheckIfAggro());   // Aggro is provoked by other scripts
        targeting = GetComponent<EnemyPathfinder>();
    }

    public void ToggleAggroOn(GameObject _provoker)     // called by Tower script on detection
    {
        if (!isAggroed)     // This check is to limit unnecessary targeting
        {
            // "Lock-on" onto the _provoker
            isAggroed = true;

            // Give Pathfinding the Target GameObject
            targeting.AggroTheEnemy(_provoker);
        }
    }

    public bool GetAggroState()
    {
        return isAggroed;
    }


    IEnumerator CheckIfAggro()
    {
        yield return new WaitForSeconds(aggroTimer);

        if (isAggroed)
        {
            // This might be done already from the Pathfinder Script
        }
    }

    public void StartAttackingCoroutine(GameObject _target) // This is bcs Coroutines can't be public?
    {
        if (!currentlyAttacking)
        {
            StartCoroutine(AttackTarget(_target));
        }
    }

    IEnumerator AttackTarget(GameObject _target)
    {
        currentlyAttacking = true;
        while (isAggroed)
        {


            // Check if target is still existing
            if (!_target)
            {
                TargetDestroyed();
                break;
            }


            //// Get the Health Component
            //if (!targetHealth)
            //{
            //    _target.GetComponent<HealthScript>();
            //}


            // Do the attack Animation

            //// Deal Damage
            //targetHealth.DealDamage(damage);


            // above the proper function
            // Below is for testing

            _target.GetComponent<HealthScript>().DealDamage(damage);    // TEST


            yield return new WaitForSeconds(attackTimer);
        }
    }

    public void TargetDestroyed()
    {
        //Restart the health target
        targetHealth = null;
        // Deaggro
        isAggroed = false;
        currentlyAttacking = false;
    }


}
