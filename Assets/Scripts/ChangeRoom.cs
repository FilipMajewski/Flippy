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

    AudioSource source;

    public AudioClip[] clips;

    UIControls ui;

    public Transform startPoint;

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

        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIControls>();

        source = GetComponent<AudioSource>();

        source.PlayOneShot(clips[imageNumber]);

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

        ui.audioSource.PlayOneShot(ui.buttonSound);
    }

    public void SelectRoom()
    {
        Destroy(GameObject.FindGameObjectWithTag("Rooms"));
        Instantiate(roomPrefab[imageNumber], gameObject.transform);
        source.Stop();
        source.PlayOneShot(clips[imageNumber]);
        ui.audioSource.PlayOneShot(ui.roomChanged);
        startPoint.position = new Vector3(3.7f, 0.3f, 0f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = startPoint.position;
    }

    string showDescriptiontext()
    {
        string description = "";

        if (imageNumber == 0)
        {
            description = "Place where magic happens.";

        }

        if (imageNumber == 1)
        {
            description = "Run chicken, run.";

        }

        if (imageNumber == 2)
        {
            description = "Developer secret room.";

        }

        if (imageNumber == 3)
        {
            description = "Something smells funny here.";

        }

        return description;
    }
}
