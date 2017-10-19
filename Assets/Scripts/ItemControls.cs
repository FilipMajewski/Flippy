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

    Transform raycastPosition;
    Transform startPoint;

    public FlippyObject flippyObject;

    float forcePower;
    float torquePower;

    float rayDistance = 0.1f;

    bool addPoints = false;
    bool grounded;
    public bool stillFliping;

    UIControls ui;

    // Use this for initialization
    void Start()
    {
        raycastPosition = GameObject.Find("raycastPoint").transform;
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        rb = GetComponent<Rigidbody>();
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIControls>();

        forcePower = flippyObject.forcePower;
        torquePower = flippyObject.torquePower;

        rb.mass = flippyObject.mass;
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

            Debug.Log(move.magnitude);
        }

        if (rb.velocity.magnitude == 0)
        {
            stillFliping = false;

            if (addPoints && Physics.Raycast(raycastPosition.position, -raycastPosition.up, rayDistance))
            {
                AddPoints();
            }

            if (!Physics.Raycast(raycastPosition.position, -raycastPosition.up, rayDistance))
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
    }

    void Restart()
    {
        addPoints = false;
        Debug.Log("Restart");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPoint.position;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator WaitAndPoints(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        addPoints = true;

    }

}
