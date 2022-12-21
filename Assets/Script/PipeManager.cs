using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
//using UnityEngine.SceneManagement;

public class PipeManager : MonoBehaviour
{
    public UnityEvent trigger;
    private bool oneTime = false;
    [SerializeField]
    GameObject[] pipes;

    // Start is called before the first frame update

    private void Awake()
    {
        if (trigger == null)
        {
            trigger = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager() == true)
        {
            //Debug.Log("Win!");
            for (int i = 0; i < pipes.Length; i++)
            {
                pipes[i].GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            }
            SceneManager.LoadScene("GameWin");

            //if(trigger != null && !oneTime)
            //{
            //    trigger.Invoke();
            //    oneTime = true;
            //}
        }
        else
        {
            oneTime = false;
        }
        //if (trigger != null && !oneTime)
        //{
        //    trigger.Invoke();
        //    oneTime = true;
        //}
    }

    public bool Manager()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            if (pipes[i].GetComponent<Pipe>().PipeConnectCheck() == false)
            {
                return false;
            }
        }
        return true;
    }

    void Start()
    {
        pipes = GameObject.FindGameObjectsWithTag("cube");
        //if (trigger == null)
        //{
        //    trigger = new UnityEvent();
        //} 
        //trigger.AddListener()
    }

}