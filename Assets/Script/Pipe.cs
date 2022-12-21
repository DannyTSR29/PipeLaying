using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class Pipe : MonoBehaviour, IConnect
{
    [Serializable]
    public class ArrayClass
    {
        public bool surfaceRay;
        public bool surfaceRayCheck;
        public bool surfaceColour;
        public GameObject cubeColour;
        public float rayDistance = 0.0f;

    }
    public ArrayClass[] arrayClass;
    public int totalNumOfSurRay = 0;
    private int cubeColourStore1 = 0;
    private int cubeColourStore2 = 0;
    private int cubeColourStore3 = 0;
    private int a = 0;
    private int b = 0;
    private float threshold = 0.0f;
    //private float heightY;
    //private float angle = 0.0f;
    private float length = 50f;
    private bool cubeColourTrueChecked = false;
    private bool colourCheck = false;
    private bool surfaceCheck = false;
    private bool hitColliderPipe = false;
    private bool hitColliderPipe3 = false;
    private bool hitColliderLazer = false;
    //private Quaternion Quaternion_Rotate_From;
    //private Quaternion Quaternion_Rotate_To;
    RaycastHit objectHit;
    //RaycastHit magtedHit;
    Vector3[] direction = new Vector3[6];
    Vector3 fwd;
    Vector3 lastPostition;
    Vector3 offset;
    //Vector3 magnetFwd;
    //Vector3 startPos;
    //Vector3 endPos;
    //LineRenderer lazer;
    //[SerializeField] private float rayMagnet = 0.0f;
    //[SerializeField] public bool pullBtn = false; // Trigger
    //[SerializeField] private bool pushBtn = false;
    //[SerializeField] private bool move = false;
    //[SerializeField] private float distanceStopPull = 0.0f;
    //[SerializeField] private float distanceStopPush = 0.0f;
    //[SerializeField] private float speedPushPull = 0.0f;
    //[SerializeField] private float speedUpDown = 0.0f;
    //[SerializeField] private float Rotation_Speed;
    //[SerializeField] private float Rotation_Friction;
    //[SerializeField] private float Rotation_Smoothness;
    Pipe referencePipe;
    Pipe referencePipe3;
    Pipe referenceLazer;

    // Start is called before the first frame update
    void Start()
    {
        CheckTypeOfPipe();
        //lazer = this.gameObject.GetComponent<LineRenderer>();
        //lazer.enabled = false;
        lastPostition = this.gameObject.transform.position;
        direction[0] = Vector3.forward;
        direction[1] = Vector3.back;
        direction[2] = Vector3.up;
        direction[3] = Vector3.down;
        direction[4] = Vector3.right;
        direction[5] = Vector3.left;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //PipeConnectCheck();
        Run();
    }

    private void CheckTypeOfPipe()
    {
        for (int i = 0; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceRay == true)
            {
                totalNumOfSurRay++;
            }
        }
    }

    private void Run()
    {
        if(totalNumOfSurRay > 0 && totalNumOfSurRay <= 2)
        {
            WorkPipe();
        }

        else if(totalNumOfSurRay >= 3 && totalNumOfSurRay < 6)
        {
            WorkPipe3();
        }

        else if(totalNumOfSurRay == 6)
        {          
            WorkPipeLazer();
        }
    }

    // Only For More than 0 and less than 3 Surface
    private void WorkPipe()
    {
        Connect();
        ChildColourChange();
        PipeMainColourChange();
        if (PipeMainColourChange() == true)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 1, 1);
        }

        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }

    }

    // Only For More than 2 and less than 6 Surface
    private void WorkPipe3()
    {
        Connect();
        ChildColourChange();
        Pipe3MainColourChange();
        if (Pipe3MainColourChange() == true)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 1, 1);
        }

        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
    }

    // Only For 6 Surface 
    private void WorkPipeLazer()
    {
        LaserConnect();
        ChildColourChange();
        PipeLaserMainColourChange();
        if (PipeLaserMainColourChange() == true)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 1, 1);
        }

        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
        }
    }

    // Only For Normal Pipe (2 Surface) || >2 Surface && <6 Surface Pipe
    private void Connect()
    {
        if (arrayClass[0].surfaceRay == true)
        {
            PipeCheck(0, 1);
        }

        if (arrayClass[1].surfaceRay == true)
        {
            PipeCheck(1, 0);
        }

        if (arrayClass[2].surfaceRay == true)
        {
            PipeCheck(2, 3);
        }

        if (arrayClass[3].surfaceRay == true)
        {
            PipeCheck(3, 2);
        }

        if (arrayClass[4].surfaceRay == true)
        {
            PipeCheck(4, 5);
        }

        if (arrayClass[5].surfaceRay == true)
        {
            PipeCheck(5, 4);

        }

    }

    // Only For 6 Surface Pipe
    private void LaserConnect()
    {
        if (arrayClass[0].surfaceRay == true)
        {
            PipeLazerCheck(0, 1);
        }

        if (arrayClass[1].surfaceRay == true)
        {
            PipeLazerCheck(1, 0);
        }

        if (arrayClass[2].surfaceRay == true)
        {
            PipeLazerCheck(2, 3);
        }

        if (arrayClass[3].surfaceRay == true)
        {
            PipeLazerCheck(3, 2);
        }

        if (arrayClass[4].surfaceRay == true)
        {
            PipeLazerCheck(4, 5);
        }

        if (arrayClass[5].surfaceRay == true)
        {
            PipeLazerCheck(5, 4);

        }
    }

    // Only For Normal Pipe (2 Surface) || >2 Surface && <6 Surface Pipe
    public void PipeCheck(int a, int b)
    {
        fwd = this.gameObject.transform.TransformDirection(direction[a]);
        Debug.DrawRay(this.gameObject.transform.position, fwd * arrayClass[a].rayDistance, Color.green);
        if (Physics.Raycast(this.gameObject.transform.position, fwd, out objectHit, arrayClass[a].rayDistance))
        {
            if (objectHit.collider.tag == "Untagger")
            {
                Debug.Log(".");
            }

            else
            {
                if (objectHit.collider.GetComponent<Pipe>() != null)
                {
                    if (objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay > 0 && objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay <= 2 && objectHit.collider.GetComponent<Pipe>().arrayClass[b].surfaceRay == true)
                    {
                        arrayClass[a].surfaceRayCheck = false;
                        arrayClass[a].surfaceColour = false;
                        referencePipe = objectHit.collider.GetComponent<Pipe>();
                        hitColliderPipe = true;

                    }

                    else if (objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay >= 3 && objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay < 6 && objectHit.collider.GetComponent<Pipe>().arrayClass[b].surfaceRay == true)
                    {
                        arrayClass[a].surfaceRayCheck = false;
                        arrayClass[a].surfaceColour = false;
                        referencePipe3 = objectHit.collider.GetComponent<Pipe>();
                        hitColliderPipe3 = true;
                    }

                    else if (objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay == 6)
                    {
                        arrayClass[a].surfaceRayCheck = false;
                        arrayClass[a].surfaceColour = false;
                        referenceLazer = objectHit.collider.GetComponent<Pipe>();
                        referenceLazer.arrayClass[b].surfaceRayCheck = false;
                        referenceLazer.arrayClass[b].surfaceColour = false;
                        hitColliderLazer = true;
                    }
                }
                // Change Colour After Move Pipe
                if (hitColliderPipe == true && PipePositionChecking() == true)
                {
                    this.arrayClass[a].surfaceColour = true;
                    this.arrayClass[a].surfaceRayCheck = true;
                    StartCoroutine(ChangeColour());
                    referencePipe.arrayClass[b].surfaceColour = true;
                    referencePipe.arrayClass[b].surfaceRayCheck = true;
                }

                else if (hitColliderPipe3 == true && PipePositionChecking() == true)
                {
                    this.arrayClass[a].surfaceColour = true;
                    this.arrayClass[a].surfaceRayCheck = true;
                    StartCoroutine(ChangeColour());
                    referencePipe3.arrayClass[b].surfaceColour = true;
                    referencePipe3.arrayClass[b].surfaceRayCheck = true;
                }

                else if (hitColliderLazer == true && PipePositionChecking() == true)
                {
                    this.arrayClass[a].surfaceColour = true;
                    this.arrayClass[a].surfaceRayCheck = true;
                    StartCoroutine(ChangeColour());
                    referenceLazer.arrayClass[b].surfaceRayCheck = true;
                    referenceLazer.arrayClass[b].surfaceColour = true;
                }
            }
        }
    }

    // Only For 6 Surface Pipe
    public void PipeLazerCheck(int a, int b)
    {
        fwd = this.gameObject.transform.TransformDirection(direction[a]);
        Debug.DrawRay(this.gameObject.transform.position, fwd * arrayClass[a].rayDistance, Color.blue);
        if (Physics.Raycast(this.gameObject.transform.position, fwd, out objectHit, arrayClass[a].rayDistance))
        {
            if (objectHit.collider.tag == "Untagged")
            {
                Debug.Log("Block");
            }

            else
            {
                if (objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay == 6 && objectHit.collider.GetComponent<Pipe>().arrayClass[b].surfaceRay == true)
                {
                    //length = Vector3.Distance(this.gameObject.transform.position, objectHit.collider.transform.position);
                    //length -= 2f;
                    //Vector3 endPosition = fwd + (length * direction[a]);
                    arrayClass[a].surfaceRayCheck = false;
                    arrayClass[a].surfaceColour = false;
                    //lazer.SetPosition(0, fwd);
                    //lazer.SetPosition(1, endPosition);
                    //lazer.material.color = Color.green;
                    //lazer.enabled = true;
                    referenceLazer = objectHit.collider.GetComponent<Pipe>();
                    hitColliderLazer = true;
                }

                
            }
        }
        if (hitColliderLazer == true && PipePositionChecking() == true)
        {
            //lazer.enabled = false;
            //referenceLazer.lazer.enabled = false;
            for (int i = 0; i < arrayClass.Length; i++)
            {
                arrayClass[i].surfaceColour = true;
                arrayClass[i].surfaceRayCheck = true;

                referenceLazer.arrayClass[i].surfaceColour = true;
                referenceLazer.arrayClass[i].surfaceRayCheck = true;
            }

        }
    }


    private void ChildColourChange()
    {
        for (int i = 0; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == false)
            {
                arrayClass[i].cubeColour.gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 1, 1);
            }
        }

        for (int i = 0; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == true)
            {
                arrayClass[i].cubeColour.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            }
        }
    }

    private bool PipeMainColourChange()
    {
        for (int i = 0; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == true)
            {
                cubeColourStore1 = i;
                break;
            }
        }

        for (int i = a + 1; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == true)
            {
                cubeColourStore2 = i;
                cubeColourTrueChecked = true;
                break;
            }
        }

        if (cubeColourTrueChecked == true)
        {
            if (arrayClass[cubeColourStore1].surfaceColour == false)
            {
                colourCheck = true;
            }

            else
            {
                colourCheck = false;
            }
        }

        if (colourCheck == true)
        {
            if (arrayClass[cubeColourStore2].surfaceColour == false)
            {
                return true;
            }
        }

        return false;
    }

    private bool Pipe3MainColourChange()
    {
        for (int i = 0; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == true)
            {
                cubeColourStore1 = i;
                break;
            }
        }

        for (int i = cubeColourStore1 + 1; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == true)
            {
                cubeColourStore2 = i;
                break;
            }
        }

        for (int i = cubeColourStore2 + 1; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == true)
            {
                cubeColourStore3 = i;
                cubeColourTrueChecked = true;
                break;
            }
        }

        if (cubeColourTrueChecked == true)
        {
            if (arrayClass[cubeColourStore1].surfaceColour == false || arrayClass[cubeColourStore2].surfaceColour == false || arrayClass[cubeColourStore2].surfaceColour == false)
            {
                colourCheck = true;
                if (arrayClass[cubeColourStore1].surfaceColour == false)
                {
                    arrayClass[cubeColourStore1].surfaceColour = arrayClass[cubeColourStore2].surfaceColour;
                }

                else if (arrayClass[cubeColourStore2].surfaceColour == false)
                {
                    arrayClass[cubeColourStore2].surfaceColour = arrayClass[cubeColourStore3].surfaceColour;
                }

                else if (arrayClass[cubeColourStore3].surfaceColour == false)
                {
                    arrayClass[cubeColourStore3].surfaceColour = arrayClass[cubeColourStore1].surfaceColour;
                }
            }

            else
            {
                colourCheck = false;
            }
        }

        if (colourCheck == true)
        {
            if (arrayClass[cubeColourStore1].surfaceColour == false || arrayClass[cubeColourStore2].surfaceColour == false || arrayClass[cubeColourStore2].surfaceColour == false)
            {
                return true;
            }
        }

        return false;
    }

    private bool PipeLaserMainColourChange()
    {
        for (int i = 0; i < arrayClass.Length; i++)
        {
            if (arrayClass[i].surfaceColour == false)
            {
                a = i;
                colourCheck = true;
                break;
            }
        }

        if (colourCheck == true)
        {
            for (int i = a + 1; i < arrayClass.Length; i++)
            {
                if (arrayClass[i].surfaceColour == false)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool PipeConnectCheck()
    {
        if (totalNumOfSurRay > 0 && totalNumOfSurRay <= 2)
        {
            for (int i = 0; i < arrayClass.Length; i++)
            {
                if (arrayClass[i].surfaceRayCheck == true)
                {
                    return false;
                }
            }

        }

        else if (totalNumOfSurRay >= 3 && totalNumOfSurRay < 6)
        {
            for (int i = 0; i < arrayClass.Length; i++)
            {
                if (arrayClass[i].surfaceRayCheck == true)
                {
                    b = i;
                    surfaceCheck = true;
                    break;
                }
            }

            if (surfaceCheck == true)
            {
                for (int i = b + 1; i < arrayClass.Length; i++)
                {
                    if (arrayClass[i].surfaceRayCheck == true)
                    {
                        return false;
                    }
                }
            }
        }

        else if (totalNumOfSurRay == 6)
        {
            for (int i = 0; i < arrayClass.Length; i++)
            {
                if (arrayClass[i].surfaceRayCheck == false)
                {
                    b = i;
                    surfaceCheck = true;
                    break;
                }
            }

            if (surfaceCheck == true)
            {
                for (int i = b + 1; i < arrayClass.Length; i++)
                {
                    if (arrayClass[i].surfaceRayCheck == false)
                    {
                        return true;
                    }
                }
            }

        }
        return true;

    }

    //public void PipeLaserConnectCheck()
    //{
    //   else if (totalNumOfSurRay == 6)
    //    {
    //        for (int i = 0; i < arrayClass.Length; i++)
    //        {
    //            if (arrayClass[i].surfaceRayCheck == false)
    //            {
    //                b = i;
    //                surfaceCheck = true;
    //                break;
    //            }
    //        }

    //        if (surfaceCheck == true)
    //        {
    //            for (int i = b + 1; i < arrayClass.Length; i++)
    //            {
    //                if (arrayClass[i].surfaceRayCheck == true)
    //                {
    //                    return false;
    //                }

    //                else
    //                {
    //                    return true;
    //                }
    //            }
    //        }

    //        return false;
    //    }
    //}
    private bool PipePositionChecking()
    {
        offset = this.gameObject.transform.position - lastPostition;
        if (offset.x > threshold || offset.y > threshold || offset.z > threshold)
        {
            lastPostition = this.gameObject.transform.position;
            return true;
        }

        if (offset.x < threshold || offset.y < threshold || offset.z < threshold)
        {
            lastPostition = this.gameObject.transform.position;
            return true;
        }
        return false;
    }

    //private void MagnetPull(int a)
    //{
    //    magnetFwd = this.gameObject.transform.TransformDirection(direction[a]);
    //    Debug.DrawRay(this.gameObject.transform.position, magnetFwd * rayMagnet, Color.red);
    //    if (Physics.Raycast(this.gameObject.transform.position, magnetFwd, out magtedHit, rayMagnet))
    //    {
    //        if (magtedHit.collider.tag == "Untagged")
    //        {
    //            Debug.Log("Unable to move Object!");
    //        }

    //        else
    //        {
    //            angle = Vector3.Angle(this.gameObject.transform.position, magtedHit.collider.transform.position);
    //            heightY = this.gameObject.transform.position.y - magtedHit.collider.transform.position.y;
    //            Quaternion_Rotate_From = magtedHit.collider.transform.rotation;
    //            startPos = magtedHit.collider.transform.localPosition;

    //            if (angle > 20)
    //            {
    //                Quaternion_Rotate_To = Quaternion.Euler(0, 0, -5f);
    //                magtedHit.collider.transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);

    //            }

    //            else if (angle < 20)
    //            {
    //                Quaternion_Rotate_To = Quaternion.Euler(0, 0, 5f);
    //                magtedHit.collider.transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
    //            }

    //            if (Vector3.Distance(this.gameObject.transform.position, magtedHit.collider.transform.position) < distanceStopPull)
    //            {
    //                move = false;

    //                Quaternion_Rotate_To = Quaternion.Euler(0, 0, 0);
    //                magtedHit.collider.transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);

    //                if (heightY > 0 || heightY < 0)
    //                {
    //                    startPos.y = Mathf.Lerp(magtedHit.collider.transform.localPosition.y, magtedHit.collider.transform.localPosition.y + heightY, Time.deltaTime * speedUpDown);
    //                    magtedHit.collider.transform.localPosition = startPos;
    //                }

                   
    //            }

    //            else
    //            {
    //                move = true;

    //            }


    //            if (move == true)
    //            {
    //                //currentLerpTime += Time.deltaTime;
    //                //if (currentLerpTime >= lerpTime)
    //                //{
    //                //    currentLerpTime = lerpTime;
    //                //}
    //                //lerpPercentage = currentLerpTime / lerpTime;
    //                //startPos = magtedHit.collider.transform.position.y;
    //                //endPos = startPos - direction[a] * distance;
    //                //magtedHit.collider.transform.position = Vector3.Lerp(startPos, endPos, lerpPercentage);
    //                magtedHit.collider.GetComponent<Rigidbody>().velocity = (-direction[a] * speedPushPull);

    //            }

    //            else
    //            {
    //                magtedHit.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //            }
    //        }
    //    }

    //}

    //private void MagnetPush(int a)
    //{
    //    magnetFwd = this.gameObject.transform.TransformDirection(direction[a]);
    //    Debug.DrawRay(this.gameObject.transform.position, magnetFwd * rayMagnet, Color.red);
    //    if (Physics.Raycast(this.gameObject.transform.position, magnetFwd, out magtedHit, rayMagnet))
    //    {
    //        if (magtedHit.collider.tag == "Untagged")
    //        {
    //            Debug.Log("Unable to move Object!");
    //        }

    //        else
    //        {
               
    //            if (Vector3.Distance(this.gameObject.transform.position, magtedHit.collider.transform.position) > distanceStopPush)
    //            {
    //                move = false;
                
    //            }

    //            else
    //            {
    //                move = true;

    //            }


    //            if (move == true)
    //            {                  
    //                // Do Reference!!!
    //                magtedHit.collider.GetComponent<Rigidbody>().velocity = (direction[a] * speedPushPull);

    //            }

    //            else
    //            {
    //                magtedHit.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //            }
    //        }
    //    }

    //}

    IEnumerator ChangeColour()
    {
        yield return new WaitForSeconds(1f);
    }
}
   
