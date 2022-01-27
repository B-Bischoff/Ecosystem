using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Localization.Settings;

public class TimeSection : MonoBehaviour
{
    public static int timeScale_s;

    public GameObject timeCycle;
    public TextMeshProUGUI time, cycle;
    public RawImage[] timeSpeedImages = new RawImage[4];
    public Color selectedColor, notSelectedColor;

    private float _animationDelay = .25f;

    private string _header, _pause, _normal, _fast, _veryFast;

    public void Awake()
    {
        InitLanguage();
        AnimateTimeSpeedImage(1); //Animate Time speed icons at the begging of the simulation
    }

    public void Start()
    {
        InvokeRepeating("DisplayCycle", 0f, 60); // Update cycle number (text) every 60 seconds
    }

    public void Update()
    {
        DisplayTime();
    }

    private void DisplayTime()
    {
        // Getting time lapsed stored in TimeCycle 
        double realTime = timeCycle.GetComponent<TimeCycle>().GetRealTimeCounter();

        // Converting realTime from seconds to HH:MM:SS
        int hours = (int)realTime / 3600;
        realTime = realTime - 3600 * hours;

        int minutes = (int)realTime / 60;
        realTime = realTime - 60 * minutes;

        int seconds = (int)realTime;

        // Updating text
        time.text = hours + "h:" + minutes + "m:" + seconds + "s";
    }

    private void DisplayCycle() // Method called every 60 seconds in void Start
    {
        cycle.text = "Cycle : " + timeCycle.GetComponent<TimeCycle>().GetCycleCounter();
    }

    public void SetTimeScale(int timeScale) 
    {
        if (timeScale == 0)
            StartCoroutine(FreezeTimeScale());
        else
        {
            timeScale_s = timeScale;
            Time.timeScale = timeScale_s;
        }
    }

    private IEnumerator FreezeTimeScale()
    {
        yield return new WaitForSeconds(_animationDelay); // Delay allowing icon animations execution
        Time.timeScale = 0;
        timeScale_s = 0;
    }

    public void AnimateTimeSpeedImage(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i != index)
            {
                timeSpeedImages[i].rectTransform.DOScale(1, _animationDelay);
                timeSpeedImages[i].color = notSelectedColor;
            }
            else
            {
                timeSpeedImages[i].rectTransform.DOScale(1.2f, _animationDelay);
                timeSpeedImages[i].color = selectedColor;
            }
        }
    }

    private void InitLanguage()
    {
        // Changing tooltip translation of time speed images
        

        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            _header = "Vitesse simulation";

            _pause = "Pause";
            _normal = "Normal";
            _fast = "Rapide (x2)";
            _veryFast = "Très rapide (x4)";
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            _header = "Time speed";

            _pause = "Pause";
            _normal = "Normal";
            _fast = "Fast (x2)";
            _veryFast = "Vert fast (x4)";
        }

        // Pause image
        timeSpeedImages[0].GetComponent<TooltipTrigger>().header = _header;
        timeSpeedImages[0].GetComponent<TooltipTrigger>().content = _pause;

        // Normal image
        timeSpeedImages[1].GetComponent<TooltipTrigger>().header = _header;
        timeSpeedImages[1].GetComponent<TooltipTrigger>().content = _normal;

        // Fast image
        timeSpeedImages[2].GetComponent<TooltipTrigger>().header = _header;
        timeSpeedImages[2].GetComponent<TooltipTrigger>().content = _fast;

        // Very fast image
        timeSpeedImages[3].GetComponent<TooltipTrigger>().header = _header;
        timeSpeedImages[3].GetComponent<TooltipTrigger>().content = _veryFast;
    }
}
