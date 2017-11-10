using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;

public class UIControls : MonoBehaviour
{
    public RectTransform buttons;
    public RectTransform objectPanel;
    public RectTransform roomPanel;
    public RectTransform optionsPanel;
    public RectTransform startPanel;
    public RectTransform adsPanel;
    public RectTransform creditsPanel;
    public RectTransform ratePanel;

    public Text scoreText;
    public Text coinsText;

    public Transform touchImage;

    public Transform startPoint;

    int score = 0;
    int itemsPanelNumber = 0;

    public GameObject[] itemsPanels;

    public AudioClip buttonSound;
    public AudioClip addScoreSound;
    public AudioClip resetScore;
    public AudioClip newItemInsta;
    public AudioClip itemUnlock;
    public AudioClip playAds;
    public AudioClip roomChanged;

    public AudioSource audioSource;
    public AudioMixer audioMixer;

    public Button unlock;

    void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        scoreText.text = score.ToString();
        touchImage.DOScale(0.8f, 1).SetLoops(-1, LoopType.Yoyo);
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            audioMixer.SetFloat("volumeSound", -80);
        }
        else
        {
            audioMixer.SetFloat("volumeSound", 0);
        }

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            audioMixer.SetFloat("volumeMusic", -80);
        }
        else
        {
            audioMixer.SetFloat("volumeMusic", 0);
        }
    }

    void Update()
    {

    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + score);
        score = 0;
        scoreText.text = score.ToString();
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        audioSource.PlayOneShot(resetScore);
    }

    public void AddScore()
    {
        if (score == 0)
        {
            score += 1;
        }

        else
        {
            score *= 2;
        }

        scoreText.text = score.ToString();
        audioSource.PlayOneShot(addScoreSound);
    }

    public void Options()
    {
        buttons.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
        optionsPanel.DOScale(Vector3.one, 1).SetEase(Ease.InBack);
        //TODO show options screne
        audioSource.PlayOneShot(buttonSound);

    }

    public void ChangeObject()
    {
        objectPanel.DOLocalMoveY(0, 1).SetEase(Ease.OutBack);
        buttons.DOScale(Vector3.zero, 1);

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        audioSource.PlayOneShot(buttonSound);

    }

    public void ChangeRoom()
    {
        roomPanel.DOLocalMoveY(0, 1).SetEase(Ease.OutBack);
        buttons.DOScale(Vector3.zero, 1);

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        audioSource.PlayOneShot(buttonSound);
    }

    public void ChangeObjectBack()
    {
        objectPanel.DOLocalMoveY(-800, 1).SetEase(Ease.InBack);
        buttons.DOScale(Vector3.one, 1);
        HideAdsPanel();

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        audioSource.PlayOneShot(buttonSound);
    }

    public void ChangeRoomBack()
    {
        roomPanel.DOLocalMoveY(-800, 1).SetEase(Ease.InBack);
        buttons.DOScale(Vector3.one, 1);

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        audioSource.PlayOneShot(buttonSound);
    }

    public void OptionsBack()
    {
        buttons.DOScale(Vector3.one, 1).SetEase(Ease.OutBack);
        optionsPanel.DOScale(Vector3.zero, 1).SetEase(Ease.OutBack);

        audioSource.PlayOneShot(buttonSound);
    }

    public void TouchToStart()
    {
        startPanel.DOScale(Vector3.zero, 1);
        buttons.DOScale(Vector3.one, 1).SetEase(Ease.OutBack);
    }

    public void ShowAdsPanel()
    {
        adsPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCubic);

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        audioSource.PlayOneShot(buttonSound);
    }

    public void HideAdsPanel()
    {
        adsPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutCubic);

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void ChangeItemsPanel(int direction)
    {
        itemsPanelNumber += direction;

        if (itemsPanelNumber < 0)
        {
            itemsPanelNumber = itemsPanels.Length - 1;
        }


        if (itemsPanelNumber > itemsPanels.Length - 1)
        {
            itemsPanelNumber = 0;
        }

        for (int i = 0; i < itemsPanels.Length; i++)
        {
            itemsPanels[i].SetActive(false);
            itemsPanels[itemsPanelNumber].SetActive(true);
        }

        audioSource.PlayOneShot(buttonSound);
    }

    public void OnRateButtonClick()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=YOUR_APP_ID");

#elif UNITY_IPHONE
     Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_APP_ID");
#endif
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
        CloseRate();
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void MuteSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
            audioMixer.SetFloat("volumeSound", -80);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            audioMixer.SetFloat("volumeSound", 0);
        }


    }

    public void MuteMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            PlayerPrefs.SetInt("Music", 1);
            audioMixer.SetFloat("volumeMusic", -80);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            audioMixer.SetFloat("volumeMusic", 0);
        }


    }

    public void CloseCredits()
    {
        creditsPanel.DOScale(Vector3.zero, 1).SetEase(Ease.OutBack);
        audioSource.PlayOneShot(buttonSound);
    }

    public void OpenCredits()
    {
        creditsPanel.DOScale(Vector3.one, 1).SetEase(Ease.InBack);
        audioSource.PlayOneShot(buttonSound);
    }

    public void ShowRate()
    {
        ratePanel.DOScale(Vector3.one, 1).SetEase(Ease.InBack);
    }

    public void CloseRate()
    {
        ratePanel.DOScale(Vector3.zero, 1).SetEase(Ease.OutBack);
    }

}
