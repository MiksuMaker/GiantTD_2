using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Universal Stats:
    [SerializeField] int health = 100;
    [SerializeField] int damageOutput = 1;

    //Attack Stats:
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject attackPoint;
    [SerializeField] float detectionRange = 2f;
    float detectionTimer = 0.2f;
    bool permissionToAttack = false;
    bool firing = false;
    [SerializeField] float firingCycleTimer = 2f;

    // Targeting and Detection:
    //private GameObject[] allEnemies;
    List<GameObject> detectedEnemies = new List<GameObject>();
    //[SerializeField] Collider[] detectedEnemies;
    [SerializeField] GameObject currentLockedOnEnemy = null;

    // References:
    HealthScript healthScript;
    LayerMask targetMask;

    // Animator


    void Start()
    {
        // Set Health
        StartCoroutine(Setter());

        // Start Detection
        StartCoroutine(EnemyDetection());

        //InvokeRepeating("ListAllEnemies", 1f, 10f);   // This might be necessary to cull unnecessary colliders

        // Set target Mask
        targetMask = LayerMask.GetMask("Enemy");

        // Set the AttackPoint
    }

    //private void ListAllEnemies()
    //{
    //    var list = FindObjectsOfType<EnemyAttacker>();
    //    for (int i = 0; i < list.Length; i++)
    //    {

    //    }
    //}

    IEnumerator Setter()    // Guarantees that other scripts have enough time to be set up
    {
        bool startUpDone = false;
        while (!startUpDone)
        {
            yield return new WaitForEndOfFrame();

            healthScript = gameObject.GetComponent<HealthScript>();
            if (healthScript)
            {
                healthScript.SetHealth(health);
                startUpDone = true; // Quits the Coroutine
            }
            else { Debug.LogError("Missing HealthScript Component!"); }
        }
    }


    //void Update()
    //{
    // Check for enemies
    //
    // if ( see Enemies )
    // // Attack Them
    // // Alert The Enemies (aggro them)*        

    //// Might be more optimized to have the Towers Check their surroundings rather than the enemies
    //// (Or have invisible navmesh agents that control the Enemy targeting?)
    ///

    //}

    //IEnumerator EnemyDetection()          // OLD WAY TO DO IT
    //{
    //    while (true)
    //    {
    //        //Reset the list
    //        detectedEnemies = null;
    //        yield return new WaitForSeconds(detectionTimer);

    //        detectedEnemies = Physics.OverlapSphere(transform.position, detectionRange, targetMask);
    //        //Collider[] _colliders = Physics.OverlapSphere(transform.position, detectionRange);
    //        //foreach (var hitCollider in _colliders)
    //        foreach (var hitCollider in detectedEnemies)
    //        {
    //            //hitCollider.gameObject.GetComponent<Enemy>().AggroOnSight();
    //            Debug.Log("Detected Enemy: " + hitCollider.gameObject.name);
    //        }

    //    }
    //}

    IEnumerator EnemyDetection()
    //void FindVisibleTargets()
    {
        while (true)
        {

            //Reset
            detectedEnemies.Clear();

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, detectionRange, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                GameObject enemy = targetsInViewRadius[i].gameObject;

                detectedEnemies.Add(enemy);
                //Debug.Log("Detected Enemy: " + enemy.name);
            }

            yield return new WaitForSeconds(detectionTimer);
            AttemptProvokingEnemies();

            AttackIfEngaged();
        }

    }

    private void AttemptProvokingEnemies()
    {
        for (int i = 0; i < detectedEnemies.Count; i++)
        {
            if (detectedEnemies[i] && detectedEnemies[i].CompareTag("Enemy"))
            {
                // Aggro the enemy  (attempt anyways)
                detectedEnemies[i].GetComponent<EnemyAttacker>().ToggleAggroOn(gameObject);

            }
        }
        //Debug.Log("Detected " + detectedEnemies.Count + " Enemies!");
    }




    // Attack Script
    //{
    //      // Check that the targetted enemy is still on sight OR alive
    //      
    //      // Attack the targeted enemy      
    //}
    private void AttackIfEngaged()
    {

        // if ( ! current_locked_target )
        //
        //      if ( enemy on sight )
        //           Lock-on and start attacking
        //      else
        //          clear current_locked_target, and idle
        //
        // else
        //      Attack( current_locked_target )

        if (!currentLockedOnEnemy)
        {
            firing = false; // <- Stops the firing Coroutine

            //if (detectedEnemies[0] != null)
            if (detectedEnemies.Count != 0)
            {
                // If no current enemy, target the first one
                currentLockedOnEnemy = detectedEnemies[0];
                permissionToAttack = true;
            }
            else
            {
                // Clear current target if NO enemies in sight
                currentLockedOnEnemy = null;
                permissionToAttack = false;
            }
        }
        else    // -> ATTACK
        {
            // Check if currently firing
            if (!firing && permissionToAttack)
            {
                // Start firing Coroutine
                StartCoroutine(FireProjectiles());
            }

        }
    }

    IEnumerator FireProjectiles()
    {
        firing = true;
        while (permissionToAttack)
        {

            yield return new WaitForSeconds(firingCycleTimer);

            if (!currentLockedOnEnemy) break;

            // Shoot a projectile!
            GameObject _projectile = Instantiate(projectile,
                                                attackPoint.transform.position,
                                                Quaternion.identity) as GameObject;

            // Pass target information to the projectile
            //
            // _projectile.GetComponent<ProjectileScript>().SetTarget();
            _projectile.GetComponent<ProjectileScript>().SetUpProjectile(currentLockedOnEnemy,
                                                                        attackPoint.transform.position,
                                                                        damageOutput);
        }
    }



    // Take Damage
    // -> Enemies, projectiles, and the soles of the Giant
    // trigger this

    public void TakeDamage(int _damageAmount)
    {
        healthScript.DealDamage(_damageAmount);
    }

    // Die
    public void DoDeathAnimation() // Called from HealthScript
    {
        // Do the death animation
        Debug.Log("Tower has been destroyed!");

        // Deploy related particle effects
    }
}
