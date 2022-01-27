using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public GameObject[] graph = new GameObject[4];
    public TextMeshProUGUI[] title = new TextMeshProUGUI[4];

    public TMP_Dropdown[] dropdown1 = new TMP_Dropdown[4];
    public TMP_Dropdown[] dropdown2 = new TMP_Dropdown[4];
    public TMP_Dropdown[] dropdown3 = new TMP_Dropdown[4];
    public TMP_Dropdown[] dropdown4 = new TMP_Dropdown[4];

    public GameObject optionsManager, graphManager;

    [SerializeField] private int _saveSlot, _loadSlot;

    public void Awake()
    {
        SaveSystem.Init();
    }

    public void Save(int saveSlot)
    {

        SaveObject saveObject = new SaveObject { };

        //Saving datas into json file
        for (int i = 0; i < 4; i++)
        {
            saveObject.title[i] = title[i].text;
            saveObject.nbDataMean[i] = graph[i].GetComponent<WindowGraph>().GetNbMeanElement();
            saveObject.xGrid[i] = graph[i].GetComponent<WindowGraph>().getVerticalGridSpace();
            saveObject.yGrid[i] = graph[i].GetComponent<WindowGraph>().getHorizontalGridSpace();
            saveObject.dotRadius[i] = graph[i].GetComponent<WindowGraph>().GetDotRadius();
            saveObject.lineThickness[i] = graph[i].GetComponent<WindowGraph>().GetLineThickness();
            saveObject.dotColor1[i] = graph[i].GetComponent<WindowGraph>().GetDotColor(0);
            saveObject.dotColor2[i] = graph[i].GetComponent<WindowGraph>().GetDotColor(1);
            saveObject.dotColor3[i] = graph[i].GetComponent<WindowGraph>().GetDotColor(2);
            saveObject.dotColor4[i] = graph[i].GetComponent<WindowGraph>().GetDotColor(3);
            saveObject.lineColor1[i] = graph[i].GetComponent<WindowGraph>().GetLineColor(0);
            saveObject.lineColor2[i] = graph[i].GetComponent<WindowGraph>().GetLineColor(1);
            saveObject.lineColor3[i] = graph[i].GetComponent<WindowGraph>().GetLineColor(2);
            saveObject.lineColor4[i] = graph[i].GetComponent<WindowGraph>().GetLineColor(3);
            saveObject.listName1[i] = graph[i].GetComponent<WindowGraph>().GetListName(0);
            saveObject.listName2[i] = graph[i].GetComponent<WindowGraph>().GetListName(1);
            saveObject.listName3[i] = graph[i].GetComponent<WindowGraph>().GetListName(2);
            saveObject.listName4[i] = graph[i].GetComponent<WindowGraph>().GetListName(3);
        }

        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json, saveSlot);

    }

    public void Load(int saveSlot)
    {
        string saveString = SaveSystem.Load(saveSlot);

        //Reassigning datas from json file to gameobjects
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            for (int i = 0; i < 4; i++)
            {
                title[i].text = saveObject.title[i];
                graph[i].GetComponent<WindowGraph>().SetEnableMean(saveObject.enableMean[i]);
                graph[i].GetComponent<WindowGraph>().SetNbMeanElement(saveObject.nbDataMean[i]);
                graph[i].GetComponent<WindowGraph>().setVerticaleGridSpace(saveObject.xGrid[i]);
                graph[i].GetComponent<WindowGraph>().setHorizontalGridSpace(saveObject.yGrid[i]);
                graph[i].GetComponent<WindowGraph>().setDotRadius(saveObject.dotRadius[i]);
                graph[i].GetComponent<WindowGraph>().setLineThickness(saveObject.lineThickness[i]);
                graph[i].GetComponent<WindowGraph>().setDotColor1(saveObject.dotColor1[i]);
                graph[i].GetComponent<WindowGraph>().setDotColor2(saveObject.dotColor2[i]);
                graph[i].GetComponent<WindowGraph>().setDotColor3(saveObject.dotColor3[i]);
                graph[i].GetComponent<WindowGraph>().setDotColor4(saveObject.dotColor4[i]);
                graph[i].GetComponent<WindowGraph>().setLineColor1(saveObject.lineColor1[i]);
                graph[i].GetComponent<WindowGraph>().setLineColor2(saveObject.lineColor2[i]);
                graph[i].GetComponent<WindowGraph>().setLineColor3(saveObject.lineColor3[i]);
                graph[i].GetComponent<WindowGraph>().setLineColor4(saveObject.lineColor4[i]);
                graph[i].GetComponent<WindowGraph>().SetListName(0, saveObject.listName1[i]);
                graph[i].GetComponent<WindowGraph>().SetListName(1, saveObject.listName1[i]);
                graph[i].GetComponent<WindowGraph>().SetListName(2, saveObject.listName1[i]);
                graph[i].GetComponent<WindowGraph>().SetListName(3, saveObject.listName1[i]);

                //Restore list
                int index = graphManager.GetComponent<GraphManager>().FindListIndex(saveObject.listName1[i]);
                graphManager.GetComponent<GraphManager>().ChangeList(index, i + 1, 1);

                switch (i) {
                    case 0: dropdown1[0].value = index; break;
                    case 1: dropdown2[0].value = index; break;
                    case 2: dropdown3[0].value = index; break;
                    case 3: dropdown4[0].value = index; break;
                }

                index = graphManager.GetComponent<GraphManager>().FindListIndex(saveObject.listName2[i]);
                graphManager.GetComponent<GraphManager>().ChangeList(index, i + 1, 2);

                switch (i)
                {
                    case 0: dropdown1[1].value = index; break;
                    case 1: dropdown2[1].value = index; break;
                    case 2: dropdown3[1].value = index; break;
                    case 3: dropdown4[1].value = index; break;
                }

                index = graphManager.GetComponent<GraphManager>().FindListIndex(saveObject.listName3[i]);
                graphManager.GetComponent<GraphManager>().ChangeList(index, i + 1, 3);

                switch (i)
                {
                    case 0: dropdown1[2].value = index; break;
                    case 1: dropdown2[2].value = index; break;
                    case 2: dropdown3[2].value = index; break;
                    case 3: dropdown4[2].value = index; break;
                }

                index = graphManager.GetComponent<GraphManager>().FindListIndex(saveObject.listName4[i]);
                graphManager.GetComponent<GraphManager>().ChangeList(index, i + 1, 4);

                switch (i)
                {
                    case 0: dropdown1[3].value = index; break;
                    case 1: dropdown2[3].value = index; break;
                    case 2: dropdown3[3].value = index; break;
                    case 3: dropdown4[3].value = index; break;
                }
            }
            optionsManager.GetComponent<RefreshContent>().RefreshContentFunction();

        }
        else
        {
            //No save
        }
    }

    public void ExecuteSaveOrLoad()
    {
        if (_saveSlot != 0)
        {
            Save(_saveSlot);
        }
        else if (_loadSlot != 0) {
            Load(_loadSlot); 
        }

        _saveSlot = 0;
        _loadSlot = 0;
    }

    public void setSaveSlot(int index) { _saveSlot = index; }
    public void setLoadSlot(int index) { _loadSlot = index; }
    public int GetLoadSlot() { return _loadSlot; }
    public int GetSaveSlot() { return _saveSlot; }

    private class SaveObject
    {
        public string[] title = new string[4];
        public bool[] enableMean = new bool[4];
        public int[] nbDataMean = new int[4];
        public int[] xGrid = new int[4];
        public int[] yGrid = new int[4];
        public int[] dotRadius = new int[4];
        public int[] lineThickness = new int[4];
        public Color[] dotColor1 = new Color[4];
        public Color[] dotColor2 = new Color[4];
        public Color[] dotColor3 = new Color[4];
        public Color[] dotColor4 = new Color[4];
        public Color[] lineColor1 = new Color[4];
        public Color[] lineColor2 = new Color[4];
        public Color[] lineColor3 = new Color[4];
        public Color[] lineColor4 = new Color[4];
        public string[] listName1 = new string[4];
        public string[] listName2 = new string[4];
        public string[] listName3 = new string[4];
        public string[] listName4 = new string[4];
    }
}

