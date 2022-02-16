using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Universal Stats:
    [SerializeField] int health = 20;
    [SerializeField] int damageOutput;

    //Attack Stats:
    [SerializeField] float detectionRange = 2f;
    float detectionTimer = 0.2f;
    [SerializeField] Collider[] detectedEnemies;
    [SerializeField] GameObject lockedOnEnemy;

    // References:
    HealthScript healthScript;
    // Animator


    void Start()
    {
        // Set Health
        StartCoroutine(Setter());
    }

    IEnumerator Setter()    // Guarantees that other scripts have enough time to be set up
    {
        bool startUpDone = false;
        while(!startUpDone)
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


    void Update()
    {
        // Check for enemies
        //
        // if ( see Enemies )
        // // Attack Them
        // // Alert The Enemies (aggro them)*        

        //// Might be more optimized to have the Towers Check their surroundings rather than the enemies
        //// (Or have invisible navmesh agents that control the Enemy targeting?)
        



    }

    IEnumerator EnemyDetection()
    {
        yield return new WaitForSeconds(detectionTimer);

        detectedEnemies = Physics.OverlapSphere(transform.position, detectionRange);
        //Collider[] _colliders = Physics.OverlapSphere(transform.position, detectionRange);
        //foreach (var hitCollider in _colliders)
        foreach (var hitCollider in detectedEnemies)
        {
            //hitCollider.gameObject.GetComponent<Enemy>().AggroOnSight();
            Debug.Log("Detected Enemy!");
        }
    }

    private bool CheckForEnemies()
    {
        for (int i = 0; i < detectedEnemies.Length; i++)
        {
            if (detectedEnemies[i].CompareTag("Enemy"))
            {
                // Aggro the enemy  (attempt anyways)
                detectedEnemies[i].GetComponent<EnemyAttacker>().ToggleAggroOn(gameObject);

                // Lock-on and start attacking
            }
        }

        return true;
    }


    // Attack Script


    // Take Damage
    // -> Enemies, projectiles, and the soles of the Giant
    // trigger this
    public void TakeDamage(int _damageAmount)
    {

    }

    // Die
    public void DoDeathAnimation() // Called from HealthScript
    {
        // Do the death animation
    }
}
