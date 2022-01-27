using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public GameObject rawImageTarget;
    public GameObject graphOptions;
    public Color color, darkerColor;
    public GameObject[] graphList = new GameObject[4];

    public void setRawImageTarget(GameObject rawImage) => rawImageTarget = rawImage;
    public void setColor()
    {
        color = rawImageTarget.GetComponent<RawImage>().color;
        CalculateDarkerColor();
        SendColor();
        graphOptions.GetComponent<DataColor>().colorChoose = true;
    }

    private void CalculateDarkerColor()
    {
        darkerColor.a = 1;
        darkerColor.r = color.r - .2f;
        darkerColor.g = color.g - .2f;
        darkerColor.b = color.b - .2f;
    }

    private void SendColor()
    {
        int _dataColor = graphOptions.GetComponent<DataColor>().dataColor - 1;
        graphOptions.GetComponent<DataColor>().colorDatas[_dataColor].GetComponent<RawImage>().color = color; //Update option preview

        //Update graph data
        switch (_dataColor)
        {
            case 0:
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().dotColor_1 = color;
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().lineColor_1 = darkerColor;
            break;
            case 1:
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().dotColor_2 = color;
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().lineColor_2 = darkerColor;
                break;
            case 2:
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().dotColor_3 = color;
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().lineColor_3 = darkerColor;
                break;
            case 3:
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().dotColor_4 = color;
                graphList[OptionsStrips.optionGraphSelected_S - 1].GetComponent<WindowGraph>().lineColor_4 = darkerColor;
                break;
        }
    }
}
