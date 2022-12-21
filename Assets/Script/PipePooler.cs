using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PipePooler : MonoBehaviour
{
    //[SerializeField] private GameObject destroyPoint;
    private ObjectPooler OP;
    private GameObject GO;
    private List<GameObject> objects;
    private int size;
    private int itemCount;
    private Vector3 offset = new Vector3(5f, 0f, 10f);
    //private Vector3 zPosition = new Vector3(0f, 0f, 3f);
    private Vector3 yPosition = new Vector3(0f, 1f, 0f);
    private Vector3 []detect;
    //private Vector3 sizeDetect = new Vector3(2f, 2f, 2f);
    private Vector3 [] spawnerPos;
    private float timer = 0;

    private void OnEnable()
    {
        SetObject();
        SetSpawner();
        Spawner1();
        Spawner2();
        Spawner3();
    }

    private void Update()
    {
        // SetSpawner();
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            Spawner1();
            Spawner2();
            Spawner3();
        }
    }

    private void SetObject()
    {
        OP = ObjectPooler.SharedInstance;
        itemCount = OP.itemsToPool.Count;
    }
    private void SetSpawner()
    {
        //detect = new Vector3[5];
        detect = new Vector3[9];
        detect[0] = new Vector3(0f, 0f, 0f);
        detect[1] = new Vector3(0.5f, 0f, 0.5f);
        detect[2] = new Vector3(-0.5f, 0f, 0.5f);
        detect[3] = new Vector3(0.5f, 0f, -0.5f);
        detect[4] = new Vector3(-0.5f, 0f, -0.5f);
        detect[5] = new Vector3(-0.5f, 0f, 0f);
        detect[6] = new Vector3(0f, 0f, -0.5f);
        detect[7] = new Vector3(0.5f, 0f, 0f);
        detect[8] = new Vector3(0f, 0f, 0.5f);
        spawnerPos = new Vector3[3];
        //for (int i=0; i< 3; i++)
        //{
        //spawnerPos[i] = transform.position + (offset * i);
        spawnerPos[0] = transform.position;
        spawnerPos[1] = transform.position + offset;
        spawnerPos[2] = transform.position - offset;
        //}
    } 
    private void Spawner1()
    {
        List<GameObject> objects;
        objects = OP.GetAllPooledObjects(0);
        int size = objects.Count;
        bool block = false;
        for (int i = 0; i < 9; i++)
        {            
            block = Physics.Raycast(spawnerPos[0] - yPosition + detect[i], transform.up);
            if(block == true)
            {
                break;
            }
        }       
        //Debug.Log(block);
        if (!block)
        {
              for (int i = 0; i < size; i++)
              {
                 if (objects[i].activeSelf == false)
                 {
                    objects[i].SetActive(true);
                    objects[i].GetComponent<BoxCollider>().isTrigger = false;
                    objects[i].GetComponent<Rigidbody>().isKinematic = false;
                    objects[i].transform.position = spawnerPos[0];
                    objects[i].transform.parent = null;
                    break;
                 }
              }
            
        }

    }
    private void Spawner2()
    {
        List<GameObject> objects;
        objects = OP.GetAllPooledObjects(1);
        int size = objects.Count;
        bool block = false;
        for (int i = 0; i < 9; i++)
        {
            block = Physics.Raycast(spawnerPos[1] - yPosition + detect[i], transform.up);
            if (block == true)
            {
                break;
            }
        }
        //Debug.Log(block);
        if (!block)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i].activeSelf == false)
                {
                    objects[i].SetActive(true);
                    objects[i].GetComponent<BoxCollider>().isTrigger = false;
                    objects[i].GetComponent<Rigidbody>().isKinematic = false;
                    objects[i].transform.position = spawnerPos[1];
                    objects[i].transform.parent = null;
                    break;
                }
            }

        }
    }
    private void Spawner3()
    {
        List<GameObject> objects;
        objects = OP.GetAllPooledObjects(2);
        int size = objects.Count;
        bool block = false;
        for (int i = 0; i < 9; i++)
        {
            block = Physics.Raycast(spawnerPos[2] - yPosition + detect[i], transform.up);
            if (block == true)
            {
                break;
            }
        }
        //Debug.Log(block);
        if (!block)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i].activeSelf == false)
                {
                    objects[i].SetActive(true);
                    objects[i].GetComponent<BoxCollider>().isTrigger = false;
                    objects[i].GetComponent<Rigidbody>().isKinematic = false;
                    objects[i].transform.position = spawnerPos[2];
                    objects[i].transform.parent = null;
                    break;
                }
            }

        }
    }

}
