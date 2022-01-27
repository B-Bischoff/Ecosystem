using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class SaveMenu_MainMenu : MonoBehaviour
{
    public GameObject loadimage, saveImage;
    public GameObject darkBackground, background;
    public GameObject darkBackgroundConfirm, backgroundConfirm;
    public TextMeshProUGUI confirmText;

    private bool _saveMenuDisplayed, _confirmMenuDisplayed;

    public void Awake()
    {
        darkBackground.SetActive(false);
        background.SetActive(false);
        darkBackgroundConfirm.SetActive(false);
        backgroundConfirm.SetActive(false);
        saveImage.SetActive(false);
        loadimage.SetActive(false);
    }

    public void ToogleSaveMenuDisplay()
    {
        if (_saveMenuDisplayed)
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
            GetComponent<SaveManager_MainMenu>().setSaveSlot(0);
            GetComponent<SaveManager_MainMenu>().setLoadSlot(0);
        }
        else
        {
            darkBackgroundConfirm.SetActive(true);
            backgroundConfirm.SetActive(true);
            _confirmMenuDisplayed = true;
            setConfirmMenuText();
        }
    }

    public void setLoadSlot(int index) => GetComponent<SaveManager_MainMenu>().setLoadSlot(index);
    public void setSaveSlot(int index) => GetComponent<SaveManager_MainMenu>().setSaveSlot(index);

    public void setConfirmMenuText()
    {
        int saveSlot = GetComponent<SaveManager_MainMenu>().GetSaveSlot();
        int loadSlot = GetComponent<SaveManager_MainMenu>().GetLoadSlot();

        saveImage.SetActive(false);
        loadimage.SetActive(false);

        if (saveSlot != 0)
        {
            if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
                confirmText.text = "Etes vous sûr de sauvegarder dans l'emplacement n°" + saveSlot + " ?";
            else
                confirmText.text = "Are you sure to save in slot n°" + saveSlot + " ?";
            saveImage.SetActive(true);
        }
        else if (loadSlot != 0)
        {
            if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
                confirmText.text = "Etes vous sûr de charger depuis l'emplacement n°" + loadSlot + " ?";
            else
                confirmText.text = "Are you sure to load from slot n°" + loadSlot + " ?";
            loadimage.SetActive(true);
        }
    }
}
