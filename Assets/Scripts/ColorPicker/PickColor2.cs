using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickColor2 : MonoBehaviour
{
    public GameObject colorPicker;

    public void SelectColor()
    {
        colorPicker.GetComponent<ColorPicker2>().SetGameobjectColor(gameObject); //Passing this gameObject to "ColorPicker2"
    }

    public void SetButtonMethod()
    {
        GetComponent<Button>().onClick.AddListener(SelectColor); //Assign method "SelectColor" on button component
    }

    public void SetColorPicker(GameObject gameObject) => colorPicker = gameObject;
}
