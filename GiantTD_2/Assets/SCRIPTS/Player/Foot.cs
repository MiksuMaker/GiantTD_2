using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public int stompDamage = 50;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Enemy")
        //{
        //    Debug.Log("Stepped on an enemy");
        //    Destroy(other.gameObject);
        //}

        if(other.GetComponent<HealthScript>())
        {
            Debug.Log("You got stepped on");
            other.GetComponent<HealthScript>().DealDamage(stompDamage);
        }
    }
}
