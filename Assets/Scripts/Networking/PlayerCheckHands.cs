using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckHands : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    public bool CheckHands() {
        if (rightHand.transform.childCount > 0) {
            if (leftHand.transform.childCount > 0) {
                return false;
            }
            
        }

        return true;
    }

    public GameObject GetHands() {
        if (rightHand.transform.childCount > 0) {
            return leftHand;
        }
        return rightHand;
    }
}
