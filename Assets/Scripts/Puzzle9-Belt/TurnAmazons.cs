using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnAmazons : NetworkBehaviour
{
    public bool RotateCW;
    public float turnDegree;
    public Quaternion startRotation;

    private void Start() {
        startRotation = transform.rotation;
        if (RotateCW) {
            turnDegree = 90f;
        }
        else {
            turnDegree = -90f;
        }
    }
    public void TurnQuarterRound() {
        //transform.Rotate(0, turnDegree, 0);
        if (!isServer) return;
        RpcAmazonTurnStatue();
    }

    public void ResetPositions() {
        if (!isServer) return;
        RpcResetRotation(startRotation);
        
    }

    [ClientRpc]
    void RpcAmazonTurnStatue() {
        transform.Rotate(0, 0, turnDegree);
    }
    [ClientRpc]
    void RpcResetRotation(Quaternion degree) {
        transform.rotation = degree;
    }

}
