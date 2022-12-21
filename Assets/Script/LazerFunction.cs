using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class LazerFunction : MonoBehaviour
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

    LineRenderer lazer;
    RaycastHit objectHit;
    Vector3 fwd;
    Vector3[] direction = new Vector3[6];
    [SerializeField] GameObject mainPipe;
    [SerializeField] private int surfaceRay = 0;
    [SerializeField] private float length;
    [SerializeField] private float rayDistance;
    Pipe pipeRef;

    // Start is called before the first frame update
    void Start()
    {
        pipeRef = mainPipe.GetComponent<Pipe>();
        lazer = this.gameObject.GetComponent<LineRenderer>();
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
            Lazer lazerRef = new Lazer(lazer, objectHit, this.gameObject, fwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, length, rayDistance);
            lazerRef.RunFunction();
        }

        if (pipeRef.arrayClass[1].surfaceRay == true && surfaceRayD.back == true)
        {
            surfaceRay = 1;
            Lazer lazerRef = new Lazer(lazer, objectHit, this.gameObject, fwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, length, rayDistance);
            lazerRef.RunFunction();
        }

        if (pipeRef.arrayClass[2].surfaceRay == true && surfaceRayD.up == true)
        {
            surfaceRay = 2;
            Lazer lazerRef = new Lazer(lazer, objectHit, this.gameObject, fwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, length, rayDistance);
            lazerRef.RunFunction();
        }

        if (pipeRef.arrayClass[3].surfaceRay == true && surfaceRayD.down == true)
        {
            surfaceRay = 3;
            Lazer lazerRef = new Lazer(lazer, objectHit, this.gameObject, fwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, length, rayDistance);
            lazerRef.RunFunction();
        }

        if (pipeRef.arrayClass[4].surfaceRay == true && surfaceRayD.left == true)
        {
            surfaceRay = 4;
            Lazer lazerRef = new Lazer(lazer, objectHit, this.gameObject, fwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, length, rayDistance);
            lazerRef.RunFunction();
        }

        if (pipeRef.arrayClass[5].surfaceRay == true && surfaceRayD.right == true)
        {
            surfaceRay = 5;
            Lazer lazerRef = new Lazer(lazer, objectHit, this.gameObject, fwd, Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left, surfaceRay, length, rayDistance);
            lazerRef.RunFunction();
        }

    }

}
