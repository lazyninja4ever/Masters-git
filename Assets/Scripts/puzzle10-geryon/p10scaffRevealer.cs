using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class p10scaffRevealer : NetworkReveiler
{
    public GameObject fence;
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
        fence.GetComponent<showWithAnim>().ShowObject();
    }
}
