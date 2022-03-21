using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBalls : MonoBehaviour
{

    [SerializeField]
    protected float _BallRadius = 1.0f;


    public virtual void OnDrawGizmos()
    {
        //Draw overlap sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _BallRadius);
    }
}
