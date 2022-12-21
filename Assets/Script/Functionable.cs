using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public abstract class Functionable : MonoBehaviour
{
    public abstract void RunFunction();
}

class MagnetPull : Functionable
{
    RaycastHit magtedHit;
    GameObject gameObject;
    Vector3 magnetFwd;
    Vector3 startPos;
    Vector3[] direction = new Vector3[6];
    private bool move = false;
    private int surfaceRay;
    private float heightY;
    private float angle;
    private float rayMagnet;
    private float distanceStopPull;
    private float speedPushPull;
    private float speedUpDown;
    private float Rotation_Speed;
    private float Rotation_Friction;
    private float Rotation_Smoothness;
    private Quaternion Quaternion_Rotate_From;
    private Quaternion Quaternion_Rotate_To;

    public MagnetPull(RaycastHit magtedHit, GameObject gameObject, Vector3 magnetFwd, Vector3 startPos, Vector3 direction0, Vector3 direction1, Vector3 direction2, Vector3 direction3, Vector3 direction4, Vector3 direction5,/* bool move, */int surfaceRay,/* float heightY, float angle,*/ float rayMagnet, float distanceStopPull, float speedPushPull, float speedUpDown, float Rotation_Speed, float Rotation_Friction, float Rotation_Smoothness, Quaternion Quaternion_Rotate_From, Quaternion Quaternion_Rotate_To)
    {
        this.magtedHit = magtedHit;
        this.gameObject = gameObject;
        this.magnetFwd = magnetFwd;
        this.startPos = startPos;
        this.direction[0] = direction0;
        this.direction[1] = direction1;
        this.direction[2] = direction2;
        this.direction[3] = direction3;
        this.direction[4] = direction4;
        this.direction[5] = direction5;
        //this.move = move;
        this.surfaceRay = surfaceRay;
        //this.heightY = heightY;
        //this.angle = angle;
        this.rayMagnet = rayMagnet;
        this.distanceStopPull = distanceStopPull;
        this.speedPushPull = speedPushPull;
        this.speedUpDown = speedUpDown;
        this.Rotation_Speed = Rotation_Speed;
        this.Rotation_Friction = Rotation_Friction;
        this.Rotation_Smoothness = Rotation_Smoothness;
        this.Quaternion_Rotate_From = Quaternion_Rotate_From;
        this.Quaternion_Rotate_To = Quaternion_Rotate_To;
    }

    public override void RunFunction()
    {
        magnetFwd = this.gameObject.transform.TransformDirection(direction[surfaceRay]);
        Debug.DrawRay(this.gameObject.transform.position, magnetFwd * rayMagnet, Color.red);
        if (Physics.Raycast(this.gameObject.transform.position, magnetFwd, out magtedHit, rayMagnet))
        {
            if (magtedHit.collider.tag == "Untagged")
            {
                Debug.Log("Unable to move Object!");
            }

            else
            {
                angle = Vector3.Angle(this.gameObject.transform.position, magtedHit.collider.transform.position);
                heightY = this.gameObject.transform.position.y - magtedHit.collider.transform.position.y;
                Quaternion_Rotate_From = magtedHit.collider.transform.rotation;
                startPos = magtedHit.collider.transform.localPosition;

                if (angle > 14)
                {
                    Quaternion_Rotate_To = Quaternion.Euler(0, 0, 5f);
                    magtedHit.collider.transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);

                }

                else if (angle < 20)
                {
                    Quaternion_Rotate_To = Quaternion.Euler(0, 0, -5f);
                    magtedHit.collider.transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
                }

                if (Vector3.Distance(this.gameObject.transform.position, magtedHit.collider.transform.position) < distanceStopPull)
                {
                    move = false;
                    Quaternion_Rotate_To = Quaternion.Euler(0, 0, 0);
                    magtedHit.collider.transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
                    if (heightY > 0 || heightY < 0)
                    {
                        startPos.y = Mathf.Lerp(magtedHit.collider.transform.localPosition.y, magtedHit.collider.transform.localPosition.y + heightY, Time.deltaTime * speedUpDown);
                        magtedHit.collider.transform.localPosition = startPos;
                    }


                }

                else
                {
                    move = true;

                }


                if (move == true)
                {
                    magtedHit.collider.GetComponent<Rigidbody>().velocity = -direction[surfaceRay] * speedPushPull;

                }

                else
                {
                    magtedHit.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }
}

class MagnetPush : Functionable
{
    RaycastHit magtedHit;
    GameObject gameObject;
    Vector3 magnetFwd;
    Vector3[] direction = new Vector3[6];
    private bool move = false;
    private int surfaceRay;
    private float rayMagnet;
    private float distanceStopPush;
    private float speedPushPull;

    public MagnetPush(RaycastHit magtedHit, GameObject gameObject, Vector3 magnetFwd, Vector3 direction0, Vector3 direction1, Vector3 direction2, Vector3 direction3, Vector3 direction4, Vector3 direction5, int surfaceRay, float rayMagnet, float distanceStopPush, float speedPushPull)
    {
        this.magtedHit = magtedHit;
        this.gameObject = gameObject;
        this.magnetFwd = magnetFwd;
        this.direction[0] = direction0;
        this.direction[1] = direction1;
        this.direction[2] = direction2;
        this.direction[3] = direction3;
        this.direction[4] = direction4;
        this.direction[5] = direction5;
        this.surfaceRay = surfaceRay;
        this.rayMagnet = rayMagnet;
        this.distanceStopPush = distanceStopPush;
        this.speedPushPull = speedPushPull;
    }

    public override void RunFunction()
    {
        magnetFwd = this.gameObject.transform.TransformDirection(direction[surfaceRay]);
        Debug.DrawRay(this.gameObject.transform.position, magnetFwd * rayMagnet, Color.red);
        if (Physics.Raycast(this.gameObject.transform.position, magnetFwd, out magtedHit, rayMagnet))
        {
            if (magtedHit.collider.tag == "Untagged")
            {
                Debug.Log("Unable to move Object!");
            }

            else
            {

                if (Vector3.Distance(this.gameObject.transform.position, magtedHit.collider.transform.position) > distanceStopPush)
                {
                    move = false;

                }

                else
                {
                    move = true;

                }


                if (move == true)
                {
                    // Do Reference!!!
                    magtedHit.collider.GetComponent<Rigidbody>().velocity = (direction[surfaceRay] * speedPushPull);

                }

                else
                {
                    magtedHit.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }

}

class Lazer : Functionable
{
    LineRenderer lazer;
    RaycastHit objectHit;
    GameObject gameObject;
    Vector3 fwd;
    Vector3[] direction = new Vector3[6];
    private int surfaceRay;
    private float length;
    private float rayDistance;

    public Lazer(LineRenderer lazer, RaycastHit objectHit, GameObject gameObject, Vector3 fwd, Vector3 direction0, Vector3 direction1, Vector3 direction2, Vector3 direction3, Vector3 direction4, Vector3 direction5, int surfaceRay, float length, float rayDistance)
    {
        this.lazer = lazer;
        this.objectHit = objectHit;
        this.gameObject = gameObject;
        this.fwd = fwd;
        this.direction[0] = direction0;
        this.direction[1] = direction1;
        this.direction[2] = direction2;
        this.direction[3] = direction3;
        this.direction[4] = direction4;
        this.direction[5] = direction5;
        this.surfaceRay = surfaceRay;
        this.length = length;
        this.rayDistance = rayDistance;
    }

    public override void RunFunction()
    {
        Color c1 = new Color(0, 0, 1, 1);
        Color c2 = new Color(0, 0, 1, 1);
        Color d1 = new Color(1, 1, 1, 1);
        Color d2 = new Color(1, 1, 1, 1);

        fwd = this.gameObject.transform.TransformDirection(direction[surfaceRay]);
        Debug.DrawRay(this.gameObject.transform.position, fwd * rayDistance, Color.blue);
        Vector3 endPosition = fwd + (length * direction[surfaceRay]);
        lazer.SetColors(d1, d2);
        lazer.SetPosition(0, fwd);
        lazer.SetPosition(1, endPosition);

        if (Physics.Raycast(this.gameObject.transform.position, fwd, out objectHit, rayDistance))
        {
            if(objectHit.collider.tag == "Untagged")
            {
                length = Vector3.Distance(this.gameObject.transform.position, objectHit.collider.transform.position);
                length = length * 2;
                endPosition = fwd + (length * direction[surfaceRay]);
                lazer.SetColors(d1, d2);
                lazer.SetPosition(0, fwd);
                lazer.SetPosition(1, endPosition);
                lazer.enabled = true;
            }

            else if(objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay != 6)
            {
                length = Vector3.Distance(this.gameObject.transform.position, objectHit.collider.transform.position);
                length = length * 2;
                endPosition = fwd + (length * direction[surfaceRay]);
                lazer.SetColors(d1, d2);
                lazer.SetPosition(0, fwd);
                lazer.SetPosition(1, endPosition);
                lazer.enabled = true;
            }

            else if (objectHit.collider.GetComponent<Pipe>().totalNumOfSurRay == 6)
            {
                length = Vector3.Distance(this.gameObject.transform.position, objectHit.collider.transform.position);
                length = length * 2;
                endPosition = fwd + (length * direction[surfaceRay]);
                lazer.SetColors(c1, c2);
                lazer.SetPosition(0, fwd);
                lazer.SetPosition(1, endPosition);
                lazer.enabled = true;
            }         
        }
    }

}

