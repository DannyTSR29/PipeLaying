using Cinemachine;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    //[SerializeField] private PickUp ispickup;
    //[SerializeField] private CinemachineFreeLook camThird;
    [SerializeField] private CinemachineVirtualCamera PipeCam;
    [SerializeField] private PickUp pickUp;
    //[SerializeField] private CinemachinePOV PipeCam;
    //[SerializeField] private Camera PipeCam;
    //private Vector3 rotation;

    private void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            PipeCam.enabled = true;
        }
        else
        {
            if (CheckPipeStatus())
            {
                PipeCam.enabled = false;
            }
            else
            {
                PipeCam.enabled = true;
            }
        }
    }
    private bool CheckPipeStatus()
    {
        if (pickUp.isPickUp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
