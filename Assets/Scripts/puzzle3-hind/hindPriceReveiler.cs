using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class hindPriceReveiler : NetworkReveiler
{
    public GameObject stair;
    public GameObject keyItem;
    public bool hasReveiled = false;
    public Vector3 newPosition;

    public Animator stairAnimation;
    public AudioSource stairMovement;
    public AudioSource priceReveal;

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
        stairAnimation.Play("stairMove");
        stairMovement.Play();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {

                player.GetComponent<PlayerMovement>().EnableText("Puzzle Solved", "");
                player.GetComponent<PlayerMovement>().Invoke("DisableText", 2.5f);


            }
        } priceReveal.Play();

        //stair.transform.localPosition = newPosition;

    }
}
