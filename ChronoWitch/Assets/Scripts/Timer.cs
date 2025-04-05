using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] private PlayerAgeSwitcher ageSwitcher;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip timeAlmostUpClip;

    public string ageState;

    public void Start()
    {
        remainingTime = 60;
        ageState = "Present";
    }

    public void Update()
    {
        ChangeTime(remainingTime);
        ChangeAgeState(ageState);
        //Debug.Log(ageState);

        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            
            if(remainingTime < 15)
                audioSource.PlayOneShot(timeAlmostUpClip);
        }
        else if(remainingTime <= 0)
        {
            //remainingTime = 0;
            if (ageState == "Present")
            {
                ChangeTime(30);
                ChangeAgeState("Future");
                ageSwitcher.SwitchAge(2, remainingTime);
            }
            else if (ageState == "Past")
            {
                ChangeTime(60);
                ChangeAgeState("Present");
                ageSwitcher.SwitchAge(0, remainingTime);
            }
            else if (ageState == "Future")
            {
                ChangeTime(0);
                ChangeAgeState("Dead");
            }
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ChangeAge(string state)
    {
        if(state == "Present")
        {
            ChangeTime(30);
            ChangeAgeState("Future");
            ageSwitcher.SwitchAge(2, remainingTime);
        }
        else if(state == "Past")
        {
            ChangeTime(60);
            ChangeAgeState("Present");
            ageSwitcher.SwitchAge(0, remainingTime);
        }
        else if(state == "Future")
        {
            ChangeTime(0);
            ChangeAgeState("Dead");
        }

    }

    public void ChangeTime(float timeTillDeath)
    {
        remainingTime = timeTillDeath;
    }

    public void ChangeAgeState(string state)
    {
        ageState = state;
    }


    public void AddTime(int time)
    {
        remainingTime += time;
        //ChangeTime(remainingTime);
    }
}
