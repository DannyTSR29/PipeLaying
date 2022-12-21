using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public bool isCollide = false;
    private void OnCollisionEnter(Collision collision)
    {
        isCollide = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isCollide = false;
    }



}
