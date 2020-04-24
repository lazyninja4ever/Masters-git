using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BeltReveiler : NetworkReveiler
{

    public ShowObjects belt;
    //public ShowObjects fences;
    public HideObjects beltHide;
    //public HideObjects fenceHide;
    public BeltState beltState;
    public HideObjects amazons;
    public showWithAnim fenceAnim;
    public AmazonHandler amazonHandler;

    public ParticleSystem beltRevealCloud;

    public AudioSource amazonSound;
    public AudioSource beltSound;


    public override void ReveilPrice() {
        if (!isServer) return;

        if (beltState.unexposed) {
            RpcReveil();
        } else if (beltState.onHippol) {
            RpcHide();
        } 
        
    }

    [ClientRpc]
    void RpcReveil() {
        belt.moveToPosition();
        beltSound.Play();
        beltRevealCloud.Play();
        //fences.moveToPosition();
        fenceAnim.ShowObject();
        beltState.unexposed = false;
        beltState.onHippol = true;
    }

    [ClientRpc]
    void RpcHide() {
        beltHide.moveToPosition();
        //fenceHide.moveToPosition();
        fenceAnim.HideObject();
        amazonHandler.gameOn = false;
        amazonSound.Play();
        amazons.moveToPosition();
        beltState.unexposed = true;
        beltState.onHippol = false;
    }

}
