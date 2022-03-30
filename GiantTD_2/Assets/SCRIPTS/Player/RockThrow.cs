using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{

    public float pickUpDistance = 50f;
    public bool hasRock = false;
    public float throwPower = 0f;
    public GameObject rock;
    [SerializeField]
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            CheckForRocks();
        }
        if(Input.GetButton("Fire2")) //&& hasRock)
        {
            throwPower += Time.deltaTime + 1;
            if(throwPower >= 300)
            {
                throwPower = 300f;
            }
        }
        if(Input.GetButtonUp("Fire2"))
        {
            GameObject instantiatedRock = Instantiate(rock, mainCam.transform.position, Quaternion.identity);
            Vector3 throwDirection = mainCam.transform.forward;
            instantiatedRock.GetComponent<Rigidbody>().AddForce(throwDirection * throwPower);
            throwPower = 0f;
        }
    }

    void CheckForRocks()
    {
        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if (hit.transform.tag == "Rock" && Vector3.Distance(mainCam.transform.position, hit.transform.position) < pickUpDistance)
            {
                Debug.Log("jebu");
                hasRock = true;
            }
        }
    }
}
