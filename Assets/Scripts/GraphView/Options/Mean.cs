using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mean : MonoBehaviour
{
    public GameObject[] windowGraphArray = new GameObject[4];

    public void ToggleMean(bool value)
    {
        int graphNumber = OptionsStrips.optionGraphSelected_S;
        windowGraphArray[graphNumber - 1].GetComponent<WindowGraph>().SetEnableMean(value);
        GetComponent<RefreshContent>().RefreshContentFunction();
    }

    public void SetNbDataMean(string value)
    {
        int input_int;
        int.TryParse(value, out input_int);

        if (input_int <= 4)
            input_int = 5;

        int graphNumber = OptionsStrips.optionGraphSelected_S;
        windowGraphArray[graphNumber - 1].GetComponent<WindowGraph>().SetNbMeanElement(input_int);

        GetComponent<RefreshContent>().RefreshContentFunction();
    }
}
