using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private Vector3 location;
    private bool hasMountain;
    private bool hasTree;

    void Start()
    {
        location = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getLocation()
    {
        return location;
    }
    
    public void setLocation(Vector3 loc)
    {
        location = loc;
    }

    public bool getHasMountain()
    {
        return hasMountain;
    }
    public void setHasMountain(bool hasMt)
    {
        hasMountain = hasMt;
    }

    public bool getHasTree()
    {
        return hasTree;
    }

    public void setHasTree(bool isTree)
    {
        hasTree = isTree;
    }
}
