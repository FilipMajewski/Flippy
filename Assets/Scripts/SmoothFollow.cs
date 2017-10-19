using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        transform.position =
            new Vector3(Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime).x,
            transform.position.y, transform.position.z);
    }
}
