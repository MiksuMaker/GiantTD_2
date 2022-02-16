using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Path_Manager : MonoBehaviour
{
    //Test_Target[] targetList = new Test_Target [2];
    //Transform[] targetPos = new Transform [2]; 

    [SerializeField] GameObject[] targetList;
    Test_Target[] tempList;
    int listLength;

    void Awake()
    {
        //targetList = FindObjectsOfType<Test_Target>();
        //for (int i = 0; i < targetList.Length; i++)
        //{
        //    targetPos[i] = targetList[i].GetTransform();
        //}


        Debug.Log("MANAGER Starting up!");


        // Find all targets
        listLength = FindObjectsOfType<Test_Target>().Length;



        // Create a list based on length
        targetList = new GameObject[listLength];
        tempList = new Test_Target[listLength];

        // All all targets to that list
        tempList = FindObjectsOfType<Test_Target>();
        for (int i = 0; i < listLength; i++)
        {
            targetList[i] = tempList[i].gameObject;
        }

    }

    public GameObject GetNextTarget()
    {
        Debug.Log("Getting Next Target!");

        // Simple version, that just gives a random member from the list
        return targetList[Random.Range(0, listLength)];
    }
}
