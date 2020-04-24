using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BowPickUp : NetworkCollectible
{
    public Transform bow;
    public Transform player;
    public Transform cameraObj;
    public GameObject thisObject;
    public AudioSource bowSound;
    // Update is called once per frame

    void Update()
    {
        if (itemHeld == true)
        {
            player = this.transform.root;
            cameraObj = player.Find("CameraParent").Find("PlayerCamera (1)");
            bow = cameraObj.Find("Bow");

            bow.GetComponent<BowState>().hasBow = true;

            if (Input.GetMouseButtonDown(0) && bow.GetComponent<BowState>().hasBow == true)
            {
                bow.GetComponent<BowState>().Equipt();
                thisObject = this.gameObject;
                thisObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
            if (Input.GetMouseButtonUp(0) && bow.GetComponent<BowState>().hasBow == true)
            {
                bow.GetComponent<BowState>().Unequipt();
                this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                bow.GetComponent<BowState>().hasBow = false;
                bow.GetComponent<BowState>().DropWeapon();
                this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }
    }
}
