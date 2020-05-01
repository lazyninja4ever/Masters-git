using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class IsLocalScriptDisabler : NetworkBehaviour
{
    public PlayerMovement movementScript;
    public GameObject playerCamera; //changed from gameobject
    public MouseLook lookScript; // added
    public GameObject[] characterParts;
    public SkinnedMeshRenderer playerMesh;
    public Mesh decapMesh;
    public Material hidingMaterial; 


    private void Start() {
        if (!isLocalPlayer) {
            movementScript.enabled = false;
            playerCamera.SetActive(false);// = false; //changed from setActive(False)
            //lookScript.enabled = false;
            Debug.Log("Tried to diable player");
            
        }
        if (isLocalPlayer) {
            playerMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh = decapMesh;
            foreach (GameObject part in characterParts) {
                part.GetComponent<SkinnedMeshRenderer>().material = hidingMaterial;
                //part.layer = LayerMask.NameToLayer("PlayerCharacter");
            }
        }
    }
}
