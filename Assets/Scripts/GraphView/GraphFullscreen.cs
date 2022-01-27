using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class GraphFullscreen : MonoBehaviour
{
    public GameObject graphContainer;
    public GameObject DarkBackground;
    public Texture2D Horizontal, Square;
    public int initialIndex;

    private bool _isFullscreen;

    private string _maximize, _minimize;

    private void Awake()
    {
        InitLanguage();
    }

    public void ToggleFullscreen()
    {
        if(_isFullscreen)
        {
            _isFullscreen = false;
            graphContainer.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            graphContainer.GetComponent<Transform>().SetSiblingIndex(initialIndex);
            DarkBackground.SetActive(false);
            SetDefaultAnchorPosition(initialIndex);
            GetComponent<RawImage>().texture = Square;

            // Tooltip update
            GetComponent<TooltipTrigger>().content = _maximize;
        }
        else
        {
            _isFullscreen = true;
            graphContainer.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
            graphContainer.GetComponent<Transform>().SetSiblingIndex(4);
            DarkBackground.SetActive(true);
            SetFullscrenAnchorPosition();
            GetComponent<RawImage>().texture = Horizontal;

            // Tooltip update
            GetComponent<TooltipTrigger>().content = _minimize;
        }
    }

    private void SetDefaultAnchorPosition(int index)
    {
        RectTransform rect = graphContainer.GetComponent<RectTransform>();
   
        switch(index)
        {
            case 0:
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
                rect.pivot = new Vector2(0, 1);
                rect.anchoredPosition = new Vector2(36, -54);
                break;

            case 1:
                rect.anchorMin = new Vector2(1, 1);
                rect.anchorMax = new Vector2(1, 1);
                rect.pivot = new Vector2(1, 1);
                rect.anchoredPosition = new Vector3(-36, -54, 0);
                break;

            case 2:
                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(0, 0);
                rect.pivot = new Vector2(0, 0);
                rect.anchoredPosition = new Vector3(36, 54, 0);
                break;

            case 3:
                rect.anchorMin = new Vector2(1, 0);
                rect.anchorMax = new Vector2(1, 0);
                rect.pivot = new Vector2(1, 0);
                rect.anchoredPosition = new Vector3(-36, 54, 0);
                break;
        }
    }

    private void SetFullscrenAnchorPosition()
    {
        RectTransform rect = graphContainer.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = new Vector2(0, 0);
    }

    private void InitLanguage()
    {
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            _maximize = "Plein écran";
            _minimize = "Minimiser";
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            _maximize = "Maximize";
            _minimize = "Minimize";
        }

        // Manually init at awake :
        if(_isFullscreen)
            GetComponent<TooltipTrigger>().content = _minimize;
        else
            GetComponent<TooltipTrigger>().content = _maximize;
    }
}
