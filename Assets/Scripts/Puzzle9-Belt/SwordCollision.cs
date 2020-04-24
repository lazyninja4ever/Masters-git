using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : TileCollisionDetektion
{
    public ResetPlayerPositionP9 resetScript;
    public AudioSource swordHitSound;

    public override void ReactToCollision() {
        swordHitSound.Play();
        if (!isServer) return;
        resetScript.ResetPosition();
        
    }
}
