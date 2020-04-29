using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BoardChecker : NetworkReveiler {
    public List<showAntHoof> antlers;
    public List<showAntHoof> hooves;

    public List<pipeRotator> board1;
    public List<pipeRotator> board2;

    public int shownItem1;
    public int shownItem2;
    public showAntHoof shownItem1GO;
    public showAntHoof shownItem2GO;


    public ParticleSystem particleRight;
    public ParticleSystem particleWrong;
    public ParticleSystem particleHead;
    public ParticleSystem particleHoof;

    public AudioSource rightSound;
    public AudioSource wrongSound;
    public AudioSource headSound;
    public AudioSource hoofSound;
    public AudioSource noItemSound;

    public override void ReveilPrice() {
        if (!isServer) return;
        RpcParticleSetupReset();
        ResetItems();
        base.ReveilPrice();
        Invoke("CheckBoards", 0.5f);
    }

    public void CheckBoards() {
        int result1 = CheckBoardOne();
        int result2 = CheckBoardTwo();
        if (result1 != 6) {
            antlers[result1].moveToPosition();
            shownItem1 = result1;
        }

        if (result2 != 6) {
            hooves[result2].moveToPosition();
            shownItem2 = result2;
        }

        if (result1 == 6 && result2 == 6) {
            RpcPlayNoDispence();
        }else if (result1 < 3 || result2 < 3) {
            RpcPlayRightDispence();
        } else if (result1 < 6 || result2 < 6) {
            RpcPlayWrongDispence();
        }
    }

    public int CheckBoardOne() {
        //for gold antlers
        if (board1[4].pipePosition == 0 || board1[4].pipePosition == 3) {
            if (board1[1].pipePosition == 0 && board1[9].pipePosition == 1 && board1[10].pipePosition == 3) {
                //right path down
                if (board1[4].pipePosition == 0) {
                    if (board1[5].pipePosition == 2 && board1[7].pipePosition == 1 && board1[8].pipePosition == 3) {
                        //wrong exit
                        if (board1[12].pipePosition == 3) {
                            return 3;
                            // right exit
                        } else if (board1[12].pipePosition == 0 && board1[13].pipePosition == 1 && board1[14].pipePosition == 0) {
                            return 0;
                        }
                    }
                    //left path down
                } else if (board1[4].pipePosition == 3) {
                    if (board1[3].pipePosition == 1 && board1[6].pipePosition == 0 && board1[7].pipePosition == 0) {
                        //wrong exit
                        if (board1[12].pipePosition == 3) {
                            return 3;
                            // right exit
                        } else if (board1[12].pipePosition == 0 && board1[13].pipePosition == 1 && board1[14].pipePosition == 0) {
                            return 0;
                        }
                    }
                }
            }
            //for silver antlers
        } else if (board1[4].pipePosition == 2) {
            if (board1[0].pipePosition == 0 && board1[3].pipePosition == 0 && board1[8].pipePosition == 2 && board1[11].pipePosition == 0) {
                //short path
                if (board1[7].pipePosition == 0) {
                    //right exit
                    if (board1[14].pipePosition == 0) {
                        return 1;
                        //wrong exit
                    } else if (board1[14].pipePosition == 1 && board1[13].pipePosition == 1 && board1[12].pipePosition == 1) {
                        return 4;
                    }
                    //long path
                } else if (board1[7].pipePosition == 1 && board1[6].pipePosition == 1 && board1[9].pipePosition == 0 && board1[10].pipePosition == 3) {
                    //right exit
                    if (board1[14].pipePosition == 0) {
                        return 1;
                        //wrong exit
                    }
                    else if (board1[14].pipePosition == 1 && board1[13].pipePosition == 1 && board1[12].pipePosition == 1) {
                        return 4;
                    }
                }
            }
            //for bronze antlers
        } else if (board1[4].pipePosition == 1) {
            if (board1[2].pipePosition == 0 && board1[5].pipePosition == 3 && board1[8].pipePosition == 2 && board1[11].pipePosition == 0) {
                //short path
                if (board1[7].pipePosition == 0) {
                    //right exit
                    if (board1[14].pipePosition == 0) {
                        return 2;
                        //wrong exit
                    } else if (board1[14].pipePosition == 1 && board1[13].pipePosition == 1 && board1[12].pipePosition == 1) {
                        return 5;
                    }
                    //long path
                } else if (board1[7].pipePosition == 1 && board1[6].pipePosition == 1 && board1[9].pipePosition == 0 && board1[10].pipePosition == 3) {
                    //right exit
                    if (board1[14].pipePosition == 0) {
                        return 2;
                        //wrong exit
                    }
                    else if (board1[14].pipePosition == 1 && board1[13].pipePosition == 1 && board1[12].pipePosition == 1) {
                        return 5;
                    }
                }
            }
        }
        //for no path
        return 6;
    }

    public int CheckBoardTwo() {
        //for silver hooves
        if (board2[4].pipePosition == 0) {
            if (board2[0].pipePosition == 2 && board2[1].pipePosition == 0 && board2[7].pipePosition == 1 && board2[6].pipePosition == 3 && board2[9].pipePosition == 2 && board2[10].pipePosition == 0) {
                //wrong exit
                if (board2[13].pipePosition == 2 && board2[14].pipePosition == 0) {
                    return 4;
                    //right exit
                } else if (board2[13].pipePosition == 1 && board2[12].pipePosition == 3) {
                    return 1;
                }
            }
            //for gold or bronze
        } else if (board2[4].pipePosition == 1) {
            //for gold
            if (board2[3].pipePosition == 0 && board2[5].pipePosition == 0) {
                if (board2[0].pipePosition == 3 && board2[1].pipePosition == 1 && board2[8].pipePosition == 0 && board2[11].pipePosition == 1) {
                    //short path
                    if (board2[10].pipePosition == 0) {
                        //right exit
                        if (board2[13].pipePosition == 1 && board2[12].pipePosition == 3) {
                            return 0;
                            //wrong exit
                        } else if (board2[13].pipePosition == 2 && board2[14].pipePosition == 0) {
                            return 3;
                        }
                        //long path
                    } else if (board2[10].pipePosition == 1 && board2[7].pipePosition == 0 && board2[6].pipePosition == 3 && board2[9].pipePosition == 2) {
                        //right exit
                        if (board2[13].pipePosition == 1 && board2[12].pipePosition == 3) {
                            return 0;
                            //wrong exit
                        }
                        else if (board2[13].pipePosition == 2 && board2[14].pipePosition == 0) {
                            return 3;
                        }
                    }
                }
                //for bronze
            } else if (board2[3].pipePosition == 1 && board2[5].pipePosition == 1) {
                if (board2[2].pipePosition == 0 && board2[6].pipePosition == 2 && board2[7].pipePosition == 0) {
                    //right exit short
                    if (board2[10].pipePosition == 1 && board2[9].pipePosition == 3 && board2[12].pipePosition == 1) {
                        return 2;
                        //right exit long
                    } else if (board2[9].pipePosition == 3 && board2[10].pipePosition == 0 && board2[11].pipePosition == 0 && board2[12].pipePosition == 1 && board2[13].pipePosition == 2 && board2[14].pipePosition == 1) {
                        return 2;
                        //wrong exit short
                    } else if (board2[10].pipePosition == 0 && board2[11].pipePosition == 0 && board2[14].pipePosition == 2) {
                        return 5;
                        //wrong exit long
                    } else if (board2[9].pipePosition == 3 && board2[10].pipePosition == 1 && board2[11].pipePosition == 0 && board2[12].pipePosition == 2 && board2[13].pipePosition == 1 && board2[14].pipePosition == 2) {
                        return 5;
                    }
                }
            }
        }
        return 6;
    }

    public void ResetItems() {
        //hide antlers shownItem1
        RpcSetItemNumbers(shownItem1, shownItem2);
        if (shownItem1 != 6) {
            shownItem1GO = antlers[shownItem1];
            if (shownItem1 < 3) {
                RpcResetSetup(shownItem1GO.gameObject);
            }
            shownItem1GO.gameObject.GetComponent<HideObjects>().moveToPosition();
        }
        //hide hooves shownItem2
        if (shownItem2 != 6) {
            shownItem2GO = hooves[shownItem2];
            if (shownItem2 < 3) {
                RpcResetSetup(shownItem2GO.gameObject);
            }
            shownItem2GO.gameObject.GetComponent<HideObjects>().moveToPosition();
        }
    }

    [ClientRpc]
    void RpcPlayNoDispence() {
        noItemSound.Play();
    }

    [ClientRpc]
    void RpcPlayRightDispence() {
        particleRight.Play();
        rightSound.Play();
    }

    [ClientRpc]
    void RpcPlayWrongDispence() {
        particleWrong.Play();
        wrongSound.Play();
    }

    [ClientRpc]
    void RpcSetItemNumbers(int item1No, int item2No) {
        if (item1No != 6) {
            shownItem1 = item1No;
            shownItem1GO = antlers[shownItem1];
        }
        if (item2No != 6) {
            shownItem2 = item2No;
            shownItem2GO = hooves[shownItem2];
        }
    }

    [ClientRpc]
    void RpcParticleSetupReset() {
        if (shownItem1GO != null) {
            particleHead.gameObject.transform.position = shownItem1GO.gameObject.transform.position;
            particleHead.Play();
            headSound.Play();
        }

        if (shownItem2GO != null) {
            particleHoof.gameObject.transform.position = shownItem2GO.gameObject.transform.position;
            particleHoof.Play();
            hoofSound.Play();
        }

}

    [ClientRpc]
    void RpcResetSetup(GameObject item) {
        NetworkInteraction usNI = item.GetComponent<NetworkInteraction>();
        if (usNI.isInItem) {
            usNI.isInItem = false;
            usNI.holderItem.GetComponent<NetworkDependant>().isOccupied = false;
            usNI.holderItem.GetComponent<NetworkDependant>().isSolved = false;
            usNI.holderItem = null;
        }
        if (usNI.itemHeld) {
            item.GetComponent<NetworkIdentity>().RemoveClientAuthority();
            item.transform.parent = null;
            usNI.itemHeld = false;
        }
        Rigidbody itemRB = item.GetComponent<Rigidbody>();
        itemRB.useGravity = false;
        itemRB.isKinematic = true;
    }
}
