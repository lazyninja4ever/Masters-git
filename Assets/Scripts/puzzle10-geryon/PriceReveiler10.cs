using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PriceReveiler10 : NetworkReveiler
{
    public GameObject chestLid;
    public GameObject keyItem;
    public AudioSource priceReveal;
    public ParticleSystem revealParticles;
    public Animator chestLidAnim;
    public AudioSource chestLidSound;

    public bool hasReveiled = false;

    public override void ReveilPrice() {
        base.ReveilPrice();
        if (!isServer) return;
        if (!hasReveiled) {
            RpcOpenChest();
            keyItem.GetComponent<ShowObjects>().moveToPosition();
            hasReveiled = true;
        }
        
        
    }

    [ClientRpc]
    void RpcOpenChest() {
        priceReveal.Play();
        chestLidSound.Play();
        chestLidAnim.Play("chestLid");
        revealParticles.Play();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {

                player.GetComponent<PlayerMovement>().EnableText("Puzzle Solved", "");
                player.GetComponent<PlayerMovement>().Invoke("DisableText", 2.5f);


            }
        }


    }

}
