using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class showWithAnim : NetworkBehaviour
{
    public AudioSource fenceSound;
    public Animator fenceAnim;
    // Start is called before the first frame update
    public void ShowObject() {
        if (!isServer) return;
        RpcShowItem();
    }

    public void HideObject() {
        if (!isServer) return;
        RpcHideItem();
    }


    [ClientRpc]
    void RpcShowItem() {
        fenceAnim.Play("fenceShow");
        fenceSound.Play();
    }

    [ClientRpc]
    void RpcHideItem() {
        fenceAnim.Play("fenceHide");
        fenceSound.Play();
    }
    
    
}
