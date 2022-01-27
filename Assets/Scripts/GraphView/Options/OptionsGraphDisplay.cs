using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsGraphDisplay : MonoBehaviour
{
    public GameObject Background, DarkBackground;

    private bool _optionMenuVisible;

    public void Awake()
    {
        Background.SetActive(false);
        DarkBackground.SetActive(false);
    }

    public void setOptionMenuVisibility(bool value)
    {
        Background.SetActive(value);
        DarkBackground.SetActive(value);
        _optionMenuVisible = value;
    }
}
