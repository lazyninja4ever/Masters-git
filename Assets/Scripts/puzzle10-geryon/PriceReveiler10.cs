using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PriceReveiler10 : NetworkReveiler
{
    public GameObject chestLid;
    public GameObject keyItem;
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
        chestLid.transform.localPosition = new Vector3(0.55f, -0.2f, -0.6f);
        chestLid.transform.localRotation = Quaternion.Euler(-161.55f, 0f, 0f);
    }

}
