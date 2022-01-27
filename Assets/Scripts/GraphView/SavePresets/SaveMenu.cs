using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using System;

public class SaveMenu : MonoBehaviour
{
    public GameObject loadimage, saveImage;
    public GameObject darkBackground, background;
    public GameObject darkBackgroundConfirm, backgroundConfirm;
    public TextMeshProUGUI confirmText;

    public GameObject saveButton;

    // Translation only
    public GameObject slot1, slot2, slot3, slot4, slot5;

    private bool _saveMenuDisplayed, _confirmMenuDisplayed;

    public void Awake()
    {
        InitLanguage();

        darkBackground.SetActive(false);
        background.SetActive(false);
        darkBackgroundConfirm.SetActive(false);
        backgroundConfirm.SetActive(false);
        saveImage.SetActive(false);
        loadimage.SetActive(false);
    }

    private void InitLanguage()
    {
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
           saveButton.GetComponent<TooltipTrigger>().content = "Sauvegarder et charger préréglages";
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
           saveButton.GetComponent<TooltipTrigger>().content = "Save & load presets";

        InitSlot(slot1, 1);
        InitSlot(slot2, 2);
        InitSlot(slot3, 3);
        InitSlot(slot4, 4);
        InitSlot(slot5, 5);
    }

    private void InitSlot(GameObject slot, int slotIndex)
    {
        if(LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            // Save
            Transform save = slot.transform.Find("Save");
            save.GetComponent<TooltipTrigger>().header = "Emplacement " + slotIndex.ToString();
            save.GetComponent<TooltipTrigger>().content = "Sauvegarder configuration";

            // Load
            Transform load = slot.transform.Find("Load");
            load.GetComponent<TooltipTrigger>().header = "Emplacement " + slotIndex.ToString();
            load.GetComponent<TooltipTrigger>().content = "Charger configuration";
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            // Save
            Transform save = slot.transform.Find("Save");
            save.GetComponent<TooltipTrigger>().header = "Slot " + slotIndex.ToString();
            save.GetComponent<TooltipTrigger>().content = "Save configuration";

            // Load
            Transform load = slot.transform.Find("Load");
            load.GetComponent<TooltipTrigger>().header = "Slot " + slotIndex.ToString();
            load.GetComponent<TooltipTrigger>().content = "Load configuration";
        }
    }

    public void ToogleSaveMenuDisplay()
    {
        if(_saveMenuDisplayed)
        {
            darkBackground.SetActive(false);
            background.SetActive(false);
            _saveMenuDisplayed = false;
        }
        else
        {
            darkBackground.SetActive(true);
            background.SetActive(true);
            _saveMenuDisplayed = true;
        }
    }

    public void ToogleConfirmMenu()
    {
        if (_confirmMenuDisplayed)
        {
            darkBackgroundConfirm.SetActive(false);
            backgroundConfirm.SetActive(false);
            _confirmMenuDisplayed = false;
            GetComponent<SaveManager>().setSaveSlot(0);
            GetComponent<SaveManager>().setLoadSlot(0);
        }
        else
        {
            darkBackgroundConfirm.SetActive(true);
            backgroundConfirm.SetActive(true);
            _confirmMenuDisplayed = true;
            setConfirmMenuText();
        }
    }

    public void setLoadSlot(int index) => GetComponent<SaveManager>().setLoadSlot(index);
    public void setSaveSlot(int index) => GetComponent<SaveManager>().setSaveSlot(index);

    public void setConfirmMenuText()
    {
        int saveSlot = GetComponent<SaveManager>().GetSaveSlot();
        int loadSlot = GetComponent<SaveManager>().GetLoadSlot();

        saveImage.SetActive(false);
        loadimage.SetActive(false);

        if (saveSlot != 0)
        {
            if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
                confirmText.text = "Etes vous sûr de sauvegarder dans l'emplacement n°" + saveSlot + " ?";
            else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
                confirmText.text = "Are you sure to save in slot n°" + saveSlot + " ?";

            saveImage.SetActive(true);
        }
        else if (loadSlot != 0)
        {
            if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
                confirmText.text = "Etes vous sûr de charger les données de l'emplacement n°" + loadSlot + " ?";
            else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
                confirmText.text = "Are you sure to load from slot n°" + loadSlot + " ?";

            loadimage.SetActive(true);
        }
    }
}
