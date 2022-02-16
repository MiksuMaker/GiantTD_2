using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // This script is for dealing with health

    // It is universal so that all mortal things will get equally smashed to bits
    // if stepped on by the Giant. No need to check whether if was a tower or enemy.


    [SerializeField] int health = 20;   // Spawn Health, changed by other script

    public void SetHealth(int _healthAmount)
    {
        health = _healthAmount;
        Debug.Log("Health has been set for: " + gameObject.name);
    }

    public void DealDamage(int _damageAmount)
    {
        health -= _damageAmount;

        // Check if destroyed
        if (health <= 0)
        {
            Die();
        }
    }

    // Die
    private void Die()
    {
        //if (this.CompareTag("Tower"))
        //{
        //    GetComponent<Tower>().DoDeathAnimation();
        //}
        //else if (this.CompareTag("Enemy"))
        //{
        //    GetComponent<EnemyHealth(etc.)>().DoDeathAnimation();
        //}

        // Above for later implementation

        // Below for testing:
        Destroy(gameObject);

    }
}
