using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HideAntHoof : NetworkBehaviour {
    public Vector3 CorrectPosition;
    public Vector3 correctRotation;
    public ParticleSystem particles;

    public void moveToPosition() {
        if (!isServer) return;
        RpcSetCorrectPosition();
    }

    [ClientRpc]
    void RpcSetCorrectPosition() {
        particles.Play();
        this.transform.position = CorrectPosition;
        this.transform.rotation = Quaternion.Euler(correctRotation);
    }
}
