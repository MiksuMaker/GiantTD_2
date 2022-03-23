using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // This script is for dealing with health

    // It is universal so that all mortal things will get equally smashed to bits
    // if stepped on by the Giant. No need to check whether if was a tower or enemy.


    [SerializeField] int health = 20;   // Spawn Health, changed by other script

    [SerializeField] private bool DEBUG_SWITCH = true;

    public void SetHealth(int _healthAmount)
    {
        health = _healthAmount;
        Debug.Log("Health has been set for: " + gameObject.name);
    }

    public void DealDamage(int _damageAmount)
    {
        if (DEBUG_SWITCH) Debug.Log(gameObject.name + " took " + _damageAmount + " amount of damage!");

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
        if (this.CompareTag("Tower"))
        {
            GetComponent<Tower>().DoDeath();
        }
        //else if (this.CompareTag("Enemy"))
        //{
        //    GetComponent<EnemyHealth(etc.)>().DoDeathAnimation();
        //}

        // Destroy
        DestroyWithDelay(3f);       // Hardcoded for the moment

        // HUOM! Vaihtoehtoisesti t‰m‰n voi hoitaa Death Animaation lopussa triggerin‰
        // -> ei tarvitse s‰‰t‰‰ ajastuksen kanssa niin paljoa
    }

    public void DestroyWithDelay(float _delay_fuse) 
    {
        //Destroy(GetComponent<Tower>());

        if (DEBUG_SWITCH) Debug.Log(gameObject.name + " has been DESTROYED!");

        Destroy(gameObject, _delay_fuse);
    }
}
