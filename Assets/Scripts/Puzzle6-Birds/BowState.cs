using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BowState : NetworkBehaviour
{
    public Shoot shootScript;
    public Animator playerAnim;
    public bool isShooting;
    public bool hasBow;
    public GameObject thisBow;
    public PlayerMovement playerScript;
    
    private void Start()
    {
        this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        shootScript.GetComponent<Shoot>().enabled = false;
    }

    public void Update()
    {
        if(hasBow == true)
        {
            playerScript.GetComponent<PlayerMovement>().hasBow = true;
        }
        else
        {
            playerScript.GetComponent<PlayerMovement>().hasBow = false;
        }
    }

    public void Unequipt()
    {
        thisBow = this.gameObject;
        isShooting = false;
        thisBow.GetComponent<SkinnedMeshRenderer>().enabled = false;
        playerAnim.Play("Idle_002");
    }
   
    public void Equipt()
    {
        isShooting = true;
        thisBow = this.gameObject;
        thisBow.GetComponent<SkinnedMeshRenderer>().enabled = true;
        shootScript.GetComponent<Shoot>().enabled = true;
    }

    public void DropWeapon()
    {
        shootScript.GetComponent<Shoot>().enabled = false;
    }
}

