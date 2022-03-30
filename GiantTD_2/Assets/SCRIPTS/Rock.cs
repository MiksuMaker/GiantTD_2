using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int rockDamage = 10000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("boom");
            other.gameObject.GetComponent<HealthScript>().DealDamage(rockDamage);
            Debug.Log("wat");
        }
    }
}
