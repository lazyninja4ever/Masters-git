using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HippolytaInteract : NetworkInteraction
{
    public ShowObjects belt;
    //public ShowObjects fence;
    public BeltState beltState;
    public showWithAnim fenceAnim;

    public AudioSource receiveItemSound;


    public override void InterActionFuntion(GameObject player) {
        base.InterActionFuntion(player);
        if (!isServer) return;
        RpcInteract();
    }

    [ClientRpc]
    void RpcInteract() {
        belt.moveToPosition();
        receiveItemSound.Play();
        //fence.moveToPosition();
        fenceAnim.ShowObject();
        beltState.onHippol = true;
        beltState.inInvent = false;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }

}
