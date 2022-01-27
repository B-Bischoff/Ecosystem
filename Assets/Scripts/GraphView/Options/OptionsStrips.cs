using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsStrips : MonoBehaviour
{
    public GameObject[] strips = new GameObject[4];
    private GameObject _previousStrip;

    public Color _lightGray, _darkGray, _txtLightGray, _txtDarkGray;

    public static int optionGraphSelected_S;

    private void Awake()
    {
        optionGraphSelected_S = 1;

        strips[0].GetComponent<RawImage>().color = _lightGray;
        strips[1].GetComponent<RawImage>().color = _darkGray;
        strips[2].GetComponent<RawImage>().color = _darkGray;
        strips[3].GetComponent<RawImage>().color = _darkGray;

        strips[0].GetComponentInChildren<TextMeshProUGUI>().color = _txtLightGray;
        strips[1].GetComponentInChildren<TextMeshProUGUI>().color = _txtDarkGray;
        strips[2].GetComponentInChildren<TextMeshProUGUI>().color = _txtDarkGray;
        strips[3].GetComponentInChildren<TextMeshProUGUI>().color = _txtDarkGray;

        _previousStrip = strips[0];
    }

    public void SelectStrip(int value)
    {
        if (value != optionGraphSelected_S)
        {
            strips[value - 1].GetComponent<RawImage>().color = _lightGray;
            strips[value - 1].GetComponentInChildren<TextMeshProUGUI>().color = _txtLightGray;

            _previousStrip.GetComponent<RawImage>().color = _darkGray;
            _previousStrip.GetComponentInChildren<TextMeshProUGUI>().color = _txtDarkGray;

            _previousStrip = strips[value - 1];
            optionGraphSelected_S = value;

            RefreshContent();
        }
    }

    private void RefreshContent()
    {

    }
}
