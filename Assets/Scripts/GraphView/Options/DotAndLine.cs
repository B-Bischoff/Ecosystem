using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotAndLine : MonoBehaviour
{
    public GameObject[] windowGraphArray = new GameObject[4];

    public void SetDotRadius(string value)
    {
        int input_int;
        int.TryParse(value, out input_int);

        if (input_int <= 0)
            input_int = 1;

        int graphNumber = OptionsStrips.optionGraphSelected_S;
        windowGraphArray[graphNumber - 1].GetComponent<WindowGraph>().setDotRadius(input_int);

        GetComponent<RefreshContent>().RefreshContentFunction();
    }

    public void SetLineThickness(string value)
    {
        int input_int;
        int.TryParse(value, out input_int);

        if (input_int <= 0)
            input_int = 1;

        int graphNumber = OptionsStrips.optionGraphSelected_S;
        windowGraphArray[graphNumber - 1].GetComponent<WindowGraph>().setLineThickness(input_int);

        GetComponent<RefreshContent>().RefreshContentFunction();
    }
}
