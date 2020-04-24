using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraSolved : PriceReveiler
{
    [SerializeField] private GameObject chestLid;
    [SerializeField] private GameObject keyItem;

    public override void ReveilPrice() {
        base.ReveilPrice();
        chestLid.transform.localPosition = new Vector3(0.05f, -0.2f, -0.6f);
        chestLid.transform.localRotation = Quaternion.Euler(-161.55f, 0, 0);
        keyItem.SetActive(true);
    }
}
