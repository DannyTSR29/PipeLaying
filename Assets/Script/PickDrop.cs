using System.Collections;
using UnityEngine;

public class PickDrop : MonoBehaviour,IPickupAndDrop
{
    [SerializeField]private GameObject Player;
    [SerializeField]private Transform AttachPoint;
    private Vector3 hitPos;
    private Vector3 angleVector;
    private Vector3 currentRotation;
    private Vector3 parentForward;
    private Vector3 thisForward;
    private Vector3 thisUpward;
    private Vector3 thisRight;
    private float maxDistance = 500f;
    private float angle;
    private float speed = 0.5f;
    private float threshold = 0.5f;
    private int i;
    private bool canRaycast;
    private bool surfaceCheck;
    public bool isRotating = false;
    public bool isPickUp = false;
    private Ray ray;
    private RaycastHit hit;
    private IPickupAndDrop pipe;
    private Pipe checkPipe;
    private CollisionDetect detect;
    private PickUp pickUp;

    public bool Pick()
    {
        AngleCheck();
        PickUp();
        return true;
    }

    public bool DropDown()
    {
        if (isPickUp)
        {
           SurfaceCheck(); 
           if (Raycast())
           {
                Position(hitPos);
              if (surfaceCheck == true)
              {
                  return true; 
              }
              else
              {
               return false;
              }
           }
           else
           {
                return false;
           }
        }
        else
        {
            return false;
        }
    }

    public void Drop()
    {
        transform.parent = null;
        gameObject.layer = 0;
        isPickUp = false;
    }
  
    private void SurfaceCheck()
    {
        surfaceCheck = false; 
        canRaycast = false;
        if (checkPipe.arrayClass[i].surfaceRay == true) //checkPipe.surfaceClose[i] == true
        {
            canRaycast = true;
        }

        if (canRaycast)
        {
            ray = new Ray(transform.position, transform.parent.forward);
        }
    }

    private void RoundOff()
    {
        parentForward.x = Mathf.Round(transform.parent.forward.x);
        parentForward.y = Mathf.Round(transform.parent.forward.y);
        parentForward.z = Mathf.Round(transform.parent.forward.z);
        thisForward.x = Mathf.Round(transform.forward.x);
        thisForward.y = Mathf.Round(transform.forward.y);
        thisForward.z = Mathf.Round(transform.forward.z);
        thisUpward.x = Mathf.Round(transform.up.x);
        thisUpward.y = Mathf.Round(transform.up.y);
        thisUpward.z = Mathf.Round(transform.up.z);
        thisRight.x = Mathf.Round(transform.right.x);
        thisRight.y = Mathf.Round(transform.right.y);
        thisRight.z = Mathf.Round(transform.right.z);
    }

    private void RaycastCheck()
    {
        RoundOff();
        if (parentForward == thisForward)
        {
            i = 0;
        }
        else if (parentForward == -thisForward)
        {
            i = 1; 
        }
        else if (parentForward == thisUpward)
        {
            i = 2; 
        }
        else if (parentForward == -thisUpward)
        {
            i = 3; ;
        }
        else if (parentForward == thisRight)
        {
            i = 4; 
        }
        else if (parentForward == -thisRight)
        {
            i = 5; 
        }
        else
        {
            Debug.Log("RaycastCheck Error : " + i);
        }
}

    private void GetRotationInput()
    {
        if(isPickUp && Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > threshold)
            {
                if (!isRotating)
                {
                    //angleRotate = Vector3.up * -90;
                    //angleRotateQ = Quaternion.AngleAxis(-90, transform.parent.up);
                    //angleRotate = Quaternion.FromToRotation(transform.parent.forward, -transform.parent.right);
                    StartCoroutine(RotateMe(Vector3.down, speed));
                }
            }
            else if (Input.GetAxis("Mouse X") < -threshold)
            {
                if (!isRotating)
                {
                    //angleRotate = Vector3.up * 90;
                    //angleRotateQ = Quaternion.AngleAxis(90, transform.parent.up);
                    //angleRotate = Quaternion.FromToRotation(transform.parent.forward, transform.parent.right);
                    StartCoroutine(RotateMe(Vector3.up, speed));
                }
            }
            else if (Input.GetAxis("Mouse Y") > threshold)
            {
                if (!isRotating)
                {
                    //if (Mathf.Approximately(Mathf.Round(transform.localEulerAngles.y), 90))
                    //{
                    //    angleRotate = Vector3.forward * 90;
                    //}
                    //else if (Mathf.Approximately(Mathf.Round(transform.localEulerAngles.y), 270))
                    //{
                    //    angleRotate = Vector3.forward * -90;
                    //}
                    //else if (Mathf.Approximately(Mathf.Round(transform.localEulerAngles.y), 180))
                    //{
                    //    angleRotate = Vector3.right * -90;
                    //}
                    //else
                    //{
                    //    angleRotate = Vector3.right * 90;
                    //}
                    //angleRotate = Vector3.left * 90;
                    //angleRotateQ = Quaternion.AngleAxis(90, transform.parent.right);
                    //angleRotate = Quaternion.FromToRotation(transform.parent.forward, -transform.parent.up);
                    StartCoroutine(RotateMe(AttachPoint.transform.right, speed));
                }
            }
            else if (Input.GetAxis("Mouse Y") < -threshold)
            {
                if (!isRotating)
                {
                    //if (Mathf.Approximately(Mathf.Round(transform.localEulerAngles.y), 90))
                    //{
                    //    angleRotate = Vector3.forward * -90;
                    //}
                    //else if (Mathf.Approximately(Mathf.Round(transform.localEulerAngles.y), 270))
                    //{
                    //    angleRotate = Vector3.forward * 90;
                    //}
                    //else if (Mathf.Approximately(Mathf.Round(transform.localEulerAngles.y), 180))
                    //{
                    //    angleRotate = Vector3.right * 90;
                    //}
                    //else
                    //{
                    //    angleRotate = Vector3.right * -90;
                    //}
                    //angleRotate = Quaternion.AngleAxis(-90, transform.parent.right);
                    //angleRotate = Vector3.right * -90;
                    //angleRotate = Quaternion.FromToRotation(transform.parent.forward, -transform.parent.up);
                    //angleRotate = Vector3.left * 90;
                    StartCoroutine(RotateMe(-AttachPoint.transform.right, speed));
                }
            }
            else
            {

            }
        }
        //if (Input.GetKeyDown("left"))
        //{
        //    StartCoroutine(RotateMe(Vector3.up * 90, _speed));
        //}
        //else if (Input.GetKeyDown("right"))
        //{
        //    StartCoroutine(RotateMe(Vector3.up * -90, _speed));

        //}
        //else if (Input.GetKeyDown("up"))
        //{
        //    StartCoroutine(RotateMe(Vector3.right * 90, _speed));

        //}
        //else if (Input.GetKeyDown("down"))
        //{
        //    StartCoroutine(RotateMe(Vector3.right * -90, _speed));
        //}
    }


    IEnumerator RotateMe(Vector3 axis, float inTime)//Vector3 byAngles
    {
        //isRotating = true;
        currentRotation = transform.localEulerAngles;
        //Vector3 toAngle = Vector3.one;
        //Quaternion fromAngle = transform.localRotation;
        //Quaternion toAngle = Quaternion.Euler(axis * 90);
        //Quaternion toAngle = transform.localRotation * Quaternion.AngleAxis(90, axis);
        for (float t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            isRotating = true;
            //transform.localEulerAngles = Vector3.Lerp(currentRotation, currentRotation + byAngles, t);
            //transform.localRotation = Quaternion.Lerp(fromAngle, toAngle, t);
            transform.RotateAround(transform.position, axis, 90 * Time.deltaTime / inTime);
            yield return null;
        }
        //transform.RotateAround(transform.position, axis, 90);
        transform.localEulerAngles = new Vector3(Mathf.Round(transform.localEulerAngles.x / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.y / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.z / 90) * 90);
        //currentRotation = transform.localEulerAngles;
        //transform.localRotation = fromAngle * byAngles;
        isRotating = false;
    }

    private bool Raycast()
    {
        if (canRaycast)
        {
            if(Physics.Raycast(ray, out hit, maxDistance))
            {
                pipe = hit.collider.GetComponent<IPickupAndDrop>();
                Debug.Log("Raycast");
                if (pipe != null)
                {
                    hitPos = hit.transform.InverseTransformPoint(hit.point);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Position(Vector3 hitpos)
    {
        if (hitpos.x == 0.5)
        {
            if (hit.collider.gameObject.GetComponent<Pipe>().arrayClass[4].surfaceRay == true)//GetComponent<Pipe>().surfaceClose[4]
            {
                transform.position = hit.transform.position + hit.transform.right;
                //transform.eulerAngles = _hit.transform.right;
                //transform.eulerAngles = _hit.normal;
                transform.parent = hit.transform;
                //transform.localEulerAngles = currentRotation;
                //transform.Rotate(0f, 270f, 0f, Space.World);
                //transform.RotateAround(transform.position, Vector3.up, 270f);
                transform.localEulerAngles = new Vector3(Mathf.Round(transform.localEulerAngles.x / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.y / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.z / 90) * 90);
                surfaceCheck = true;
            }
        }
        else if (hitpos.x == -0.5)
        {
            if (hit.collider.gameObject.GetComponent<Pipe>().arrayClass[5].surfaceRay == true)
            {
                transform.position = hit.transform.position - hit.transform.right;
                //transform.eulerAngles = -_hit.transform.right;
                transform.parent = hit.transform;
                //transform.localEulerAngles = currentRotation;
                //transform.Rotate(0f, 90f, 0f, Space.World);
                //transform.RotateAround(transform.position, Vector3.up, 90f);
                transform.localEulerAngles = new Vector3(Mathf.Round(transform.localEulerAngles.x / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.y / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.z / 90) * 90);
                surfaceCheck = true;
            }
        }
        else if (hitpos.z == -0.5)
        {
            if (hit.collider.gameObject.GetComponent<Pipe>().arrayClass[1].surfaceRay == true)
            {
                transform.position = hit.transform.position - hit.transform.forward;
                transform.parent = hit.transform;
                //transform.localEulerAngles = currentRotation;
                transform.localEulerAngles = new Vector3(Mathf.Round(transform.localEulerAngles.x / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.y / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.z / 90) * 90);
                surfaceCheck = true;
            }
        }
        else if (hitpos.z == 0.5)
        {
            if (hit.collider.gameObject.GetComponent<Pipe>().arrayClass[0].surfaceRay == true)
            {
                transform.position = hit.transform.position + hit.transform.forward; 
                //transform.eulerAngles = _hit.transform.forward;
                transform.parent = hit.transform;
                //transform.localEulerAngles = currentRotation;
                //transform.Rotate(0f, 180f, 0f, Space.World);
                //transform.RotateAround(transform.position, Vector3.up, 180f);
                transform.localEulerAngles = new Vector3(Mathf.Round(transform.localEulerAngles.x / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.y / 90) * 90,
                                                 Mathf.Round(transform.localEulerAngles.z / 90) * 90);
                surfaceCheck = true;
            }
        }
        else if (hitpos.y == 0.5)
        {
            if (hit.collider.gameObject.GetComponent<Pipe>().arrayClass[2].surfaceRay == true)
            {
                transform.position = hit.transform.position + hit.transform.up; 
                //transform.eulerAngles = _hit.transform.up;
                transform.parent = hit.transform;
                //transform.localEulerAngles = currentRotation;
                transform.Rotate(90f, 0f, 0f, Space.World);
                surfaceCheck = true;
            }
        }
        else if (hitpos.y == -0.5)
        {
            if (hit.collider.gameObject.GetComponent<Pipe>().arrayClass[3].surfaceRay == true)
            {
                transform.position = hit.transform.position - hit.transform.up; 
                //transform.eulerAngles = -_hit.transform.up;
                transform.parent = hit.transform;
                //transform.localEulerAngles = currentRotation;
                transform.Rotate(270f, 0f, 0f, Space.World);
                surfaceCheck = true;
            }
        }
        if (surfaceCheck == true)
        {
            transform.parent = null;
            gameObject.layer = 0;
            isPickUp = false;
        }
    }

    private void PickUp()
    {
        //transform.parent = Player.transform;
        transform.parent = AttachPoint;
        transform.position = AttachPoint.position;
        SetAngle();
        gameObject.layer = 2;
        isPickUp = true;
    }

    private void AngleCheck()
    {
        angleVector = Player.transform.position - transform.position;
        angle = Vector3.SignedAngle(transform.forward, angleVector, Vector3.up);
    }

    private void SetAngle()
    {
        if (angle > -45 && angle < 45)
        {
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (angle >= 45 && angle <= 135)
        {
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (angle > 135 || angle <= -135)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (angle >= -135 && angle <= -45)
        {
            transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
        }
        else
        {
            Debug.Log("Error");
        }
    }


    private void Start()
    {
        checkPipe = GetComponent<Pipe>();
        detect = GetComponent<CollisionDetect>();
        pickUp = Player.GetComponent<PickUp>();
    }

    private void Update()
    {
        if (detect.isCollide)
        {
            isPickUp = false;
            pickUp.isPickUp = isPickUp;
            transform.parent = null;
            gameObject.layer = 0;
        }
        if (transform.parent == AttachPoint)//_Player.transform
        {
            isPickUp = true;
            pickUp.isPickUp = isPickUp;
        }
        if (isPickUp == true)
        {
            GetRotationInput();

            RaycastCheck();
        }
        
    }

}