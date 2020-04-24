using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PriceCollect : NetworkInteraction
{
    public HideObjects price;
    public bool inInventory = false;

    public override void InterActionFuntion(GameObject player)
    {
        base.InterActionFuntion(player);
        RpcInteract();
    }

    [ClientRpc]
    void RpcInteract()
    {
        price.moveToPosition();
        inInventory = true;
    }
}
