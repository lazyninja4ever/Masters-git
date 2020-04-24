using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class showAntHoof : NetworkBehaviour
{
    public Vector3 CorrectPosition;
    public Vector3 correctRotation;

    public void moveToPosition() {
        if (!isServer) return;
        RpcSetCorrectPosition();
    }

    [ClientRpc]
    void RpcSetCorrectPosition() {
        this.transform.position = CorrectPosition;
        this.transform.rotation = Quaternion.Euler(correctRotation);
    }
}
