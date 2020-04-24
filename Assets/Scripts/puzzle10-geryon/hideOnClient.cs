using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class hideOnClient : NetworkBehaviour
{
    public List<GameObject> clues;
    private void Start() {
        if (!isServer) {
            foreach (GameObject clue in clues) {
                clue.layer = LayerMask.NameToLayer("PlayerCharacter");
            }
        }
    }
}
