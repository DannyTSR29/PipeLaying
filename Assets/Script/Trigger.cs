using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    //[SerializeField] private GameObject canvas;
    private float fadeOutTime = 1f;
    //[SerializeField] private TextMeshPro text;
    [SerializeField] private Text text;
    //[SerializeField] private Canvas text;
    private bool oneTime = false;
    [SerializeField] private PipeManager manager;
    //private List<GameObject> objects;
    //private List<Pipe> connection;
    //private int itemCount;
    //private ObjectPooler OP;
    //private GameObject GO;

    void Start()
    {
    //    text.enabled = true;
    //    OP = ObjectPooler.SharedInstance;
    //    GO = OP.GetPooledObject(0);
    //    //objects = OP.GetAllPooledObjects(0);
    //    //size = objects.Count;
    //    int itemCount = OP.itemsToPool.Count;
    //    //int indexOfThisObj = OP.AddObject(GO, size, true);
    //    for (int i = 0; i < itemCount; i++)
    //    {
    //        objects = OP.GetAllPooledObjects(i);
    //        int size = objects.Count;

        //        for (int count = 0; count < size; count++)
        //        {

        //            //connection[count] = objects[count].GetComponent<Pipe>();
        //        }
        //    }
    }


    void Update()
    {
        //canvas.SetActive(true);
        //FadeOut();

    }


    public void FadeOut()
    {
        //if (!oneTime)
        //{
        //    StartCoroutine(FadeOutRoutine());
        //}
    }

    private IEnumerator FadeOutRoutine()
    {
        //Text text = GetComponent<Text>();
        text.enabled = true;
        //oneTime = true;
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
        text.enabled = false ;
    }
}
