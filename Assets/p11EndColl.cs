using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class p11EndColl : NetworkBehaviour
{
    public TileHandler tileHand;

    public bool hasEntered = false;
    private void OnTriggerEnter(Collider other) {
        if (!isServer) return;
        hasEntered = true;
        tileHand.hasEnded();
    }
}
