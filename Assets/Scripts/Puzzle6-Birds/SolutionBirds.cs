using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SolutionBirds : NetworkBehaviour
{
    //public List<string> deadBirds = new List<string>();
    public GameObject chestLid;
    public ShowObjects priceReveil;
    public bool priceReveiledOnce;
    public int birdsCount;
    public bool hasIncreased = false;
    public GameObject[] players;

    // Update is called once per frame
    void Update()
    {

        if (!isServer)
        {
            if (birdsCount == 4 && !hasIncreased)
            {
                players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject player in players)
                {
                    if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
                    {
                        player.gameObject.GetComponent<PlayerShotBirds>().CmdShotBirds();
                        hasIncreased = true;
                    }
                }

            }
        }
        if (birdsCount == 8 && priceReveiledOnce == false)
        {
            if (!isServer) return;
            RpcReveilPrice();
        }
        
    }

    public void IncreaseBirds(int birdShot)
    {
        if (!isServer) return;
        birdsCount += birdShot;
    }

    [ClientRpc]
    public void RpcReveilPrice()
    {
        chestLid.transform.localPosition = new Vector3(0.05f, -0.2f, -0.6f);
        chestLid.transform.localRotation = Quaternion.Euler(-161.55f, 0, 0);
        priceReveil.moveToPosition();
        priceReveiledOnce = true;
    }
}
