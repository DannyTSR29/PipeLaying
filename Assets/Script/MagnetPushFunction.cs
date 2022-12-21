using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class MagnetPushFunction : MonoBehaviour
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
    Vector3[] direction = new Vector3[6];
    [SerializeField] private GameObject mainPipe;
    [SerializeField] private int surfaceRay;
    [SerializeField] private float rayMagnet;
    [SerializeField] private float distanceStopPush;
    [SerializeField] private float speedPushPull;
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
            MagnetPush magnetPush = new MagnetPush(magtedHit, this.gameObject, magnetFwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, rayMagnet, distanceStopPush, speedPushPull);
            magnetPush.RunFunction();
        }

        else if (pipeRef.arrayClass[1].surfaceRay == true && surfaceRayD.back == true)
        {
            surfaceRay = 1;
            MagnetPush magnetPush = new MagnetPush(magtedHit, this.gameObject, magnetFwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, rayMagnet, distanceStopPush, speedPushPull);
            magnetPush.RunFunction();
        }

        else if (pipeRef.arrayClass[2].surfaceRay == true && surfaceRayD.up == true)
        {
            surfaceRay = 2;
            MagnetPush magnetPush = new MagnetPush(magtedHit, this.gameObject, magnetFwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, rayMagnet, distanceStopPush, speedPushPull);
            magnetPush.RunFunction();
        }

        else if (pipeRef.arrayClass[3].surfaceRay == true && surfaceRayD.down == true)
        {
            surfaceRay = 3;
            MagnetPush magnetPush = new MagnetPush(magtedHit, this.gameObject, magnetFwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, rayMagnet, distanceStopPush, speedPushPull);
            magnetPush.RunFunction();
        }

        else if (pipeRef.arrayClass[4].surfaceRay == true && surfaceRayD.left == true)
        {
            surfaceRay = 4;
            MagnetPush magnetPush = new MagnetPush(magtedHit, this.gameObject, magnetFwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, rayMagnet, distanceStopPush, speedPushPull);
            magnetPush.RunFunction();
        }

        else if (pipeRef.arrayClass[5].surfaceRay == true && surfaceRayD.right == true)
        {
            surfaceRay = 5;
            MagnetPush magnetPush = new MagnetPush(magtedHit, this.gameObject, magnetFwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, rayMagnet, distanceStopPush, speedPushPull);
            magnetPush.RunFunction();
        }
        
    }
}
