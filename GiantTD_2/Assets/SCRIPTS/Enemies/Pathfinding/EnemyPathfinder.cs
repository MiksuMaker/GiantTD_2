using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinder : MonoBehaviour
{
    // References:
    Test_Path_Manager manager;
    NavMeshAgent agent;
    EnemyAttacker attacker;



    public GameObject currentTargetObject;
    public Vector3 currentTargetPos;
    private Vector3 defaultVector = new Vector3(0, 0, 0);

    [SerializeField] float updateTime = 0.1f;
    [SerializeField] float requiredDistance = 0.2f;

    //STATS
    [Header("Stats")]
    [SerializeField] float agentSpeed = 1f;



    void Start()
    {
        //Set up references:
        manager = FindObjectOfType<Test_Path_Manager>();
        agent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<EnemyAttacker>();

        StartCoroutine(LateSetUp());

        //Debug.Log("ENEMY Starting up!");
        RandomizeNextTarget();

        // Set stats
        agent.speed = agentSpeed;

        StartCoroutine(UpdatePathfinding());
    }

    IEnumerator LateSetUp()
    {
        //makes sure all the components have time to be set up before referencing

        bool doneWithSetUp = false;
        while (!doneWithSetUp)
        {
            yield return new WaitForEndOfFrame();
            attacker = GetComponent<EnemyAttacker>();
            if (attacker)
            {
                doneWithSetUp = true;
            }
        }
    }

    private void Move()
    {
        agent.SetDestination(GetTargetVectorPositionWithout_Y());
    }

    IEnumerator UpdatePathfinding()
    {
        Move();



        while (true)
        {
            // Check if Enemy has reached target
            // If, Get New Target
            // + SetDestination()
            if (!IsCloseEnoughToTarget())
            {
                // if NOT aggroed
                Move();
                // if IS aggroed
                //attacker.StartAttackingCoroutine(currentTargetObject);
            }
            else
            {
                // if NOT aggroed
                //RandomizeNextTarget();

                // if IS aggroed
                //  -> AttackTarget()


                if (CheckAggro())   // if aggored
                {
                    attacker.StartAttackingCoroutine(currentTargetObject);
                }
                else
                {
                    RandomizeNextTarget();
                }
            }

            // If not, move but do not update SetDestination()

            // ... -> Nothing required here?

            // Waiting period for the next cycle
            yield return new WaitForSeconds(updateTime);

        }
    }

    #region Targeting

    public GameObject GetCurrentTarget()
    {
        return currentTargetObject;
    }

    private bool IsCloseEnoughToTarget()
    {
        // Check if Target exists
        if (!DoesTargetExist())
        {
            RandomizeNextTarget();
        }

        float _distance = Vector3.Distance(GetTargetVectorPositionWithout_Y(), GetSelfVectorPositionWithout_Y());
        if (_distance <= requiredDistance)
        {
            //Debug.Log("Enemy has reached the target!");
            return true;
        }
        else
        {
            return false;
        }
    }

    // Checks that Target has not been destroyed
    private bool DoesTargetExist()
    {
        if (!currentTargetObject)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void RandomizeNextTarget()
    {
        currentTargetObject = manager.GetNextTarget();
        currentTargetPos = GetTargetVectorPositionWithout_Y();
    }

    private Vector3 GetTargetVectorPositionWithout_Y()
    {
        Vector3 obj = currentTargetObject.transform.position;
        Vector3 _p = new Vector3(obj.x, 0, obj.z);

        return _p;
    }

    private Vector3 GetSelfVectorPositionWithout_Y()
    {
        Vector3 obj = gameObject.transform.position;
        Vector3 _b = new Vector3(obj.x, 0, obj.z);

        return _b;
    }

    #endregion

    // Attacking

    public void AggroTheEnemy(GameObject _target)
    {
        currentTargetObject = _target;

        // Start Checking For Attack Distance
        //
        // -> Check If Close Enough already does this

        // Toggle Aggro
    }

    private bool CheckAggro()   // Gets the Aggro state from Attacker
    {
        return attacker.GetAggroState();
    }




}
