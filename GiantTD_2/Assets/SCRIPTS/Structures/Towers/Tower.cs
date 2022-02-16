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

    // Targeting and Detection:
    //private GameObject[] allEnemies;
    List<GameObject> detectedEnemies = new List<GameObject>();
    //[SerializeField] Collider[] detectedEnemies;
    [SerializeField] GameObject lockedOnEnemy;

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
            CheckForEnemies();
        }

    }

    private bool CheckForEnemies()
    {
        for (int i = 0; i < detectedEnemies.Count; i++)
        {
            if (detectedEnemies[i].CompareTag("Enemy"))
            {
                Debug.Log("Detected " + detectedEnemies.Count + "Enemies!");
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
