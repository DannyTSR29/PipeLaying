using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class MagnetPullFunction : MonoBehaviour
{
    [Serializable]
    public class SurfaceRayDirection
    {
        public bool front;
        public bool back;
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }
    public SurfaceRayDirection surfaceRayD;
    RaycastHit magtedHit;
    Vector3 magnetFwd;
    Vector3 startPos;
    Vector3[] direction = new Vector3[6];
    private Quaternion Quaternion_Rotate_From;
    private Quaternion Quaternion_Rotate_To;
    [SerializeField] private GameObject mainPipe;
    //[SerializeField] private bool move = false;
    [SerializeField] private int surfaceRay;
    //[SerializeField] private float heightY;
    //[SerializeField] private float angle;
    [SerializeField] private float rayMagnet;
    [SerializeField] private float distanceStopPull;
    [SerializeField] private float speedPushPull;
    [SerializeField] private float speedUpDown;
    [SerializeField] private float Rotation_Speed;
    [SerializeField] private float Rotation_Friction;
    [SerializeField] private float Rotation_Smoothness;
    Pipe pipeRef;

    // Start is called before the first frame update
    void Start()
    {
        pipeRef = mainPipe.GetComponent<Pipe>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (pipeRef.arrayClass[0].surfaceRay == true && surfaceRayD.front == true)
        {
            surfaceRay = 0;
            MagnetPull magnetPull = new MagnetPull(magtedHit, this.gameObject, magnetFwd, startPos, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, /*move,*/ surfaceRay, /*heightY, angle,*/ rayMagnet, distanceStopPull, speedPushPull, speedUpDown, Rotation_Speed, Rotation_Friction, Rotation_Smoothness, Quaternion_Rotate_From, Quaternion_Rotate_To);
            magnetPull.RunFunction();
        }

        else if (pipeRef.arrayClass[1].surfaceRay == true && surfaceRayD.back == true)
        {
            surfaceRay = 1;
            MagnetPull magnetPull = new MagnetPull(magtedHit, this.gameObject, magnetFwd, startPos, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, /*move,*/ surfaceRay, /*heightY, angle,*/ rayMagnet, distanceStopPull, speedPushPull, speedUpDown, Rotation_Speed, Rotation_Friction, Rotation_Smoothness, Quaternion_Rotate_From, Quaternion_Rotate_To);
            magnetPull.RunFunction();
        }

        else if (pipeRef.arrayClass[2].surfaceRay == true && surfaceRayD.up == true)
        {
            surfaceRay = 2;
            MagnetPull magnetPull = new MagnetPull(magtedHit, this.gameObject, magnetFwd, startPos, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, /*move,*/ surfaceRay, /*heightY, angle,*/ rayMagnet, distanceStopPull, speedPushPull, speedUpDown, Rotation_Speed, Rotation_Friction, Rotation_Smoothness, Quaternion_Rotate_From, Quaternion_Rotate_To);
            magnetPull.RunFunction();
        }

        else if (pipeRef.arrayClass[3].surfaceRay == true && surfaceRayD.down == true)
        {
            surfaceRay = 3;
            MagnetPull magnetPull = new MagnetPull(magtedHit, this.gameObject, magnetFwd, startPos, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, /*move,*/ surfaceRay, /*heightY, angle,*/ rayMagnet, distanceStopPull, speedPushPull, speedUpDown, Rotation_Speed, Rotation_Friction, Rotation_Smoothness, Quaternion_Rotate_From, Quaternion_Rotate_To);
            magnetPull.RunFunction();
        }

        else if (pipeRef.arrayClass[5].surfaceRay == true && surfaceRayD.right == true)
        {
            surfaceRay = 5;
            MagnetPull magnetPull = new MagnetPull(magtedHit, this.gameObject, magnetFwd, startPos, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, /*move,*/ surfaceRay, /*heightY, angle,*/ rayMagnet, distanceStopPull, speedPushPull, speedUpDown, Rotation_Speed, Rotation_Friction, Rotation_Smoothness, Quaternion_Rotate_From, Quaternion_Rotate_To);
            magnetPull.RunFunction();
        }

        else if (pipeRef.arrayClass[4].surfaceRay == true && surfaceRayD.left == true)
        {
            surfaceRay = 4;
            MagnetPull magnetPull = new MagnetPull(magtedHit, this.gameObject, magnetFwd, startPos, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, /*move,*/ surfaceRay, /*heightY, angle,*/ rayMagnet, distanceStopPull, speedPushPull, speedUpDown, Rotation_Speed, Rotation_Friction, Rotation_Smoothness, Quaternion_Rotate_From, Quaternion_Rotate_To);
            magnetPull.RunFunction();
        }
    }
}
