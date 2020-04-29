using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class gameEnder : NetworkBehaviour
{
    public bool serverEnter;
    public bool clientEnter;
    public AmazonHandler amazonHandler;

    public showWithAnim fenceAnim;
    public HideObjects hideAmazons;
    public AudioSource amazonSound;

    private void OnTriggerEnter(Collider other) {
        if (!isServer) return;
        if (other.gameObject.CompareTag("Player")) {
            if (other.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {
                serverEnter = true;
            }
            else {
                clientEnter = true;
            }
            EndGame();
        }
    }

    void EndGame() {
        if (serverEnter && clientEnter) {
            RpcShowGameEnd();
        }
    }

    [ClientRpc]
    void RpcShowGameEnd() {
        fenceAnim.ShowObject();
        amazonHandler.gameOn = false;
        amazonSound.Play();
        hideAmazons.moveToPosition();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {
                player.GetComponent<PlayerMovement>().EnableText("Puzzle Solved", "");
                player.GetComponent<PlayerMovement>().Invoke("DisableText", 2.5f);
            }
        }
    }
}
