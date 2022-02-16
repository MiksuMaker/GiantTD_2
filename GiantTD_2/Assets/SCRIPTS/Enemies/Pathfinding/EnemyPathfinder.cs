using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinder : MonoBehaviour
{
    Test_Path_Manager manager;

    NavMeshAgent agent;
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
        manager = FindObjectOfType<Test_Path_Manager>();
        agent = GetComponent<NavMeshAgent>();

        Debug.Log("ENEMY Starting up!");
        RandomizeNextTarget();
        //currentTargetObject = manager.GetNextTarget();
        //currentTargetPos = currentTargetObject.transform.position;

        // Set stats
        agent.speed = agentSpeed;

        StartCoroutine(UpdatePathfinding());
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
            //if (HasArrivedToTarget())
            {
                Move();
            }
            else
            {
                RandomizeNextTarget();

            }

            // If not, move but do not update SetDestination()

            // ... -> Nothing required here?

            // Waiting period for the next cycle
            yield return new WaitForSeconds(updateTime); 

        }
    }

    private void Move()
    {
        //agent.SetDestination(GetTargetVectorPos());
        agent.SetDestination(GetTargetVectorPositionWithout_Y());
        //if (!HasArrivedToTarget())
        //{
        //    agent.SetDestination(currentTargetPos);

        //}
        //else
        //{
        //    RandomizeNextTarget();
        //}
    }



    public GameObject GetCurrentTarget()
    {
        return currentTargetObject;
    }

    //private bool HasArrivedToTarget()
    //{
    //    if (GetSelfVectorPositionWithout_Y() != GetTargetVectorPositionWithout_Y())
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        Debug.Log("Enemy has reached the target!");
    //        return true;
    //    }
    //}

    private bool IsCloseEnoughToTarget()
    {
        float _distance = Vector3.Distance(GetTargetVectorPositionWithout_Y(), GetSelfVectorPositionWithout_Y());
        if (_distance <= requiredDistance)
        {
            Debug.Log("Enemy has reached the target!");
            return true;
        }
        else
        {
            return false;
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

}
