﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TileHandler : NetworkBehaviour
{
    public string[] ColorPath = { "red", "blue", "red", "red", "blue", "yellow", "white", "blue", "white", "yellow", "blue", "yellow" };
    public int colorPathIncrease = 0;
    public bool puzzleSolved;
    public string currentColor;
    public p11EndColl rightEnd;
    public p11EndColl leftEnd;
    public ResetPositionP11 resetPositionScript;
    public ParticleSystem fireL1;
    public ParticleSystem fireL2;
    public ParticleSystem fireL3;
    public ParticleSystem fireL4;
    public bool hasEntered;

    public ParticleSystem fireR1;
    public ParticleSystem fireR2;
    public ParticleSystem fireR3;
    public ParticleSystem fireR4;

    public AudioSource fireSound;
    public Animator fenceEndAnim;
    public AudioSource fenceSound;
    public Animator fenceEndStartAnim;
    public AudioSource endGameSound;
    public ParticleSystem particleEnd;


    public void Start()
    {
        currentColor = ColorPath[0];
    }

    public void hasEnded() {
        if (rightEnd.hasEntered && leftEnd.hasEntered && hasEntered == false) {
            /*  foreach (HideObjects fence in fences) {
                  fence.moveToPosition();
              }*/
            
            RpcFinalFence();
        }  
    }

    void ResetBoard() {
        colorPathIncrease = 0;
        currentColor = ColorPath[colorPathIncrease];
    }

    public void CheckColour(string tileColour) {
        if (rightEnd.hasEntered && leftEnd.hasEntered) {
            return;
        } else {
            Debug.Log("colour: " + tileColour + "_ expected " + currentColor);
            if (tileColour == currentColor) {
                colorPathIncrease++;
                Debug.Log("tile number in order: " + colorPathIncrease);
                if (colorPathIncrease >11) {
                    colorPathIncrease = 11;
                }
                currentColor = ColorPath[colorPathIncrease];
            }
            else {
                RpcPlayResetAnimations();
                resetPositionScript.Invoke("ResetPosition", 0.2f);
                Invoke("ResetBoard", 0.2f);
                
            }
        }

        
    }

    [ClientRpc]
    public void RpcPlayResetAnimations()
    {
        fireL1.Play();
        fireL2.Play();
        fireL3.Play();
        fireL4.Play();
        fireR1.Play();
        fireR2.Play();
        fireR3.Play();
        fireR4.Play();
        fireSound.Play();
    }

    [ClientRpc]
    public void RpcFinalFence()
    {
        hasEntered = true;
        fenceSound.Play();
        fenceEndAnim.Play("FenceEndGame");
        fenceEndStartAnim.Play("fenceEndGameSingle");
        endGameSound.Play();
        particleEnd.Play();

    }

    /*
    public void CallRpcColorCheck(GameObject tileColor)
    {
        Debug.Log("collided");
        if (!isServer) return;
        RpcCheckColor(tileColor);
    }

    public void CallNextColor()
    {
        Debug.Log("called callrpcnext");
        if (!isServer) return;
        RpcNextColor();
    }


    [ClientRpc]
    public void RpcCheckColor(GameObject tile)
    {
        Debug.Log("called RpcCheckColor");
        if (tile.CompareTag(currentColor))
        {
            Debug.Log("is true");
            CallNextColor();
        }
        else
        {
            Debug.Log("is false");
            resetPositionScript.ResetPosition();
        }
    }

    [ClientRpc]
    public void RpcNextColor()
    {
        colorPathIncrease++;
        currentColor = ColorPath[colorPathIncrease];
    }*/

}
