using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class DropdownGraphData : MonoBehaviour
{
    public GameObject GraphManager;

    public TMP_Dropdown Dropdown1_1, Dropdown1_2, Dropdown1_3, Dropdown1_4;
    public TMP_Dropdown Dropdown2_1, Dropdown2_2, Dropdown2_3, Dropdown2_4;
    public TMP_Dropdown Dropdown3_1, Dropdown3_2, Dropdown3_3, Dropdown3_4;
    public TMP_Dropdown Dropdown4_1, Dropdown4_2, Dropdown4_3, Dropdown4_4;

    private int _graphNumber, _dataNumber;

    private string _none, _bunnyNumber, _bunnyDead, _bunnyDehydrated, _bunnyStarved, _bunnyEaten, _carrotNumber, _foxNumber, _foxDead, _foxDehydrated, _foxStarved;

    void Awake()
    {
        InitLanguage();
    }

    void Update()
    {

    }

    public void SelectData(int index)
    {
        GraphManager.GetComponent<GraphManager>().ChangeList(index, _graphNumber, _dataNumber);
    }

    public void SendGraphNumber(int graphNumber){_graphNumber = graphNumber; }
    public void SendDataNumber(int dataNumber) { _dataNumber = dataNumber; }


    private void InitLanguage()
    {
        // Setting strings in selected language
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            _none = "Aucune";

            _bunnyNumber = "Nombre lapin";
            _bunnyDead = "Lapin mort";
            _bunnyDehydrated = "Lapin déshydraté";
            _bunnyStarved = "Lapin affamé";
            _bunnyEaten = "Lapin chassé";

            _carrotNumber = "Nombre carotte";

            _foxNumber = "Nombre renard";
            _foxDead = "Renard mort";
            _foxDehydrated = "Renard déshydraté";
            _foxStarved = "Renard affamé";
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            _none = "None";

            _bunnyNumber = "Bunny number";
            _bunnyDead = "Bunny dead";
            _bunnyDehydrated = "Bunny dehydrated";
            _bunnyStarved = "Bunny starved";
            _bunnyEaten = "Bunny eaten";

            _carrotNumber = "Carrot number";

            _foxNumber = "Fox number";
            _foxDead = "Fox dead";
            _foxDehydrated = "Fox dehydrated";
            _foxStarved = "Fox starved";
        }

        // Setting dropdown options
        SetDropdownLangauge(Dropdown1_1);
        SetDropdownLangauge(Dropdown1_2);
        SetDropdownLangauge(Dropdown1_3);
        SetDropdownLangauge(Dropdown1_4);

        SetDropdownLangauge(Dropdown2_1);
        SetDropdownLangauge(Dropdown2_2);
        SetDropdownLangauge(Dropdown2_3);
        SetDropdownLangauge(Dropdown2_4);

        SetDropdownLangauge(Dropdown3_1);
        SetDropdownLangauge(Dropdown3_2);
        SetDropdownLangauge(Dropdown3_3);
        SetDropdownLangauge(Dropdown3_4);

        SetDropdownLangauge(Dropdown4_1);
        SetDropdownLangauge(Dropdown4_2);
        SetDropdownLangauge(Dropdown4_3);
        SetDropdownLangauge(Dropdown4_4);
    }

    private void SetDropdownLangauge(TMP_Dropdown dropdown)
    {
        List<string> list = new List<string> { 
            _none,
            _bunnyNumber,
            _bunnyDead,
            _bunnyDehydrated,
            _bunnyStarved,
            _bunnyEaten,
            _carrotNumber,
            _foxNumber,
            _foxDead,
            _foxDehydrated,
            _foxStarved
        };

        dropdown.ClearOptions();
        dropdown.AddOptions(list);
    }
}
