using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class RawInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI carrot, bunny, fox;
    public GameObject bunnyGo, foxGo;

    // String defined in "InitLanguage" according to the language selected
    private string _baby, _adult, _carrot, _bunny, _fox;

    private void Awake()
    {
        InitLanguage();
    }

    public void Update()
    {
        // Raw informations
        carrot.text = _carrot + " : " + NatureManager.carrotsList.Count;
        bunny.text = _bunny + " : " + (AnimalsManager.bunnyList.Count + AnimalsManager.babyBunnyList.Count);
        fox.text = _fox + " : " + (AnimalsManager.foxList.Count + AnimalsManager.babyFoxList.Count);

        // Bunny tooltip 
        bunnyGo.GetComponent<TooltipTrigger>().header = _bunny;
        bunnyGo.GetComponent<TooltipTrigger>().content = _adult + " : " + AnimalsManager.bunnyList.Count + "\n" + _baby + " : " + AnimalsManager.babyBunnyList.Count;

        // Fox tooltip
        foxGo.GetComponent<TooltipTrigger>().header = _fox;
        foxGo.GetComponent<TooltipTrigger>().content = _adult + " : " + AnimalsManager.foxList.Count + "\n" + _baby + " : " + AnimalsManager.babyFoxList.Count;
    }
    
    private void InitLanguage()
    {
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            _baby = "Bébé";
            _adult = "Adulte";
            _carrot = "Carotte";
            _bunny = "Lapin";
            _fox = "Renard";
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            _baby = "Baby";
            _adult = "Adult";
            _carrot = "Carrot";
            _bunny = "Bunny";
            _fox = "Fox";
        }
    }
}

