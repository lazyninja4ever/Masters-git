using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BeltInteraction : NetworkInteraction
{
    public GameObject hippol;
    public ShowObjects amazons;
    public AmazonHandler amazonHandler;
    public HideObjects belt;
    public BeltState beltState;
    public showWithAnim fenceAnim;

    public AudioSource keyItemCollection;

    public override void InterActionFuntion(GameObject player) {
        base.InterActionFuntion(player);
        RpcInteract();
    }

    [ClientRpc]
    void RpcInteract() {
        //fence.moveToPosition();
        fenceAnim.HideObject();
        keyItemCollection.Play();
        amazonHandler.gameOn = true;
        amazonHandler.ResetPositions();
        amazons.moveToPosition();
        hippol.layer = LayerMask.NameToLayer("Interact");
        beltState.onHippol = false;
        beltState.inInvent = true;
        belt.moveToPosition();


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {

                player.GetComponent<PlayerMovement>().EnableText("Key item collected", "");
                player.GetComponent<PlayerMovement>().Invoke("DisableText", 2.5f);


            }
        }
    }

}
