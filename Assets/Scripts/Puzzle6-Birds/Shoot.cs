using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shoot : NetworkBehaviour
{
    // Update is called once per frame
    public GameObject bow;
    public GameObject arrow;
    public GameObject arrowPrefab;
    public float pullAmount = 0;
    public float pullSpeed = 50;
    public bool arrowSlotted;
    public bool hideArrow;

    void Start()
    {
        SpawnArrow();    
    }
    void Update()
    {
        ShootLogic();
    }


    public void ShootLogic()
    {
        if (pullAmount > 100)
            pullAmount = 100;

        SkinnedMeshRenderer _bowSkin = bow.transform.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer _arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();
        Rigidbody _arrowRigidB = arrow.transform.GetComponent<Rigidbody>();
        ArrowAddForce _arrowProjectile = arrow.transform.GetComponent<ArrowAddForce>();

        if (Input.GetMouseButton(0))
        {
            pullAmount += Time.deltaTime * pullSpeed;
        }
        if (Input.GetMouseButtonUp(0))
        {
            arrowSlotted = false;
            _arrowRigidB.isKinematic = false;
            arrow.transform.parent = null;
            _arrowProjectile.shootForce *= ((pullAmount / 100) + 0.05f);
            pullAmount = 0;
            _arrowProjectile.enabled = true;
        }
        _bowSkin.SetBlendShapeWeight(0, pullAmount);
        _arrowSkin.SetBlendShapeWeight(0, pullAmount);
       
        if(Input.GetMouseButtonDown(0) && arrowSlotted == false)
        {
           // if (!isServer) return;
            SpawnArrow();
        }
    }

   
    void SpawnArrow()
    {
        arrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as GameObject;
        arrow.transform.parent = transform;
     //   NetworkServer.Spawn(arrow);
        arrowSlotted = true;

    }

}
