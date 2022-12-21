
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isPickUp;
    private IPickupAndDrop pipe;
    private float maxDistance = 10f;
    private Ray ray;
    private RaycastHit hit;
    
    public void PickUpObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (isPickUp == false)
        {
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
               pipe = hit.collider.GetComponent<IPickupAndDrop>();
               if (pipe != null)
               {
                    pipe.Pick();
                    isPickUp = true;        
               }
            }
        }
        
    }
    public void PlaceObject()
    {
        if (isPickUp == true)
         {
            if (pipe.DropDown())
            {
                isPickUp = false;
            }
         }
    }

    private void DropObject()
    {
        if (isPickUp == true)
        {
            pipe.Drop();
            isPickUp = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PickUpObject();
        }
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
        if (Input.GetMouseButtonDown(2))
        {
            DropObject();
        }
    }

}