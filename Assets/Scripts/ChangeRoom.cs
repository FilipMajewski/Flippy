using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRoom : MonoBehaviour
{

    public Sprite[] roomIcon;
    public GameObject[] roomPrefab;

    public Image roomImage;
    public Text roomName;
    public Text description;

    int imageNumber;

    private void Awake()
    {
        imageNumber = PlayerPrefs.GetInt("CurrentRoom");
        Instantiate(roomPrefab[imageNumber], gameObject.transform);
    }

    // Use this for initialization
    void Start()
    {
        roomImage.sprite = roomIcon[imageNumber];
        roomName.text = roomPrefab[imageNumber].name;
        description.text = showDescriptiontext();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeImage(int next)
    {
        imageNumber += next;

        if (imageNumber < 0)
            imageNumber = roomIcon.Length - 1;

        if (imageNumber > roomIcon.Length - 1)
            imageNumber = 0;

        roomImage.sprite = roomIcon[imageNumber];
        PlayerPrefs.SetInt("CurrentRoom", imageNumber);
        roomName.text = roomPrefab[imageNumber].name;
        description.text = showDescriptiontext();
    }

    public void SelectRoom()
    {
        Destroy(GameObject.FindGameObjectWithTag("Rooms"));
        Instantiate(roomPrefab[imageNumber], gameObject.transform);
    }

    string showDescriptiontext()
    {
        string description = "";

        if (imageNumber == 0)
        {
            description = "Place where magic happens.\nNo bonuses here.";
        }

        if (imageNumber == 1)
        {
            description = "Run chicken, run.\nEvery succesfull flip +1 coin.";
        }

        if (imageNumber == 2)
        {
            description = "Developer secret room.\nGravity is lower";
        }

        if (imageNumber == 3)
        {
            description = "Something smells funny here.\nEvery succesfull flip coins multiply *3";
        }

        return description;
    }
}
