using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror; //added



public class NetworkInteraction : NetworkBehaviour //changed from monobehaviour
{
    public string InteractMsg;
    public bool itemType = false;
    public bool itemHeld;
    public bool isInItem;
    public GameObject holderItem;
    public bool isPlaceable;
    public int solutionNmbr;
    public float placementOffsetY;
    public float pickupOffsetX;
    public float pickupOffsetY;
    public float pickupOffsetZ;

    public Sprite serverImage;
    public Sprite clientImage;


    public virtual void InterActionFuntion(GameObject player) {

    }

}

