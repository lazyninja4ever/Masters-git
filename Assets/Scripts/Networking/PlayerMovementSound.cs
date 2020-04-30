using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovementSound : NetworkBehaviour
{
    public AudioSource playerMoveSound;
    public AudioClip softSound;
    public AudioClip hardSound;

    [Command]
    public void CmdPlayerMovementSound(int surface) {
        RpcPlaySound(surface);
    }

    [Command]
    public void CmdStopMovementSound() {
        RpcStopSound();
    }

    [ClientRpc]
    void RpcPlaySound(int surface) {
        if (surface == 0) {
            playerMoveSound.clip = softSound;
        } else if (surface == 1) {
            playerMoveSound.clip = hardSound;
        }
        playerMoveSound.Play();
    }

    [ClientRpc]
    void RpcStopSound() {
        playerMoveSound.Stop();
    }
}
