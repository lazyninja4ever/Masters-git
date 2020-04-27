using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class p10scaffRevealer : NetworkReveiler
{
    public Animator fenceAnim;
    public AudioSource fenceSound;
    public bool hasReveiled = false;
    public HideObjects[] elementsOfPuzzle;
    public ParticleSystem[] elementParticles;
    public AudioSource disappearingSound;

    public override void ReveilPrice() {
        base.ReveilPrice();
        if (!isServer) return;
        if (!hasReveiled) {
            RpcOpenChest();
            hasReveiled = true;
        }


    }

    [ClientRpc]
    void RpcOpenChest() {
        fenceAnim.Play("fenceHide");
        fenceSound.Play();
        foreach (HideObjects elem in elementsOfPuzzle) {
            elem.moveToPosition();
        }
        foreach (ParticleSystem partSys in elementParticles) {
            disappearingSound.Play();
            partSys.Play();            
        }
    }
}
