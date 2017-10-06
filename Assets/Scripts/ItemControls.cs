using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControls : MonoBehaviour
{

    Rigidbody rb;

    Vector2 slideStart;
    Vector2 slideStop;

    public Transform raycastPosition;

    public float forcePower;
    public float torquePower;

    public float rayDistance;

    bool addPoints;
    bool grounded;
    public bool stillFliping;

    public UIControls ui;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            slideStart = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            rb.isKinematic = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            slideStop = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (!stillFliping)
            {
                Flip();
            }

        }

        if (rb.velocity.magnitude <= 0.1)
        {
            stillFliping = false;

            if (addPoints && Physics.Raycast(raycastPosition.position, -raycastPosition.up, rayDistance))
            {
                AddPoints();
                rb.isKinematic = true;
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

        Vector2 move = slideStop - slideStart;
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
    }

    void Restart()
    {
        Debug.Log("Restart");
    }

    private IEnumerator WaitAndPoints(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        addPoints = true;
    }

}
