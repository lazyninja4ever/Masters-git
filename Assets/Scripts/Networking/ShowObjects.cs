using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShowObjects : NetworkBehaviour
{
    //public Rigidbody rbody;

    public Vector3 CorrectPosition;

    public void moveToPosition() {
        if (!isServer) return;
        RpcSetCorrectPosition();
    }

    [ClientRpc]
    void RpcSetCorrectPosition() {
        this.transform.position = CorrectPosition;
    }

}
