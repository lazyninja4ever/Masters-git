using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PriceInteraction : NetworkInteraction
{
    public HideObjects hideScript;
    public AudioSource priceSound;

    public override void InterActionFuntion(GameObject player) {
        base.InterActionFuntion(player);
        if (!isServer) return;
        RpcInteract();
    }

    [ClientRpc]
    void RpcInteract() {
        priceSound.Play();
        hideScript.moveToPosition();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {

                player.GetComponent<PlayerMovement>().EnableText("Key item collected", "");
                player.GetComponent<PlayerMovement>().Invoke("DisableText", 2.5f);


            }
        }

    }
}
