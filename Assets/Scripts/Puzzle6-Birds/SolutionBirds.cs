using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SolutionBirds : NetworkBehaviour
{
    //public List<string> deadBirds = new List<string>();
  //  public GameObject chestLid;
    public ShowObjects priceReveil;
    public bool priceReveiledOnce;
    public int birdsCount;
    public bool hasIncreased = false;
    public GameObject[] players;
    public AudioSource lidOff;
    public AudioSource reveilPriceSound;
    public ParticleSystem particleReveil;
    public AudioSource crowsAmbSound;
    public Animator chestLidAnim;

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
        crowsAmbSound.Stop();
        lidOff.Play();
        chestLidAnim.Play("chestLid");
        priceReveil.moveToPosition();
        priceReveiledOnce = true;
        reveilPriceSound.Play();
        particleReveil.Play();
    }

}
