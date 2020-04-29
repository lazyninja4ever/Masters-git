using System.Collections;
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

    public ParticleSystem fireR1;
    public ParticleSystem fireR2;
    public ParticleSystem fireR3;
    public ParticleSystem fireR4;

   // public HideObjects[] fences;
    public AudioSource fireSound;
    public Animator fenceEndAnim;
    public AudioSource fenceSound;


    public void Start()
    {
        currentColor = ColorPath[0];
    }

    public void hasEnded() {
        if (rightEnd.hasEntered && leftEnd.hasEntered) {
            /*  foreach (HideObjects fence in fences) {
                  fence.moveToPosition();
              }*/
            fenceEndAnim.Play("FenceEndGame");
            RpcPlaySound();
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
        fireSound.Play();
        fireL1.Play();
        fireL2.Play();
        fireL3.Play();
        fireL4.Play();
        fireR1.Play();
        fireR2.Play();
        fireR3.Play();
        fireR4.Play();
    }

    [ClientRpc]
    public void RpcPlaySound()
    {
        fenceSound.Play();
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
