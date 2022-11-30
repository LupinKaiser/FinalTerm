using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMPro timerText;
    public float currentTime = 0;
    public float startTime = 10;

     void Start()
    {
        currentTime = startTime;
    }

    public void Update()
    {
        currentTime -= 1*Time.deltaTime;
        countdownText.text = currentTime.ToString("Time Left:"+currentTime);
        if(currentTime <= 0)
        {
            SceneManager.LoadScene("Fail");
        }
    }
}
