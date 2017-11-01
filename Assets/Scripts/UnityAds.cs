using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour
{
    UIControls uiControls;
    // Use this for initialization
    void Start()
    {
        uiControls = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIControls>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions
        {
            resultCallback = HandleShowResult
        };

        Advertisement.Show("rewardedVideo", options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}
