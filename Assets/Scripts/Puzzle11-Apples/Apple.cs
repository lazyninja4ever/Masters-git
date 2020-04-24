using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Apple : NetworkInteraction
{
    public HideObjects apple;
    public bool inInventory = false;
    public AudioSource collectPriceSound;


    public override void InterActionFuntion(GameObject player)
    {
        base.InterActionFuntion(player);
        RpcInteract();
    }
    
    [ClientRpc]
    void RpcInteract()
    {
        apple.moveToPosition();
        collectPriceSound.Play();
        inInventory = true;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
            {

                player.GetComponent<PlayerMovement>().EnableText("Key item collected", "");
                player.GetComponent<PlayerMovement>().Invoke("DisableText", 2.5f);


            }
        }

    }
}
