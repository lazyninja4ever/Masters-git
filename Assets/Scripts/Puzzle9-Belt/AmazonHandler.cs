using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AmazonHandler : NetworkBehaviour
{
    public List<TurnAmazons> amazons;
    public AudioSource amazonSounds;
    public bool gameOn = false;

    public void TakeStep() {
        if (!isServer) return;
        
        //don't rotate unless game is in progress, else they are underground
        if (!gameOn) return;

        foreach (var amazon in amazons) {
            amazon.TurnQuarterRound();
            RpcPlaySound();
        }
    }

    public void ResetPositions() {
        if (!isServer)
            return;

        foreach (var amazon in amazons) {
            amazon.ResetPositions();
            RpcPlaySound();
        }
    }

    [ClientRpc]
    void RpcPlaySound() {
        amazonSounds.Play();
    }
}
