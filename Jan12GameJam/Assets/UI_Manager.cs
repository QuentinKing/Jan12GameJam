using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI liveValues;
    public TextMeshProUGUI pointsValues;
    public TextMeshProUGUI timerValue;
    public Image staminaBarFill;

    public TextMeshProUGUI youLoseText;
    public TextMeshProUGUI youWinText;

    private void Start()
    {
        OnGameStart();
    }

    private void Update()
    {
        // TODO, hook these up
        SetLives(3);
        SetPoints(100);
        SetStaminaBar(0.7f);
        SetTime(142.0f);
    }

    public void SetLives(int livesTotal)
    {
        liveValues.text = livesTotal.ToString();
    }

    public void SetPoints(int points)
    {
        pointsValues.text = points.ToString();
    }

    public void SetStaminaBar(float percentage)
    {
        staminaBarFill.fillAmount = percentage;
    }

    public void SetTime(float secondsRemaining)
    {
        if (secondsRemaining < 0)
        {
            secondsRemaining = 0;
        }
        string minutes = Mathf.Floor(secondsRemaining / 60.0f).ToString("00");
        string seconds = (secondsRemaining % 60.0f).ToString("00");
        timerValue.text = string.Format("{0}:{1}", minutes, seconds);
    }

    public void OnGameOver()
    {
        youLoseText.gameObject.SetActive(true);
    }

    public void OnGameStart()
    {
        youWinText.gameObject.SetActive(false);
        youLoseText.gameObject.SetActive(false);
    }
}
