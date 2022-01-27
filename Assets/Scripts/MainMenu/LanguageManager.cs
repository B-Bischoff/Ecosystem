using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    private string currentLanguage;

    public TextMeshProUGUI madeBy;

    [Header("Terrain")]
    public GameObject size;
    public GameObject offset, zoom, seed;

    [Header("Bunny")]
    public GameObject bunny_speedA;
    public GameObject bunny_viewA, bunny_consumTimeA, bunny_speedB, bunny_viewB, bunny_consumTimeB, bunny_growTime, bunny_reproHunger, bunny_reproWater;
    public GameObject bunny_gestTime, bunny_litter, bunny_reduRate, bunny_hungerRestored, bunny_thirstRestored, bunny_hungerLimit, bunny_thirstLimit;

    [Header("Fox")]
    public GameObject fox_speedA;
    public GameObject fox_viewA, fox_consumTimeA, fox_speedB, fox_viewB, fox_consumTimeB, fox_growTime, fox_reproHunger, fox_reproWater;
    public GameObject fox_gestTime, fox_litter, fox_reduRate, fox_hungerRestored, fox_thirstRestored, fox_hungerLimit, fox_thirstLimit;

    [Header("Save and load")]
    public GameObject[] saves = new GameObject[5];
    public GameObject[] loads    = new GameObject[5];

    private void Start()
    {
        StartCoroutine("InitLanguage");
        currentLanguage = LocalizationSettings.SelectedLocale.ToString();
        UpdateInfoBubble();
    }

    private void Update()
    {
        if (currentLanguage != LocalizationSettings.SelectedLocale.ToString())
        {
            currentLanguage = LocalizationSettings.SelectedLocale.ToString();
            UpdateInfoBubble();
        }    
    }

    public void UpdateInfoBubble()
    {
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            madeBy.text = "Réalisé par\nBrice Bischoff";

            // Terrain
            size.GetComponent<TooltipTrigger>().content = "Changer la longueur du terrain";
            size.GetComponent<TooltipTrigger>().header = "Taille du terrain";
            offset.GetComponent<TooltipTrigger>().content = "Déplacer la génération du terrain";
            offset.GetComponent<TooltipTrigger>().header = "Déplacement génération";
            zoom.GetComponent<TooltipTrigger>().content = "Zoomer ou réduire l'aspect du terrain";
            zoom.GetComponent<TooltipTrigger>().header = "Aspect terrain";
            seed.GetComponent<TooltipTrigger>().content = "La génération du terrain dépend de l'indice de génération";
            seed.GetComponent<TooltipTrigger>().header = "Indice de génération";
            // Bunny
            bunny_speedA.GetComponent<TooltipTrigger>().content = "Vitesse de déplacement";
            bunny_speedA.GetComponent<TooltipTrigger>().header = "Vitesse";
            bunny_viewA.GetComponent<TooltipTrigger>().content = "distance à partir de laquelle l'animal voit l'eau et la nourriture, symbolisé par la sphere rouge";
            bunny_viewA.GetComponent<TooltipTrigger>().header = "Champ de vision";
            bunny_consumTimeA.GetComponent<TooltipTrigger>().content = "Temps (en secondes) requis pour se nourrir ou boire";
            bunny_consumTimeA.GetComponent<TooltipTrigger>().header = "Temps de consommation";
            bunny_speedB.GetComponent<TooltipTrigger>().content = "Vitesse de déplacement";
            bunny_speedB.GetComponent<TooltipTrigger>().header = "Vitesse";
            bunny_viewB.GetComponent<TooltipTrigger>().content = "Champ de vision, symbolisé par la sphere rouge";
            bunny_viewB.GetComponent<TooltipTrigger>().header = "Champ de vision";
            bunny_consumTimeB.GetComponent<TooltipTrigger>().content = "Temps (en secondes) requis pour se nourrir ou boire";
            bunny_consumTimeB.GetComponent<TooltipTrigger>().header = "Temps de consommation";
            bunny_growTime.GetComponent<TooltipTrigger>().content = "Temps (en secondes) requis pour arriver à maturité";
            bunny_growTime.GetComponent<TooltipTrigger>().header = "Temps de croissance";
            bunny_reproHunger.GetComponent<TooltipTrigger>().content = "Quantité de nourriture requise pour se reproduire";
            bunny_reproHunger.GetComponent<TooltipTrigger>().header = "Faim pour se reproduire";
            bunny_reproWater.GetComponent<TooltipTrigger>().content = "Quantité d'eau requise pour se reproduire";
            bunny_reproWater.GetComponent<TooltipTrigger>().header = "Eau pour se reproduire";
            bunny_gestTime.GetComponent<TooltipTrigger>().content = "Durée (en secondes) de la période de gestation";
            bunny_gestTime.GetComponent<TooltipTrigger>().header = "Periode de gestation";
            bunny_litter.GetComponent<TooltipTrigger>().content = "Le nombre de bébé est généré entre ces 2 nombres (tous 2 inclus)";
            bunny_litter.GetComponent<TooltipTrigger>().header = "Bébé par portée";
            bunny_reduRate.GetComponent<TooltipTrigger>().content = "Quantité d'eau et de nourriture depensé chaque seconde";
            bunny_reduRate.GetComponent<TooltipTrigger>().header = "Vitesse réduction eau et nourriture";
            bunny_hungerRestored.GetComponent<TooltipTrigger>().content = "Quantité de nourriture restaurée";
            bunny_hungerRestored.GetComponent<TooltipTrigger>().header = "Valeur nourriture";
            bunny_thirstRestored.GetComponent<TooltipTrigger>().content = "Niveau d'eau restoré";
            bunny_thirstRestored.GetComponent<TooltipTrigger>().header = "Regain d'hydratation";
            bunny_hungerLimit.GetComponent<TooltipTrigger>().content = "L'animal cherchera de la nourriture quand sa faim sera inférieure ou égale à la valeur indiquée";
            bunny_hungerLimit.GetComponent<TooltipTrigger>().header = "Limite de faim";
            bunny_thirstLimit.GetComponent<TooltipTrigger>().content = "L'animal cherchera de l'eau quand sa faim sera inférieure ou égale à la valeur indiquée";
            bunny_thirstLimit.GetComponent<TooltipTrigger>().header = "Limite d'hydratation";
            // Fox
            fox_speedA.GetComponent<TooltipTrigger>().content = "Vitesse de déplacement";
            fox_speedA.GetComponent<TooltipTrigger>().header = "Vitesse";
            fox_viewA.GetComponent<TooltipTrigger>().content = "distance à partir de laquelle l'animal voit l'eau et la nourriture, symbolisé par la sphere rouge";
            fox_viewA.GetComponent<TooltipTrigger>().header = "Champ de vision";
            fox_consumTimeA.GetComponent<TooltipTrigger>().content = "Temps (en secondes) requis pour se nourrir ou boire";
            fox_consumTimeA.GetComponent<TooltipTrigger>().header = "Temps de consommation";
            fox_speedB.GetComponent<TooltipTrigger>().content = "Vitesse de déplacement";
            fox_speedB.GetComponent<TooltipTrigger>().header = "Vitesse";
            fox_viewB.GetComponent<TooltipTrigger>().content = "Champ de vision, symbolisé par la sphere rouge";
            fox_viewB.GetComponent<TooltipTrigger>().header = "Champ de vision";
            fox_consumTimeB.GetComponent<TooltipTrigger>().content = "Temps (en secondes) requis pour se nourrir ou boire";
            fox_consumTimeB.GetComponent<TooltipTrigger>().header = "Temps de consommation";
            fox_growTime.GetComponent<TooltipTrigger>().content = "Temps (en secondes) requis pour arriver à maturité";
            fox_growTime.GetComponent<TooltipTrigger>().header = "Temps de croissance";
            fox_reproHunger.GetComponent<TooltipTrigger>().content = "Quantité de nourriture requise pour se reproduire";
            fox_reproHunger.GetComponent<TooltipTrigger>().header = "Faim pour se reproduire";
            fox_reproWater.GetComponent<TooltipTrigger>().content = "Quantité d'eau requise pour se reproduire";
            fox_reproWater.GetComponent<TooltipTrigger>().header = "Eau pour se reproduire";
            fox_gestTime.GetComponent<TooltipTrigger>().content = "Durée (en secondes) de la période de gestation";
            fox_gestTime.GetComponent<TooltipTrigger>().header = "Periode de gestation";
            fox_litter.GetComponent<TooltipTrigger>().content = "Le nombre de bébé est généré entre ces 2 nombres (tous 2 inclus)";
            fox_litter.GetComponent<TooltipTrigger>().header = "Bébé par portée";
            fox_reduRate.GetComponent<TooltipTrigger>().content = "Quantité d'eau et de nourriture depensé chaque seconde";
            fox_reduRate.GetComponent<TooltipTrigger>().header = "Vitesse réduction eau et nourriture";
            fox_hungerRestored.GetComponent<TooltipTrigger>().content = "Quantité de nourriture restaurée";
            fox_hungerRestored.GetComponent<TooltipTrigger>().header = "Valeur nourriture";
            fox_thirstRestored.GetComponent<TooltipTrigger>().content = "Niveau d'eau restoré";
            fox_thirstRestored.GetComponent<TooltipTrigger>().header = "Regain d'hydratation";
            fox_hungerLimit.GetComponent<TooltipTrigger>().content = "L'animal cherchera de la nourriture quand sa faim sera inférieure ou égale à la valeur indiquée";
            fox_hungerLimit.GetComponent<TooltipTrigger>().header = "Limite de faim";
            fox_thirstLimit.GetComponent<TooltipTrigger>().content = "L'animal cherchera de l'eau quand sa faim sera inférieure ou égale à la valeur indiquée";
            fox_thirstLimit.GetComponent<TooltipTrigger>().header = "Limite d'hydratation";

            // Save and load
            for (int i = 0; i < 5; i++)
            {
                saves[i].GetComponent<TooltipTrigger>().content = "Sauvegarder configuration";
                saves[i].GetComponent<TooltipTrigger>().header = "Emplacement : " + (i + 1).ToString();
                loads[i].GetComponent<TooltipTrigger>().content = "Charger configuration";
                loads[i].GetComponent<TooltipTrigger>().header = "Emplacement : " + (i + 1).ToString();
            }
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            madeBy.text = "Made by\nBrice Bischoff";

            // Terrain
            size.GetComponent<TooltipTrigger>().content = "Change the length of the terrain";
            size.GetComponent<TooltipTrigger>().header = "Size";
            offset.GetComponent<TooltipTrigger>().content = "Shift the terrain generation";
            offset.GetComponent<TooltipTrigger>().header = "Offset";
            zoom.GetComponent<TooltipTrigger>().content = "Enlarge or shrink terrain aspect";
            zoom.GetComponent<TooltipTrigger>().header = "Zoom";
            seed.GetComponent<TooltipTrigger>().content = "The terrain generation depend of the seed numbers";
            seed.GetComponent<TooltipTrigger>().header = "Seed";
            // Bunny
            bunny_speedA.GetComponent<TooltipTrigger>().content = "Movement speed";
            bunny_speedA.GetComponent<TooltipTrigger>().header = "Speed";
            bunny_viewA.GetComponent<TooltipTrigger>().content = "Distance from which the animal can target food or water. Symbolized by the red sphere on the terrain";
            bunny_viewA.GetComponent<TooltipTrigger>().header = "View distance";
            bunny_consumTimeA.GetComponent<TooltipTrigger>().content = "Time (in seconds) needed to eat food or drink";
            bunny_consumTimeA.GetComponent<TooltipTrigger>().header = "Consumption time";
            bunny_speedB.GetComponent<TooltipTrigger>().content = "Movement speed";
            bunny_speedB.GetComponent<TooltipTrigger>().header = "Speed";
            bunny_viewB.GetComponent<TooltipTrigger>().content = "Field of view. Symbolized by the red sphere on the terrain";
            bunny_viewB.GetComponent<TooltipTrigger>().header = "View distance";
            bunny_consumTimeB.GetComponent<TooltipTrigger>().content = "Time (in seconds) needed to eat food or drink";
            bunny_consumTimeB.GetComponent<TooltipTrigger>().header = "Consumption time";
            bunny_growTime.GetComponent<TooltipTrigger>().content = "Time (in seconds) needed to become a mature bunny";
            bunny_growTime.GetComponent<TooltipTrigger>().header = "Growth time";
            bunny_reproHunger.GetComponent<TooltipTrigger>().content = "Amount of food required to reproduce";
            bunny_reproHunger.GetComponent<TooltipTrigger>().header = "Hunger to reproduce";
            bunny_reproWater.GetComponent<TooltipTrigger>().content = "Amout of water required to reproduce";
            bunny_reproWater.GetComponent<TooltipTrigger>().header = "Thirst to reproduce";
            bunny_gestTime.GetComponent<TooltipTrigger>().content = "Duration (in seconds) of the gestational period";
            bunny_gestTime.GetComponent<TooltipTrigger>().header = "Gestational time";
            bunny_litter.GetComponent<TooltipTrigger>().content = "The number of baby is generated between those numbers (both included)";
            bunny_litter.GetComponent<TooltipTrigger>().header = "Baby per litter";
            bunny_reduRate.GetComponent<TooltipTrigger>().content = "Amount of food and water lost each second";
            bunny_reduRate.GetComponent<TooltipTrigger>().header = "Food and water reduction rate";
            bunny_hungerRestored.GetComponent<TooltipTrigger>().content = "Value given when the animal eat";
            bunny_hungerRestored.GetComponent<TooltipTrigger>().header = "Food value";
            bunny_thirstRestored.GetComponent<TooltipTrigger>().content = "Value given when the animal drink";
            bunny_thirstRestored.GetComponent<TooltipTrigger>().header = "Water value";
            bunny_hungerLimit.GetComponent<TooltipTrigger>().content = "The animal will search food when his hunger is equal or below the hunger limit";
            bunny_hungerLimit.GetComponent<TooltipTrigger>().header = "Hunger limit";
            bunny_thirstLimit.GetComponent<TooltipTrigger>().content = "The animal will search water when his thirst is equal or below the hunger limit";
            bunny_thirstLimit.GetComponent<TooltipTrigger>().header = "Thirst limit";
            // Fox
            fox_speedA.GetComponent<TooltipTrigger>().content = "Movement speed";
            fox_speedA.GetComponent<TooltipTrigger>().header = "Speed";
            fox_viewA.GetComponent<TooltipTrigger>().content = "Distance from which the animal can target food or water. Symbolized by the red sphere on the terrain";
            fox_viewA.GetComponent<TooltipTrigger>().header = "View distance";
            fox_consumTimeA.GetComponent<TooltipTrigger>().content = "Time (in seconds) needed to eat food or drink";
            fox_consumTimeA.GetComponent<TooltipTrigger>().header = "Consumption time";
            fox_speedB.GetComponent<TooltipTrigger>().content = "Movement speed";
            fox_speedB.GetComponent<TooltipTrigger>().header = "Speed";
            fox_viewB.GetComponent<TooltipTrigger>().content = "Field of view. Symbolized by the red sphere on the terrain";
            fox_viewB.GetComponent<TooltipTrigger>().header = "View distance";
            fox_consumTimeB.GetComponent<TooltipTrigger>().content = "Time (in seconds) needed to eat food or drink";
            fox_consumTimeB.GetComponent<TooltipTrigger>().header = "Consumption time";
            fox_growTime.GetComponent<TooltipTrigger>().content = "Time (in seconds) needed to become a mature fox";
            fox_growTime.GetComponent<TooltipTrigger>().header = "Growth time";
            fox_reproHunger.GetComponent<TooltipTrigger>().content = "Amount of food required to reproduce";
            fox_reproHunger.GetComponent<TooltipTrigger>().header = "Hunger to reproduce";
            fox_reproWater.GetComponent<TooltipTrigger>().content = "Amout of water required to reproduce";
            fox_reproWater.GetComponent<TooltipTrigger>().header = "Thirst to reproduce";
            fox_gestTime.GetComponent<TooltipTrigger>().content = "Duration (in seconds) of the gestational period";
            fox_gestTime.GetComponent<TooltipTrigger>().header = "Gestational time";
            fox_litter.GetComponent<TooltipTrigger>().content = "The number of baby is generated between those numbers (both included)";
            fox_litter.GetComponent<TooltipTrigger>().header = "Baby per litter";
            fox_reduRate.GetComponent<TooltipTrigger>().content = "Amount of food and water lost each second";
            fox_reduRate.GetComponent<TooltipTrigger>().header = "Food and water reduction rate";
            fox_hungerRestored.GetComponent<TooltipTrigger>().content = "Value given when the animal eat";
            fox_hungerRestored.GetComponent<TooltipTrigger>().header = "Food value";
            fox_thirstRestored.GetComponent<TooltipTrigger>().content = "Value given when the animal drink";
            fox_thirstRestored.GetComponent<TooltipTrigger>().header = "Water value";
            fox_hungerLimit.GetComponent<TooltipTrigger>().content = "The animal will search food when his hunger is equal or below the hunger limit";
            fox_hungerLimit.GetComponent<TooltipTrigger>().header = "Hunger limit";
            fox_thirstLimit.GetComponent<TooltipTrigger>().content = "The animal will search water when his thirst is equal or below the hunger limit";
            fox_thirstLimit.GetComponent<TooltipTrigger>().header = "Thirst limit";

            // Save and load
            for (int i = 0; i < 5; i++)
            {
                saves[i].GetComponent<TooltipTrigger>().content = "Save presets";
                saves[i].GetComponent<TooltipTrigger>().header = "Slot : " + (i + 1).ToString();
                loads[i].GetComponent<TooltipTrigger>().content = "Load presets";
                loads[i].GetComponent<TooltipTrigger>().header = "Slot : " + (i + 1).ToString();
            }
        }
    }

    public IEnumerator InitLanguage()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Generate list of available Locales
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selected = i;
            options.Add(new TMP_Dropdown.OptionData(locale.name));
        }
        dropdown.options = options;

        dropdown.value = selected;
        dropdown.onValueChanged.AddListener(LocaleSelected);
    }

    static void LocaleSelected(int index)
    {
        // Select language with dropdown list
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
