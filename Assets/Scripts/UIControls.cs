using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIControls : MonoBehaviour
{
    public RectTransform buttons;
    public RectTransform objectPanel;
    public RectTransform roomPanel;
    public RectTransform optionsPanel;
    public RectTransform startPanel;

    public Text scoreText;
    public Text coinsText;

    public Transform touchImage;

    public Transform startPoint;
    int score = 0;

    void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        scoreText.text = score.ToString();
        touchImage.DOScale(0.8f, 1).SetLoops(-1, LoopType.Yoyo);
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
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
    }

    public void Options()
    {
        buttons.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
        optionsPanel.DOScale(Vector3.one, 1).SetEase(Ease.InBack);
        //TODO show options screne
    }

    public void Restart()
    {
        //TODO return flipy object to orginal position and rotation

    }

    public void ChangeObject()
    {
        objectPanel.DOLocalMoveY(0, 1).SetEase(Ease.OutBack);
    }

    public void ChangeRoom()
    {
        roomPanel.DOLocalMoveY(0, 1).SetEase(Ease.OutBack);
    }

    public void ChangeObjectBack()
    {
        objectPanel.DOLocalMoveY(-800, 1).SetEase(Ease.InBack);
    }

    public void ChangeRoomBack()
    {
        roomPanel.DOLocalMoveY(-800, 1).SetEase(Ease.InBack);
    }

    public void OptionsBack()
    {
        buttons.DOScale(Vector3.one, 1).SetEase(Ease.OutBack);
        optionsPanel.DOScale(Vector3.zero, 1).SetEase(Ease.OutBack);
    }

    public void TouchToStart()
    {
        startPanel.DOScale(Vector3.zero, 1);
        buttons.DOScale(Vector3.one, 1).SetEase(Ease.OutBack);
    }


}
