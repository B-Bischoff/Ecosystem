using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Title : MonoBehaviour
{
    public TextMeshProUGUI[] titleTab = new TextMeshProUGUI[4];

    public void SetGraphTitle(string text) 
    {
        titleTab[OptionsStrips.optionGraphSelected_S - 1].text = text;
    }
}
