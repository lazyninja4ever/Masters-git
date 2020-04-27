using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class p10scaffRevealer : NetworkReveiler
{
    public Animator fenceAnim;
    public AudioSource fenceSound;
    public bool hasReveiled = false;

    public override void ReveilPrice() {
        base.ReveilPrice();
        if (!isServer) return;
        if (!hasReveiled) {
            RpcOpenChest();
            hasReveiled = true;
        }


    }

    [ClientRpc]
    void RpcOpenChest() {
        fenceAnim.Play("fenceHide");
        fenceSound.Play();
    }
}
