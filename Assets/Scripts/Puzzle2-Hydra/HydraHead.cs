using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraHead : HitableObject
{

    public override void ReactionFunction() {
        base.ReactionFunction();
        this.transform.parent.GetComponent<HydraSpawner>().Decapitate();
        Destroy(this.gameObject);
    }

}
