using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefreshContent : MonoBehaviour
{
    public TMP_InputField title, nbDataMean, yGridSpace, xGridSpace, dotsRadius, lineThickness;
    public Toggle enableMean;
    public RawImage colorData1, colorData2, colorData3, colorData4;
    public TextMeshProUGUI  nbDataMeanTxt;

    public Color darkGray, lightGray;

    public GameObject[] windowGraphList = new GameObject[4];

    public TextMeshProUGUI[] titleTab = new TextMeshProUGUI[4];

    public RawImage[] colorsImg = new RawImage[4];

    public void RefreshContentFunction()
    {
        int graphNumber = OptionsStrips.optionGraphSelected_S;

        //Refresh title
        title.text = titleTab[graphNumber - 1].text;

        //Refresh mean

        //Enable Mean
        enableMean.isOn = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().GetEnableMean();
        if(!windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().GetEnableMean()) //if enable mean is off -> AutoMean & nbMeanElem not interactable
        {
            nbDataMean.interactable = false;
            nbDataMeanTxt.color = darkGray;
        }
        else
        {
            nbDataMean.interactable = true;
            nbDataMeanTxt.color = lightGray;
        }


        //nb mean element
        nbDataMean.text = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().GetNbMeanElement().ToString();

        //Refresh X & Y Grid space
        yGridSpace.text = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().getVerticalGridSpace().ToString();
        xGridSpace.text = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().getHorizontalGridSpace().ToString();

        //Refresh line & dot
        lineThickness.text = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().GetLineThickness().ToString();
        dotsRadius.text = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().GetDotRadius().ToString();

        //Colors
        colorsImg[0].color = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().dotColor_1;
        colorsImg[1].color = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().dotColor_2;
        colorsImg[2].color = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().dotColor_3;
        colorsImg[3].color = windowGraphList[graphNumber - 1].GetComponent<WindowGraph>().dotColor_4;
    }
}
