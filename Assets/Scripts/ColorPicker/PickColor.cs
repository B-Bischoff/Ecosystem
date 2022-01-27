using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickColor : MonoBehaviour
{
    public GameObject colorPicker;
    public void SelectColor()
    {
        colorPicker.GetComponent<ColorPicker>().setRawImageTarget(gameObject);
        colorPicker.GetComponent<ColorPicker>().setColor();
    }
}
