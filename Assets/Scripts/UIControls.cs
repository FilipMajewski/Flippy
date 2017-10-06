using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{

    public Text scoreText;
    int score = 0;

    void Start()
    {
        scoreText.text = score.ToString();
    }


    void Update()
    {

    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }


}
