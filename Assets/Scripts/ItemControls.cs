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

    // Use this for initialization
    void Start()
    {

        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        rb = GetComponent<Rigidbody>();
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIControls>();

        forcePower = flippyObject.forcePower;
        torquePower = flippyObject.torquePower;

        rb.mass = flippyObject.mass;
        //justSpawned = true;

        startRotation = transform.rotation;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

        Debug.Log(addPoints);

        if (rb.velocity.magnitude == 0)
        {
            stillFliping = false;

            if (addPoints && Physics.Raycast(raycastPosition.position, -raycastPosition.up, rayDistance, grounded))
            {
                AddPoints();

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
        justSpawned = false;
        rb.AddForce(move * forcePower, ForceMode.Impulse);

        if (move.x > 0)
            rb.AddTorque(0, 0, -torquePower, ForceMode.Impulse);
        else
            rb.AddTorque(0, 0, torquePower, ForceMode.Impulse);

        StartCoroutine("WaitAndPoints", 0.5f);

    }

    void AddPoints()
    {
        addPoints = false;
        ui.AddScore();
        startPoint.position = transform.position;
        //justSpawned = true;
        //StopCoroutine("WaitAndPoints");
    }

    void Restart()
    {
        addPoints = false;
        Debug.Log("Restart");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPoint.position;
        transform.rotation = startRotation;
        //justSpawned = true;
    }

    private IEnumerator WaitAndPoints(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        addPoints = true;
    }

}
