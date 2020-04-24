using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; //added


public class TimedHandle : HandleButtonInteract
{
    public AudioSource handleSound;
    public Animation handleAnimation;
    public NetworkSolutionChecker solutionChecker;


    public override void InterActionFuntion(GameObject player) {
        base.InterActionFuntion(player);
        Debug.Log("ran on handle");
        if (!isServer) return;
        if (isOn) {
            RpcFlipOff();
        }
        else if (!isOn) {
            RpcFlipOn();
            Invoke("RpcFlipOff", 1f);
            
        }
    }

    
    [ClientRpc]
    void RpcFlipOn() {
        isOn = true;
        isSolved = true;
        solutionChecker.PuzzleState();
        handleSound.Play();
        handleAnimation.Play("Handle|HandleOn");

    }

    [ClientRpc]
    void RpcFlipOff() {
        if (isOn) {
            isOn = false;
            isSolved = false;
            handleSound.Play();
            handleAnimation.Play("Handle|HandleOff");

        }
    }
}
