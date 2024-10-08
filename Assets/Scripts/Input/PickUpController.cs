using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using KeyElements;
using Resources.Scripts;
 using UnityEngine;

public class PickUpController : MonoBehaviour, IInterctable
{
    public GameObject player;
    public Transform holdPos;
    
    public float pickUpRange = 5f; 
    private float rotationSensitivity = 1f;
    public GameObject heldObj;
    private Rigidbody heldObjRb;
    private int LayerNumber;
    
    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    public void StopPickUp()
    {
        DropObject();
    }
    
    void DropObject()
    {
        Debug.Log("some");
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
    }

    public virtual void Interact(GameObject gameObject)
    {
        if (heldObj == null)
        {
            heldObj = this.gameObject;
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.transform.DOMove(holdPos.transform.position,0.25f);
            heldObj.transform.rotation = holdPos.transform.rotation;
            heldObj.layer = LayerNumber;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
        else
        {
            DropObject();
        }
        
    }
}
