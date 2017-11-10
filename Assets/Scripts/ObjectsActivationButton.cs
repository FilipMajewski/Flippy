using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsActivationButton : MonoBehaviour
{
    public Image icon;
    public Image lockIcon;
    public FlippyObject flippyObject;
    public Button unlock;
    public Text price;
    public Text nameText;

    public bool objectIsUnlock;

    Transform startPosition;

    UIControls uiControls;

    // Use this for initialization
    void Start()
    {
        uiControls = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIControls>();

        icon.sprite = flippyObject.aSprite;
        startPosition = GameObject.FindGameObjectWithTag("StartPoint").transform;
        if (PlayerPrefs.GetInt(flippyObject.objectName) == 1)
        {
            objectIsUnlock = true;

        }

        if (objectIsUnlock)
            lockIcon.gameObject.SetActive(false);
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
            unlock.gameObject.SetActive(false);
            if (player.GetComponent<ItemControls>().flippyObject.objectPrefab.name == flippyObject.objectPrefab.name)
            {
                Debug.Log("Same Name");
                unlock.onClick.RemoveAllListeners();
                return;
            }
            else
            {
                Destroy(player);
                Instantiate(flippyObject.objectPrefab, startPosition.position, flippyObject.objectPrefab.transform.rotation);
                //player.GetComponent<ItemControls>().justSpawned = true;
                uiControls.audioSource.PlayOneShot(uiControls.newItemInsta);
                unlock.onClick.RemoveAllListeners();
            }


        }

        else
        {
            //show unlockbutton
            unlock.gameObject.SetActive(true);
            price.text = "Unlock for: " + flippyObject.price;
            //select.gameObject.SetActive(false);
            unlock.onClick.RemoveAllListeners();
            unlock.onClick.AddListener(OnUnlockClick);

        }

        nameText.text = flippyObject.name;
    }

    public void OnUnlockClick()
    {
        if (flippyObject.price <= PlayerPrefs.GetInt("Coins"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - flippyObject.price);
            objectIsUnlock = true;
            PlayerPrefs.SetInt(flippyObject.objectName, 1);
            unlock.gameObject.SetActive(false);
            //select.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);
            uiControls.coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
            OnButtonClick();

            uiControls.audioSource.PlayOneShot(uiControls.itemUnlock);
            unlock.onClick.RemoveAllListeners();
        }
        if (flippyObject.price > PlayerPrefs.GetInt("Coins") && !objectIsUnlock)
        {
            // show notification that you need more coins
            uiControls.ShowAdsPanel();
            uiControls.audioSource.PlayOneShot(uiControls.playAds);
            Debug.Log("Need more coins");
        }
    }
}
