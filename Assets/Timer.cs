using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    DateTime startTime;
    Text totalTime;
    Text roundTime;
    Text phaseLabel;
    Image background;

    DateTime deploy2;

    DateTime round11;
    DateTime round12;
    DateTime round21;
    DateTime round22;
    DateTime round31;
    DateTime round32;

    TimeSpan remainingTime;
    TimeSpan turnTime;

    int player;
    int round;
    string phaseText;

    int sumMinutes;

    // Start is called before the first frame update
    void Start()
    {
        roundTime = GameObject.Find("turn_time").GetComponent<Text>();
        totalTime = GameObject.Find("total_time").GetComponent<Text>();
        phaseLabel = GameObject.Find("phase").GetComponent<Text>();
        background = GameObject.Find("background").GetComponent<Image>();
        startTime = DateTime.Now;

        sumMinutes = (2 * TimerSettings.DeploymentMinutes) + (2 * TimerSettings.Round1Minutes) + (2 * TimerSettings.Round2Minutes) + (2 * TimerSettings.Round3Minutes);

        deploy2 = startTime.AddMinutes(TimerSettings.DeploymentMinutes);
        round11 = deploy2.AddMinutes(TimerSettings.DeploymentMinutes);
        round12 = round11.AddMinutes(TimerSettings.Round1Minutes);
        round21 = round12.AddMinutes(TimerSettings.Round1Minutes);
        round22 = round21.AddMinutes(TimerSettings.Round2Minutes);
        round31 = round22.AddMinutes(TimerSettings.Round2Minutes);
        round32 = round31.AddMinutes(TimerSettings.Round3Minutes);

        player = 1;
        phaseText = "deployment";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Setup");
        }

        SetRemainingTime();
        SetTurnTime();
        totalTime.text = remainingTime.ToString("hh\\:mm\\:ss");
        roundTime.text = turnTime.ToString("mm\\:ss");
        phaseLabel.text = phaseText;
        background.color = player == 1 ? Color.blue : Color.red;
    }

    void SetRemainingTime()
    {
        remainingTime = TimeSpan.FromMinutes(sumMinutes).Subtract(DateTime.Now.Subtract(startTime));
        if (remainingTime < TimeSpan.Zero)
        {
            remainingTime = TimeSpan.Zero;
        }
    }

    void SetTurnTime()
    {
        phaseText = "";
        if (DateTime.Now > round32)
        {
            turnTime = remainingTime;
            player = 2;
            round = 3;
        }
        else if (DateTime.Now > round31)
        {
            turnTime = round32.Subtract(DateTime.Now);
            player = 1;
            round = 3;
        }
        else if (DateTime.Now > round22)
        {
            turnTime = round31.Subtract(DateTime.Now);
            player = 2;
            round = 2;
        }
        else if (DateTime.Now > round21)
        {
            turnTime = round22.Subtract(DateTime.Now);
            player = 1;
            round = 2;
        }
        else if (DateTime.Now > round12)
        {
            turnTime = round21.Subtract(DateTime.Now);
            player = 2;
            round = 1;
        }
        else if (DateTime.Now > round11)
        {
            turnTime = round12.Subtract(DateTime.Now);
            player = 1;
            round = 1;
        }
        else if (DateTime.Now > deploy2)
        {
            turnTime = round11.Subtract(DateTime.Now);
            player = 2;
            phaseText = "Deployment - Player 2";
        }
        else
        {
            turnTime = deploy2.Subtract(DateTime.Now);
            player = 1;
            phaseText = "Deployment - Player 1";
        }

        if (string.IsNullOrEmpty(phaseText))
        {
            phaseText = $"Round {round} - Player {player}";
        }

        if (turnTime < TimeSpan.Zero)
        {
            turnTime = TimeSpan.Zero;
        }
    }
}
