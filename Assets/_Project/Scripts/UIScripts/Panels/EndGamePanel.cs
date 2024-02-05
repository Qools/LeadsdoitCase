using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePanel : UIPanel
{
    public GameObject nextButton;
    public GameObject retryButton;
    public TextMeshProUGUI endGameText;


    public void Win()
    {
        nextButton.SetActive(true);
        retryButton.SetActive(false);

        endGameText.text = "Level " + (DataManager.Instance.GetLevel() - 1).ToString() + " Completed";
    }

    public void GameOver()
    {
        nextButton.SetActive(false);
        retryButton.SetActive(true);

        endGameText.text = "Level Failed";
    }

    public void OnNextButtonPressed()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());
    }

    public void OnRetryButtonPressed()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());
    }

}
