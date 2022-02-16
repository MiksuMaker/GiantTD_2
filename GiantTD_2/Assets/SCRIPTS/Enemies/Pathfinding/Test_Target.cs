using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Target : MonoBehaviour
{
    public Transform transformReference;
    private void Awake()
    {

        Debug.Log("TARGET Starting up!");
        transformReference = gameObject.transform;
    }

    public Transform GetTransform()
    {
        return transformReference;
    }
}
