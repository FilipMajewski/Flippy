using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsActivationButton : MonoBehaviour
{
    public Image icon;
    public FlippyObject flippyObject;
    public Button unlock;
    public Button select;
    public bool objectIsUnlock;

    Transform startPosition;

    // Use this for initialization
    void Start()
    {
        icon.sprite = flippyObject.aSprite;
        startPosition = GameObject.FindGameObjectWithTag("StartPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonClick()
    {
        if (objectIsUnlock)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player);
            Instantiate(flippyObject.objectPrefab, startPosition);
        }

        else
        {
            //show unlockbutton
            unlock.gameObject.SetActive(true);
            select.gameObject.SetActive(false);
            unlock.onClick.AddListener(OnUnlockClick);
        }
    }

    public void OnUnlockClick()
    {
        if (flippyObject.price <= PlayerPrefs.GetInt("Coins"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - flippyObject.price);
            objectIsUnlock = true;

            unlock.gameObject.SetActive(false);
            select.gameObject.SetActive(true);
        }
        else
        {
            // show notification that you need more coins
        }
    }
}
