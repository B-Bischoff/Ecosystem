using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.Localization.Settings;

public class ExpandAnimalCarac: MonoBehaviour
{
    public GameObject expand;
    public RawImage arrow;

    private bool _rightShown;

    public void Awake()
    {
        InitLanguage();

        _rightShown = false;
        transform.DOMoveX(1920+300, 0);
        arrow.transform.DORotate(new Vector3(0, 0, -90), 0);
    }

    private void InitLanguage()
    {
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
            expand.GetComponent<TooltipTrigger>().content = "Caractéristiques animal";
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
            expand.GetComponent<TooltipTrigger>().content = "Animal characteristics";
    }

    public void ToggleLeft()
    {
        if(_rightShown)
        {
            _rightShown = false;
            transform.DOMoveX(1920+300, .5f);
            arrow.transform.DORotate(new Vector3(0, 0, -90), .3f);
        }
        else
        {
            _rightShown = true;
            transform.DOMoveX(1920, .5f);
            arrow.transform.DORotate(new Vector3(0, 0, 90), .3f);
        }
    }
}
