using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ResetPlayerPositionP9 : NetworkBehaviour
{
    public Transform player1Pos;
    public Transform player2Pos;
    public GameObject[] players;
    public AmazonHandler amazonHandler;
    public GameObject resetCloud;
    //public GameObject player1Particle;

    public void ResetPosition() {
        //assuming there are only two players in the game
        //called from swordCollision by server
        RpcResetPlayerPosition();
        

    }

    [ClientRpc]
    void RpcResetPlayerPosition() {
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            Transform newPosition;
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {
                if (isServer) {
                    newPosition = player1Pos;
                }
                else {
                    newPosition = player2Pos;
                }
                //Instantiate(resetCloud, newPosition.position, Quaternion.identity);
                //player1Particle.GetComponent<ParticleSystem>().Play();
                player.transform.Find("ResetCloud").GetComponent<ParticleSystem>().Play();
                player.transform.Find("ResetCloud").GetComponent<AudioSource>().Play();

                player.GetComponent<CharacterController>().enabled = false;
                player.transform.SetPositionAndRotation(newPosition.position, newPosition.rotation);
                player.GetComponent<CharacterController>().enabled = enabled;
                

            }
        }
        amazonHandler.ResetPositions();
    }

}
