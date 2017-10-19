using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    Transform player;
    public Vector3 offset;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position - offset;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
