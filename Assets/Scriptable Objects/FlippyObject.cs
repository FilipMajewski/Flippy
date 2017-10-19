using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flippy Object")]
public class FlippyObject : ScriptableObject
{

    public string objectName = "Default";
    public int price = 1;

    public float forcePower = 8;
    public float torquePower = 4;

    public float mass = 1;

    public Sprite aSprite;
    public AudioClip aSound;

    public GameObject objectPrefab;

}
