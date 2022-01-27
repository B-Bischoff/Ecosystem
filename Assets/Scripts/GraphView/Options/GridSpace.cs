using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridSpace : MonoBehaviour
{
    public GameObject graphManager;
    public TMP_InputField horizontalInput, verticalInput;

    public void ChangeVerticalGrid(string value)
    {
        int input_int;
        int.TryParse(value, out input_int);
        if(input_int <= 0)
        {
            verticalInput.text = "1";
            input_int = 1;
        }

        graphManager.GetComponent<GraphManager>().changeVerticalAndHorizontal(false, OptionsStrips.optionGraphSelected_S, input_int);
    }

    public void ChangeHorizontalGrid(string value)
    {
        int input_int;
        int.TryParse(value, out input_int);

        if (input_int <= 0)
        {
            horizontalInput.text = "1";
            input_int = 1;
        }

        graphManager.GetComponent<GraphManager>().changeVerticalAndHorizontal(true, OptionsStrips.optionGraphSelected_S, input_int);
    }
}
