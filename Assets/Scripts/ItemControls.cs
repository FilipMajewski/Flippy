using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemControls : MonoBehaviour
{

    Rigidbody rb;

    Vector2 slideStart;
    Vector2 slideStop;
    Vector2 move;

    public Transform raycastPosition;
    Transform startPoint;

    public FlippyObject flippyObject;

    float forcePower;
    float torquePower;

    float rayDistance = 0.2f;
    Quaternion startRotation;
    bool addPoints = false;
    public LayerMask grounded;
    public bool justSpawned;
    public bool stillFliping;

    UIControls ui;
    UnityAds ads;

    AudioSource source;
    AudioClip flipAudioClip;

    int restartCount = 0;
    int flipsCount;
    int rateShow;
    // Use this for initialization
    void Start()
    {
        rateShow = PlayerPrefs.GetInt("Rate");
        flipsCount = PlayerPrefs.GetInt("Flips");
        startPoint = GameObject.FindGameObjectWithTag("StartPointNew").transform;

        rb = GetComponent<Rigidbody>();

        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIControls>();
        ads = GameObject.FindGameObjectWithTag("GameController").GetComponent<UnityAds>();

        forcePower = flippyObject.forcePower;
        torquePower = flippyObject.torquePower;

        rb.mass = flippyObject.mass;

        startRotation = transform.rotation;

        source = GetComponent<AudioSource>();
        flipAudioClip = flippyObject.flipSound;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            slideStart = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            slideStop = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            move = slideStop - slideStart;

            if (!stillFliping)
            {
                Flip();
            }


        }

        if (rb.velocity.magnitude == 0)
        {
            stillFliping = false;

            if (addPoints && Physics.Raycast(raycastPosition.position, -raycastPosition.up, rayDistance, grounded))
            {
                AddPoints();

                if (flipsCount >= 30 && rateShow == 0)
                {
                    PlayerPrefs.SetInt("Rate", 1);
                    ui.ShowRate();
                    rateShow = 1;
                }
            }

            if (!Physics.Raycast(raycastPosition.position, -raycastPosition.up, rayDistance, grounded))
            {
                Restart();
                ui.ResetScore();

            }


        }
        else
        {
            stillFliping = true;
        }
    }

    void Flip()
    {
        source.PlayOneShot(flipAudioClip);
        justSpawned = false;
        rb.AddForce(move * forcePower, ForceMode.Impulse);

        if (move.x > 0)
            rb.AddTorque(0, 0, -torquePower, ForceMode.Impulse);
        else
            rb.AddTorque(0, 0, torquePower, ForceMode.Impulse);

        StartCoroutine("WaitAndPoints", 0.5f);

        flipsCount++;
        PlayerPrefs.SetInt("Flips", flipsCount);
    }

    void AddPoints()
    {
        addPoints = false;
        ui.AddScore();
        startPoint.position = transform.position;
    }

    void Restart()
    {
        addPoints = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPoint.position;
        transform.rotation = startRotation;
        restartCount++;
        if (restartCount > 8)
        {
            ads.ShowNormalVideo();
            restartCount = 0;
        }
    }

    private IEnumerator WaitAndPoints(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        addPoints = true;
    }

}
