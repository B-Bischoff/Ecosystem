using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Localization.Settings;

public class WindowGraph : MonoBehaviour
{
    public RectTransform graphContainer;
    public Sprite circleSprite;
    public GameObject DotsManager, LinesManager, GridManager;

    public Color axisColor;
    public Color dotColor_1, lineColor_1;
    public Color dotColor_2, lineColor_2;
    public Color dotColor_3, lineColor_3;
    public Color dotColor_4, lineColor_4;

    public int nbMeanElem;
    public float dotRadius;
    public float lineThickness;
    public float gridThickness;
    public float marges;
    public float VerticalGridSpace; //cycle unit
    public float HorizontalGridSpace;
    public bool EnableMean;

    public List<int> list1 = new List<int>();
    public List<int> list2 = new List<int>();
    public List<int> list3 = new List<int>();
    public List<int> list4 = new List<int>();

    public string[] _listsName = new string[4];

    private int _listCount1, _listCount2, _listCount3, _listCount4;

    private bool _forceUpdate;

    public void Awake()
    {
        for (int i = 0; i < _listsName.Length; i++)
            _listsName[i] = "No data";

        graphContainer = GetComponent<RectTransform>();
    }

    public void Start()
    {
        // Setting default values 
        nbMeanElem = 20;

        if (list1 != null)
            _listCount1 = list1.Count;
        if (list2 != null)
            _listCount2 = list2.Count;
        if (list3 != null)
            _listCount3 = list3.Count;
        if (list4 != null)
            _listCount4 = list4.Count;
    }

    public void Update()
    {
        if (_listCount1 != list1.Count && list1 != null)
            UpdateGraph();
        else if (_listCount2 != list2.Count && list2 != null)
            UpdateGraph();
        else if (_listCount3 != list3.Count && list3 != null)
            UpdateGraph();
        else if (_listCount4 != list4.Count && list4 != null)
            UpdateGraph();
        else if (_forceUpdate){
            _forceUpdate = false;
            UpdateGraph();
        }
    }


    private void UpdateGraph()
    {
        _listCount1 = list1.Count;
        _listCount2 = list2.Count;
        _listCount3 = list3.Count;
        _listCount4 = list4.Count;

        RemoveGraph();

        if(list1 != null)
            ShowGraph(list1, 1);
        if(list2 != null)
            ShowGraph(list2, 2);
        if(list3 != null)
            ShowGraph(list3, 3);
        if(list4 != null)
            ShowGraph(list4, 4);
    }

    private void RemoveGraph()
    {
        foreach (Transform child in LinesManager.transform)
        {
            if (child.name == "dotConnection")
                Destroy(child.gameObject);
        }
        foreach (Transform child in DotsManager.transform)
        {
            if (child.name == "Circle")
                Destroy(child.gameObject);
        }
        foreach(Transform child in GridManager.transform)
        {
            if (child.name == "VerticalGridRect" || child.name == "HorizontalGridRect")
                Destroy(child.gameObject);
        }

    }

    private void ShowGraph(List<int> list, int listNumber)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float xSize = graphContainer.sizeDelta.x / list.Count; //xSize is the distance between each point on the x axis (-> number of cycle in that case)

        float yMax = FindYmax(); // Getting max value in the list to adjust the y coordinate of dots
        int mean = 0; // Used in "Mean display", create a mean of the datas 
        int dotCounter = 0; // Used to display vertical grids

        GameObject lastCircleGameObject = null;
        for(int i = 0; i < list.Count; i++)
        {
            if (list.Count <= nbMeanElem || !EnableMean) // sClassic display
            {
                //Circles
                float xPosition = i * xSize;
                float yPosition = (list[i] / yMax) * (graphHeight - marges);
                GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), listNumber, ((int)i / TimeCycle.nbMeasurePerCycle) + 1, list[i]);

                if (lastCircleGameObject != null)
                {
                    CreateDotConnectin(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition, listNumber);
                }
                lastCircleGameObject = circleGameObject;

                //Grid
                if(i % VerticalGridSpace == 0 && i != 0)
                    CreateVerticalGrid(xPosition);
                }
            else //Mean display
            { 
                if(i % (int)(list.Count / nbMeanElem) == 0) // Display
                {
                    // Adding the new sample
                    mean += list[i];

                    if (i != 0)
                    {
                        mean /= (list.Count / (int)nbMeanElem);
                        dotCounter++;
                    }

                    // Calculating graph position 
                    float xPosition = (((float)i) / list.Count) * graphContainer.sizeDelta.x;
                    float yPosition = (mean / yMax) * (graphHeight - marges);
                    GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), listNumber, ((int)i / TimeCycle.nbMeasurePerCycle) + 1, mean);


                    // Draw line between circles
                    if (lastCircleGameObject != null)
                    {
                        CreateDotConnectin(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                        circleGameObject.GetComponent<RectTransform>().anchoredPosition, listNumber);
                    }
                    lastCircleGameObject = circleGameObject;

                    // Grid
                    if (dotCounter % VerticalGridSpace == 0)
                        CreateVerticalGrid(xPosition);

                    mean = 0;
                }
                else
                {
                    mean += list[i];
                }
            }
        }
        if(yMax >= HorizontalGridSpace)
        {
            for(int i = 1; i <= (yMax / HorizontalGridSpace); i++)
                    CreateHorizontalGrid((HorizontalGridSpace * i) * ((graphHeight - marges)/((yMax/HorizontalGridSpace) * HorizontalGridSpace)));
        }
    }
    #region createShapes
    private GameObject CreateCircle(Vector2 anchoredPosition, int listNumber, float xValue, float yValue)
    {
        Color dotColor = new Color();
        switch (listNumber)
        {
            case 1: dotColor = dotColor_1; break;
            case 2: dotColor = dotColor_2; break;
            case 3: dotColor = dotColor_3; break;
            case 4: dotColor = dotColor_4; break;
        }

        GameObject gameObject = new GameObject("Circle", typeof(Image));
        gameObject.transform.SetParent(DotsManager.transform, false);
        gameObject.GetComponent<Image>().color = dotColor;
        gameObject.GetComponent<Image>().sprite = circleSprite;

        //Tooltip informations :

        gameObject.AddComponent<TooltipTrigger>();

        if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            gameObject.GetComponent<TooltipTrigger>().header = _listsName[listNumber - 1]; // Header
            gameObject.GetComponent<TooltipTrigger>().content = "Cycle : " + xValue + "\nValue : " + yValue; // Content
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            gameObject.GetComponent<TooltipTrigger>().header = TranslateListName(_listsName[listNumber - 1]); // Header
            gameObject.GetComponent<TooltipTrigger>().content = "Cycle : " + xValue + "\nValeur : " + yValue; // Content
        }

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(dotRadius, dotRadius);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return gameObject;
    }

    private string TranslateListName(string listName)
    {
        switch (listName) {
            case "No data": return "Aucune";
            case "Bunny Number": return "Nombre lapin";
            case "Bunny Dead": return "Lapin mort";
            case "Bunny Dehydrated": return "Lapin déshydraté";
            case "Bunny Starved": return "Lapin affamé";
            case "Bunny Eaten": return "Lapin chassé";
            case "Carrot Number": return "Nombre carotte"; 
            case "Fox Number": return "Nombre renard";
            case "Fox Dead": return "Renard mort";
            case "Fox Dehydrated": return "Renard déshydraté";
            case "Fox Starved": return  "Renard affamé";
            default: return null;
        }
    }

    private void CreateDotConnectin(Vector2 dotPosA, Vector2 dotPosB, int listNumber)
    {
        Color lineColor = new Color();
        switch (listNumber)
        {
            case 1: lineColor = lineColor_1; break;
            case 2: lineColor = lineColor_2; break;
            case 3: lineColor = lineColor_3; break;
            case 4: lineColor = lineColor_4; break;
        }

        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(LinesManager.transform, false);
        gameObject.GetComponent<Image>().color = lineColor;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPosB - dotPosA).normalized;
        float distance = Vector2.Distance(dotPosA, dotPosB);

        rectTransform.sizeDelta = new Vector2(distance, lineThickness);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchoredPosition = dotPosA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    private void CreateVerticalGrid(float xPos)
    {
        GameObject gameObject = new GameObject("VerticalGridRect", typeof(Image));
        gameObject.transform.SetParent(GridManager.transform, false);
        gameObject.GetComponent<Image>().color = axisColor;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(gridThickness, graphContainer.sizeDelta.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchoredPosition = new Vector2(xPos, rectTransform.sizeDelta.y / 2);
    }

    private void CreateHorizontalGrid(float yPos)
    {
        GameObject gameObject = new GameObject("HorizontalGridRect", typeof(Image));
        gameObject.transform.SetParent(GridManager.transform, false);
        gameObject.GetComponent<Image>().color = axisColor;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(graphContainer.sizeDelta.x, gridThickness);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x / 2, yPos);
    }

    #endregion
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private float FindYmax()
    {
        float yMax = float.MinValue;

        if(list1 != null)
            for (int i = 0; i < list1.Count; i++)
            {
                if (yMax < list1[i])
                    yMax = list1[i];
            }
        if (list2 != null)
            for (int i = 0; i < list2.Count; i++)
            {
                if (yMax < list2[i])
                    yMax = list2[i];
            }
        if (list3 != null)
            for (int i = 0; i < list3.Count; i++)
            {
                if (yMax < list3[i])
                    yMax = list3[i];
            }
        if (list4 != null)
            for (int i = 0; i < list4.Count; i++)
            {
                if (yMax < list4[i])
                    yMax = list4[i];
            }

        return yMax;
    }

    //Getters & Setters

    public void setForceUpdate(bool value) => _forceUpdate = value;

    public void setList1(List<int> list) => list1 = list;
    public void setList2(List<int> list) => list2 = list;
    public void setList3(List<int> list) => list3 = list;
    public void setList4(List<int> list) => list4 = list;
    public void setAxisColor(Color color) => axisColor = color;
    public void setDotColor1(Color color) => dotColor_1 = color;
    public void setLineColor1(Color color) => lineColor_1 = color;
    public void setDotColor2(Color color) => dotColor_2 = color;
    public void setLineColor2(Color color) => lineColor_2 = color;
    public void setDotColor3(Color color) => dotColor_3 = color;
    public void setLineColor3(Color color) => lineColor_3 = color;
    public void setDotColor4(Color color) => dotColor_4 = color;
    public void setLineColor4(Color color) => lineColor_4 = color;
    public void setDotRadius(float value) => dotRadius = value;
    public void setLineThickness(float value) => lineThickness = value;
    public void setGridThickness(float value) => gridThickness = value;
    public void setMarges(float value) => marges = value;
    public void setVerticaleGridSpace(int value) => VerticalGridSpace = value;
    public void setHorizontalGridSpace(int value) => HorizontalGridSpace = value;
    public void SetEnableMean(bool value) => EnableMean = value;
    public void SetNbMeanElement(int value) => nbMeanElem = value;
    public void SetListName(int index, string text) => _listsName[index] = text;

    public int getHorizontalGridSpace() { return (int)HorizontalGridSpace; }
    public int getVerticalGridSpace() { return (int)VerticalGridSpace; }
    public bool GetEnableMean() { return EnableMean; }
    public int GetNbMeanElement() { return nbMeanElem; }
    public int GetLineThickness() { return (int)lineThickness; }
    public int GetDotRadius() { return (int)dotRadius; }
    public string GetListName(int i) { return _listsName[i]; }
    public Color GetDotColor(int i)
    {
        Color color = Color.white;
        switch(i)
        {
            case 0: color = dotColor_1; break;
            case 1: color = dotColor_2; break;
            case 2: color = dotColor_3; break;
            case 3: color = dotColor_4; break;
        }
        return color;
    }
    public Color GetLineColor(int i)
    {
        Color color = Color.white;
        switch (i)
        {
            case 0: color = lineColor_1; break;
            case 1: color = lineColor_2; break;
            case 2: color = lineColor_3; break;
            case 3: color = lineColor_4; break;
        }
        return color;
    }
}