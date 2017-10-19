using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObject : MonoBehaviour
{

    public FlippyObject[] flippyObjects;

    Image[] icons;

    // Use this for initialization
    void Start()
    {
        icons = GetComponentsInChildren<Image>();

        for (int i = 0; i < flippyObjects.Length; i++)
        {
            icons[i].sprite = flippyObjects[i].aSprite;
        }

        Debug.Log("icons " + icons.Length);
        Debug.Log("flippy " + flippyObjects.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
