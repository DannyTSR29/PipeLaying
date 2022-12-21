using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //private Ray ray;
    //private RaycastHit hit;
    //private float maxDistance = 10f;
    [SerializeField] Transform objectPooler;
    [SerializeField] PickUp pickup;

    //private void detect()
    //{
    //    if(Physics.Raycast(ray, out hit, maxDistance))
    //    {
    //        IPickupAndDrop pipe = hit.collider.GetComponent<IPickupAndDrop>();
    //        if (pipe != null)
    //        {
    //            hit.collider.gameObject.SetActive(false);
    //        }
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        IPickupAndDrop pipe = collision.collider.GetComponent<IPickupAndDrop>();
        if (pipe != null)
        {
            collision.collider.transform.parent = objectPooler;
            collision.collider.GetComponent<PickDrop>().isPickUp = false;
            collision.collider.GetComponent<CollisionDetect>().isCollide = false;
            collision.collider.isTrigger = true;
            collision.collider.attachedRigidbody.isKinematic = true;
            collision.collider.gameObject.layer = 0;
            collision.collider.gameObject.SetActive(false);
            pickup.isPickUp = false;
        }
    }

    private void Start()
    {

    }
}
