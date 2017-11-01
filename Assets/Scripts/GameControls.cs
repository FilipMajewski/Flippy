using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControls : MonoBehaviour
{
    public Font currentFont;
    private void Awake()
    {
        Text[] texts = FindObjectsOfType(typeof(Text)) as Text[];
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].font = currentFont;
        }
    }
}
