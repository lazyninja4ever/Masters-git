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

    public HideObjects[] fences;


    public void Start()
    {
        currentColor = ColorPath[0];
    }

    public void hasEnded() {
        if (rightEnd.hasEntered && leftEnd.hasEntered) {
            foreach (HideObjects fence in fences) {
                fence.moveToPosition();
            }
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
                resetPositionScript.ResetPosition();
                Invoke("ResetBoard", 0.2f);
                
            }
        }

        
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