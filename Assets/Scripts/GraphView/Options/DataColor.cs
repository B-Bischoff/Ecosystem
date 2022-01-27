using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataColor : MonoBehaviour
{
    public GameObject colorPicker, graphManager;
    public RawImage[] colorDatas = new RawImage[4];
    public bool colorChoose;

    private bool _isColorPickerShown;
    public int dataColor;

    public void Start()
    {
        colorPicker.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isColorPickerShown)
            colorPicker.SetActive(false);

        if(colorChoose)
        {
            colorChoose = false;
            colorPicker.SetActive(false);
            _isColorPickerShown = false; ;
        }
    }

    public void SelectColor(int dataNumber)
    {
        if(_isColorPickerShown)
        {
            colorPicker.SetActive(false);
            _isColorPickerShown = false;
        }
        else
        {
            dataColor = dataNumber;
            colorPicker.SetActive(true);
            _isColorPickerShown = true;
        }
    }
}
