using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BirdsShot : NetworkBehaviour
{
    public SolutionBirds birdsSolutionScript;
    public bool isShot;

    public void Start()
    {
        this.GetComponentInChildren<CapsuleCollider>().enabled = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if(isShot == false)
            {
                //  if (!isServer) return;
                //  RpcIncreaseBirds();
                birdsSolutionScript.GetComponent<SolutionBirds>().birdsCount++;
                this.isShot = true;
                this.GetComponentInChildren<CapsuleCollider>().enabled = true;
            }
        }


        if(collision.gameObject.name == "Landscape.001")
        {
            Debug.Log("collided with ground bird");
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    [ClientRpc]
    void RpcIncreaseBirds()
    {
        birdsSolutionScript.GetComponent<SolutionBirds>().birdsCount++;
        this.isShot = true;
        this.GetComponentInChildren<CapsuleCollider>().enabled = true;
    }
}
