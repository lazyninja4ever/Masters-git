using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClueHighLighter : NetworkBehaviour
{
    public NetworkReceiverItem[] piedestals;
    public Light[] highLights;


    public void CheckHighLight() {
        int lightCount = FromLeft();
        if (lightCount != 14) {
            FromRight();
        }
        
    }

    public int FromLeft() {
        int lightCount = 0;

        for (int i = 0; i < piedestals.Length; i++) {
            if (!piedestals[i].isOccupied) {
                break;
            }
            lightCount += piedestals[i].heldItem.GetComponent<NetworkInteraction>().clueSize;
        }

        return lightCount;

    }

    public int FromRight() {
        int lightCount = 0;
        for (int i = piedestals.Length; i > 1; i--) {
            if (!piedestals[i].isOccupied) {
                break;
            }
            lightCount += piedestals[i].heldItem.GetComponent<NetworkInteraction>().clueSize;
        }

        return lightCount;
    }
}
