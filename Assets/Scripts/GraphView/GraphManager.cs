using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public Transform GraphContainer1, GraphContainer2, GraphContainer3, GraphContainer4;

    [Header("Graph1")]
    public List<int> List1_1 = new List<int>();
    public List<int> List1_2 = new List<int>();
    public List<int> List1_3 = new List<int>();
    public List<int> List1_4 = new List<int>();

    public Color Axis_Color1;
    public Color DotColor1_1, LineColor1_1;
    public Color DotColor1_2, LineColor1_2;
    public Color DotColor1_3, LineColor1_3;
    public Color DotColor1_4, LineColor1_4;

    public float DotRadius1, LineThickness1, GridThickness1, Marges1;
    public int VerticalGridSpace1, HorizontalGridSpace1;

    [Header("Graph2")]
    public List<int> List2_1 = new List<int>();
    public List<int> List2_2 = new List<int>();
    public List<int> List2_3 = new List<int>();
    public List<int> List2_4 = new List<int>();

    public Color Axis_Color2;
    public Color DotColor2_1, LineColor2_1;
    public Color DotColor2_2, LineColor2_2;
    public Color DotColor2_3, LineColor2_3;
    public Color DotColor2_4, LineColor2_4;
    public float DotRadius2, LineThickness2, GridThickness2, Marges2;
    public int VerticalGridSpace2, HorizontalGridSpace2;

    [Header("Graph3")]
    public List<int> List3_1 = new List<int>();
    public List<int> List3_2 = new List<int>();
    public List<int> List3_3 = new List<int>();
    public List<int> List3_4 = new List<int>();

    public Color Axis_Color3;
    public Color DotColor3_1, LineColor3_1;
    public Color DotColor3_2, LineColor3_2;
    public Color DotColor3_3, LineColor3_3;
    public Color DotColor3_4, LineColor3_4;
    public float DotRadius3, LineThickness3, GridThickness3, Marges3;
    public int VerticalGridSpace3, HorizontalGridSpace3;

    [Header("Graph4")]
    public List<int> List4_1 = new List<int>();
    public List<int> List4_2 = new List<int>();
    public List<int> List4_3 = new List<int>();
    public List<int> List4_4 = new List<int>();

    public Color Axis_Color4;
    public Color DotColor4_1, LineColor4_1;
    public Color DotColor4_2, LineColor4_2;
    public Color DotColor4_3, LineColor4_3;
    public Color DotColor4_4, LineColor4_4;
    public float DotRadius4, LineThickness4, GridThickness4, Marges4;
    public int VerticalGridSpace4, HorizontalGridSpace4;


    void Start()
    {
        List1_1.Clear();
        List1_2.Clear();
        List1_3.Clear();
        List1_4.Clear();

        List2_1.Clear();
        List2_2.Clear();
        List2_3.Clear();
        List2_4.Clear();

        List3_1.Clear();
        List3_2.Clear();
        List3_3.Clear();
        List3_4.Clear();

        List4_1.Clear();
        List4_2.Clear();
        List4_3.Clear();
        List4_4.Clear();


        UpdateGraphParameters(1);
        UpdateGraphParameters(2);
        UpdateGraphParameters(3);
        UpdateGraphParameters(4);
    }

    void Update()
    {
        
    }

    public int FindListIndex(string listName)
    {
        int index = 0;
        switch(listName)
        {
            case "No data": index = 0; break;
            case "Bunny Number": index = 1; break;
            case "Bunny Dead": index = 2; break;
            case "Bunny Dehydrated": index = 3; break;
            case "Bunny Starved": index = 4; break;
            case "Bunny Eaten": index = 5; break;
            case "Carrot Number": index = 6; break;
            case "Fox Number": index = 7; break;
            case "Fox Dead": index = 8; break;
            case "Fox Dehydrated": index = 9; break;
            case "Fox Starved": index = 10; break;
        }
        return index;
    }

    public List<int> FindList(int index)
    {
        List<int> list = new List<int>();
        switch(index)
        {
            case 0: list = new List<int>();
                break;
            case 1: list = ListsManager.bunnyNumber;
                break;
            case 2: list = ListsManager.bunnyDead;
                break;
            case 3: list = ListsManager.bunnyDehydrated;
                break;
            case 4: list = ListsManager.bunnyStarved;
                break;
            case 5: list = ListsManager.bunnyEaten;
                break;
            case 6: list = ListsManager.carrotNumber;
                break;
            case 7: list = ListsManager.foxNumber;
                break;
            case 8: list = ListsManager.foxDead;
                break;
            case 9: list = ListsManager.foxDehydrated;
                break;
            case 10: list = ListsManager.foxStarved;
                break;

        }
        return list;
    }

    public void ChangeList(int listIndex, int graphNumber, int dataNumber)
    {
        List<int> list = FindList(listIndex);

        switch (graphNumber)
        {
            case 1: //graph1
                switch (dataNumber)
                {
                    case 1:GraphContainer1.GetComponent<WindowGraph>().setList1(list);GraphContainer1.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer1.GetComponent<WindowGraph>().SetListName(0, ListsManager.listsName[listIndex]); break;
                    case 2:GraphContainer1.GetComponent<WindowGraph>().setList2(list);GraphContainer1.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer1.GetComponent<WindowGraph>().SetListName(1, ListsManager.listsName[listIndex]); break;
                    case 3:GraphContainer1.GetComponent<WindowGraph>().setList3(list);GraphContainer1.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer1.GetComponent<WindowGraph>().SetListName(2, ListsManager.listsName[listIndex]); break;
                    case 4:GraphContainer1.GetComponent<WindowGraph>().setList4(list);GraphContainer1.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer1.GetComponent<WindowGraph>().SetListName(3, ListsManager.listsName[listIndex]); break;
                }
                break;

            case 2: //graph2
                switch (dataNumber)
                {
                    case 1:GraphContainer2.GetComponent<WindowGraph>().setList1(list);GraphContainer2.GetComponent<WindowGraph>().setForceUpdate(true); 
                        GraphContainer2.GetComponent<WindowGraph>().SetListName(0, ListsManager.listsName[listIndex]);break;
                    case 2:GraphContainer2.GetComponent<WindowGraph>().setList2(list);GraphContainer2.GetComponent<WindowGraph>().setForceUpdate(true); 
                        GraphContainer2.GetComponent<WindowGraph>().SetListName(1, ListsManager.listsName[listIndex]);break;
                    case 3:GraphContainer2.GetComponent<WindowGraph>().setList3(list);GraphContainer2.GetComponent<WindowGraph>().setForceUpdate(true); 
                        GraphContainer2.GetComponent<WindowGraph>().SetListName(2, ListsManager.listsName[listIndex]);break;
                    case 4:GraphContainer2.GetComponent<WindowGraph>().setList4(list);GraphContainer2.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer2.GetComponent<WindowGraph>().SetListName(3, ListsManager.listsName[listIndex]);break;
                }
                break;

            case 3: //graph3
                switch (dataNumber)
                {
                    case 1:GraphContainer3.GetComponent<WindowGraph>().setList1(list);GraphContainer3.GetComponent<WindowGraph>().setForceUpdate(true); 
                        GraphContainer3.GetComponent<WindowGraph>().SetListName(0, ListsManager.listsName[listIndex]); break;
                    case 2:GraphContainer3.GetComponent<WindowGraph>().setList2(list);GraphContainer3.GetComponent<WindowGraph>().setForceUpdate(true); 
                        GraphContainer3.GetComponent<WindowGraph>().SetListName(1, ListsManager.listsName[listIndex]); break;
                    case 3:GraphContainer3.GetComponent<WindowGraph>().setList3(list);GraphContainer3.GetComponent<WindowGraph>().setForceUpdate(true); 
                        GraphContainer3.GetComponent<WindowGraph>().SetListName(2, ListsManager.listsName[listIndex]); break;
                    case 4:GraphContainer3.GetComponent<WindowGraph>().setList4(list);GraphContainer3.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer3.GetComponent<WindowGraph>().SetListName(3, ListsManager.listsName[listIndex]);  break;
                }
                break;

            case 4: //grap4
                switch (dataNumber)
                {
                    case 1:GraphContainer4.GetComponent<WindowGraph>().setList1(list);GraphContainer4.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer4.GetComponent<WindowGraph>().SetListName(0, ListsManager.listsName[listIndex]);break;
                    case 2:GraphContainer4.GetComponent<WindowGraph>().setList2(list);GraphContainer4.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer4.GetComponent<WindowGraph>().SetListName(1, ListsManager.listsName[listIndex]);break;
                    case 3:GraphContainer4.GetComponent<WindowGraph>().setList3(list);GraphContainer4.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer4.GetComponent<WindowGraph>().SetListName(2, ListsManager.listsName[listIndex]);break;
                    case 4:GraphContainer4.GetComponent<WindowGraph>().setList4(list);GraphContainer4.GetComponent<WindowGraph>().setForceUpdate(true);
                        GraphContainer4.GetComponent<WindowGraph>().SetListName(3, ListsManager.listsName[listIndex]); break;
                }
                break;
        }
    }

    public void changeVerticalAndHorizontal(bool horizontalOrVertical, int graphNumber, int data)
    {
        //False = vertical | True = horizontal

        switch(graphNumber)
        {
            case 1:if (horizontalOrVertical) GraphContainer1.GetComponent<WindowGraph>().setHorizontalGridSpace(data);
                else GraphContainer1.GetComponent<WindowGraph>().setVerticaleGridSpace(data);
                break;

            case 2:
                if (horizontalOrVertical) GraphContainer2.GetComponent<WindowGraph>().setHorizontalGridSpace(data);
                else GraphContainer2.GetComponent<WindowGraph>().setVerticaleGridSpace(data);
                break;

            case 3:
                if (horizontalOrVertical) GraphContainer3.GetComponent<WindowGraph>().setHorizontalGridSpace(data);
                else GraphContainer3.GetComponent<WindowGraph>().setVerticaleGridSpace(data);
                break;

            case 4:
                if (horizontalOrVertical) GraphContainer4.GetComponent<WindowGraph>().setHorizontalGridSpace(data);
                else GraphContainer4.GetComponent<WindowGraph>().setVerticaleGridSpace(data);
                break;
        }
    }

    public void UpdateGraphParameters(int listNumber)
    {
        switch (listNumber)
        {
            case 1:
                GraphContainer1.GetComponent<WindowGraph>().setList1(List1_1);
                GraphContainer1.GetComponent<WindowGraph>().setList2(List1_2);
                GraphContainer1.GetComponent<WindowGraph>().setList3(List1_3);
                GraphContainer1.GetComponent<WindowGraph>().setList4(List1_4);

                GraphContainer1.GetComponent<WindowGraph>().setAxisColor(Axis_Color1);

                GraphContainer1.GetComponent<WindowGraph>().setDotColor1(DotColor1_1);
                GraphContainer1.GetComponent<WindowGraph>().setLineColor1(LineColor1_1);

                GraphContainer1.GetComponent<WindowGraph>().setDotColor2(DotColor1_2);
                GraphContainer1.GetComponent<WindowGraph>().setLineColor2(LineColor1_2);
                          
                GraphContainer1.GetComponent<WindowGraph>().setDotColor3(DotColor1_3);
                GraphContainer1.GetComponent<WindowGraph>().setLineColor3(LineColor1_3);
                         
                GraphContainer1.GetComponent<WindowGraph>().setDotColor4(DotColor1_4);
                GraphContainer1.GetComponent<WindowGraph>().setLineColor4(LineColor1_4);

                GraphContainer1.GetComponent<WindowGraph>().setDotRadius(DotRadius1);
                GraphContainer1.GetComponent<WindowGraph>().setLineThickness(LineThickness1);
                GraphContainer1.GetComponent<WindowGraph>().setGridThickness(GridThickness1);
                GraphContainer1.GetComponent<WindowGraph>().setMarges(Marges1);
                GraphContainer1.GetComponent<WindowGraph>().setVerticaleGridSpace(VerticalGridSpace1);
                GraphContainer1.GetComponent<WindowGraph>().setHorizontalGridSpace(HorizontalGridSpace1);

                break;
            case 2:
                GraphContainer2.GetComponent<WindowGraph>().setList1(List2_1);
                GraphContainer2.GetComponent<WindowGraph>().setList2(List2_2);
                GraphContainer2.GetComponent<WindowGraph>().setList3(List2_3);
                GraphContainer2.GetComponent<WindowGraph>().setList4(List2_4);

                GraphContainer2.GetComponent<WindowGraph>().setAxisColor(Axis_Color2);

                GraphContainer2.GetComponent<WindowGraph>().setDotColor1(DotColor2_1);
                GraphContainer2.GetComponent<WindowGraph>().setLineColor1(LineColor2_1);

                GraphContainer2.GetComponent<WindowGraph>().setDotColor2(DotColor2_2);
                GraphContainer2.GetComponent<WindowGraph>().setLineColor2(LineColor2_2);

                GraphContainer2.GetComponent<WindowGraph>().setDotColor3(DotColor2_3);
                GraphContainer2.GetComponent<WindowGraph>().setLineColor3(LineColor2_3);

                GraphContainer2.GetComponent<WindowGraph>().setDotColor4(DotColor2_4);
                GraphContainer2.GetComponent<WindowGraph>().setLineColor4(LineColor2_4);

                GraphContainer2.GetComponent<WindowGraph>().setDotRadius(DotRadius2);
                GraphContainer2.GetComponent<WindowGraph>().setLineThickness(LineThickness2);
                GraphContainer2.GetComponent<WindowGraph>().setGridThickness(GridThickness2);
                GraphContainer2.GetComponent<WindowGraph>().setMarges(Marges2);
                GraphContainer2.GetComponent<WindowGraph>().setVerticaleGridSpace(VerticalGridSpace2);
                GraphContainer2.GetComponent<WindowGraph>().setHorizontalGridSpace(HorizontalGridSpace2);
                break;

            case 3:
                GraphContainer3.GetComponent<WindowGraph>().setList1(List3_1);
                GraphContainer3.GetComponent<WindowGraph>().setList2(List3_2);
                GraphContainer3.GetComponent<WindowGraph>().setList3(List3_3);
                GraphContainer3.GetComponent<WindowGraph>().setList4(List3_4);

                GraphContainer3.GetComponent<WindowGraph>().setAxisColor(Axis_Color3);

                GraphContainer3.GetComponent<WindowGraph>().setDotColor1(DotColor3_1);
                GraphContainer3.GetComponent<WindowGraph>().setLineColor1(LineColor3_1);
                              
                GraphContainer3.GetComponent<WindowGraph>().setDotColor2(DotColor3_2);
                GraphContainer3.GetComponent<WindowGraph>().setLineColor2(LineColor3_2);
                              
                GraphContainer3.GetComponent<WindowGraph>().setDotColor3(DotColor3_3);
                GraphContainer3.GetComponent<WindowGraph>().setLineColor3(LineColor3_3);
                              
                GraphContainer3.GetComponent<WindowGraph>().setDotColor4(DotColor3_4);
                GraphContainer3.GetComponent<WindowGraph>().setLineColor4(LineColor3_4);

                GraphContainer3.GetComponent<WindowGraph>().setDotRadius(DotRadius3);
                GraphContainer3.GetComponent<WindowGraph>().setLineThickness(LineThickness3);
                GraphContainer3.GetComponent<WindowGraph>().setGridThickness(GridThickness3);
                GraphContainer3.GetComponent<WindowGraph>().setMarges(Marges3);
                GraphContainer3.GetComponent<WindowGraph>().setVerticaleGridSpace(VerticalGridSpace3);
                GraphContainer3.GetComponent<WindowGraph>().setHorizontalGridSpace(HorizontalGridSpace3);
                break;

            case 4:
                GraphContainer4.GetComponent<WindowGraph>().setList1(List4_1);
                GraphContainer4.GetComponent<WindowGraph>().setList2(List4_2);
                GraphContainer4.GetComponent<WindowGraph>().setList3(List4_3);
                GraphContainer4.GetComponent<WindowGraph>().setList4(List4_4);

                GraphContainer4.GetComponent<WindowGraph>().setAxisColor(Axis_Color4);

                GraphContainer4.GetComponent<WindowGraph>().setDotColor1(DotColor4_1);
                GraphContainer4.GetComponent<WindowGraph>().setLineColor1(LineColor4_1);
                              
                GraphContainer4.GetComponent<WindowGraph>().setDotColor2(DotColor4_2);
                GraphContainer4.GetComponent<WindowGraph>().setLineColor2(LineColor4_2);
                              
                GraphContainer4.GetComponent<WindowGraph>().setDotColor3(DotColor4_3);
                GraphContainer4.GetComponent<WindowGraph>().setLineColor3(LineColor4_3);
                              
                GraphContainer4.GetComponent<WindowGraph>().setDotColor4(DotColor4_4);
                GraphContainer4.GetComponent<WindowGraph>().setLineColor4(LineColor4_4);

                GraphContainer4.GetComponent<WindowGraph>().setDotRadius(DotRadius4);
                GraphContainer4.GetComponent<WindowGraph>().setLineThickness(LineThickness4);
                GraphContainer4.GetComponent<WindowGraph>().setGridThickness(GridThickness4);
                GraphContainer4.GetComponent<WindowGraph>().setMarges(Marges4);
                GraphContainer4.GetComponent<WindowGraph>().setVerticaleGridSpace(VerticalGridSpace4);
                GraphContainer4.GetComponent<WindowGraph>().setHorizontalGridSpace(HorizontalGridSpace4);
                break;
        }
    }
}
