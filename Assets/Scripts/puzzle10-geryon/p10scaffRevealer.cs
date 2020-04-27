using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class p10scaffRevealer : NetworkReveiler
{
    public GameObject chestLid;
    public GameObject keyItem;
    public GameObject fence;
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
        fence.GetComponent<showWithAnim>().ShowObject();
    }
}
