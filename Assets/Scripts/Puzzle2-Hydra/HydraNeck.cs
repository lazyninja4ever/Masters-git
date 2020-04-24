using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraNeck : HitableObject
{
    private HydraSpawner parentHS;
    private void Start() {
        Invoke("RespawnHead", 5.0f);
        parentHS = this.transform.parent.transform.parent.GetComponent<HydraSpawner>();
    }

    public override void ReactionFunction() {
        base.ReactionFunction();
        parentHS.KillHydra();
        Destroy(this.gameObject);
    }

    private void RespawnHead() {
        parentHS.SpawnHydra();
        parentHS.SpawnNewHead();
        Destroy(this.gameObject);
        
    }
}
