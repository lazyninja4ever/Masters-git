using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PriceInteraction : NetworkInteraction
{
    public HideObjects hideScript;

    public override void InterActionFuntion(GameObject player) {
        base.InterActionFuntion(player);
        if (!isServer) return;
        RpcInteract();
    }

    [ClientRpc]
    void RpcInteract() {
        hideScript.moveToPosition();
        
    }
}
