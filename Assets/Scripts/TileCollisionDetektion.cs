using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TileCollisionDetektion : NetworkBehaviour
{
    

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            ReactToCollision();
        }
    }

    public virtual void ReactToCollision() {

    }
}
