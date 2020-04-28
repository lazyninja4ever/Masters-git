using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClueHighLighter : NetworkBehaviour
{
    public NetworkReceiverItem[] piedestals;
    public Light[] highLights;

    private void Start() {
        for (int i = 0; i < highLights.Length; i++) {
            highLights[i].enabled = false;
        }
    }

    public void CheckHighLight() {
        if (!isServer) return;
        int lightCountLeft = FromLeft();
        int lightCountRight = 0;
        if (lightCountLeft != 14) {
            lightCountRight = FromRight();
        }
        RpcFlipLights(lightCountLeft, lightCountRight);
        
    }

    public int FromLeft() {
        int lightCount = 0;

        for (int i = 0; i < piedestals.Length; i++) {
            if (!piedestals[i].isOccupied) {
                break;
            }
            lightCount += piedestals[i].heldItem.GetComponent<p10Collectible>().clueSize;
        }

        return lightCount;

    }

    public int FromRight() {
        int lightCount = 0;
        for (int i = piedestals.Length-1; i > 0; i--) {
            if (!piedestals[i].isOccupied) {
                break;
            }
            lightCount += piedestals[i].heldItem.GetComponent<p10Collectible>().clueSize;
        }

        return lightCount;
    }

    [ClientRpc]
    void RpcFlipLights(int left, int right) {
        for (int i = 0; i < highLights.Length; i++) {
            if (i < left || i > (highLights.Length - 1) - right) {
                highLights[i].enabled = true;
            }
            else {
                highLights[i].enabled = false;
            }

        }
    }
}
