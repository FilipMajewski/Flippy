using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour
{
    UIControls uiControls;

    public AudioClip adCompleteSound;

    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        uiControls = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIControls>();
        audioSource = GetComponent<AudioSource>();
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

    public void ShowNormalVideo()
    {
        Advertisement.Show();
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
            audioSource.PlayOneShot(adCompleteSound);

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
