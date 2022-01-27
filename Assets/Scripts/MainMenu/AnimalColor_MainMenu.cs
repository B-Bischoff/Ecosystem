using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalColor_MainMenu : MonoBehaviour
{
    public GameObject colorPicker;

    public RawImage bunnyMale, bunnyFemale, foxMale, foxFemale;

    public Material bunnyMaleMat, bunnyFemaleMat, foxMaleMat, foxFemaleMat;
    public Color bunnyMaleColor, bunnyFemaleColor, foxMaleColor, foxFemaleColor;

    private string species, gender;

    public void Awake()
    {
        //Main menu's images color
        bunnyMale.color = bunnyMaleMat.color;
        bunnyFemale.color = bunnyFemaleMat.color;

        foxMale.color = foxMaleMat.GetColor("Color_AAC4E4DA"); //Getting color properties from shader graph which is not needed for bunnys due to their simple color mat
        foxFemale.color = foxFemaleMat.GetColor("Color_50B22DC6");

        //Color variable assignement
        bunnyMaleColor = bunnyMaleMat.color;
        bunnyFemaleColor = bunnyFemaleMat.color;

        foxMaleColor = foxMaleMat.GetColor("Color_AAC4E4DA");
        foxFemaleColor = foxFemaleMat.GetColor("Color_50B22DC6");
    }

    public void Start()
    {
        colorPicker.SetActive(false);
    }

    public void Update()
    {
        if(colorPicker.GetComponent<ColorPicker2>().GetColorChanged())
        {
            if(species == "Bunny")
            {
                if (gender == "Male")
                {
                    bunnyMale.color = colorPicker.GetComponent<ColorPicker2>().selectedColor;
                    bunnyMaleColor = colorPicker.GetComponent<ColorPicker2>().selectedColor;

                    bunnyMaleMat.color = bunnyMaleColor;
                }
                else
                {
                    bunnyFemale.color = colorPicker.GetComponent<ColorPicker2>().selectedColor;
                    bunnyFemaleColor = colorPicker.GetComponent<ColorPicker2>().selectedColor;

                    bunnyFemaleMat.color = bunnyFemaleColor;
                }
            }
            else
            {
                if (gender == "Male")
                {
                    foxMale.color = colorPicker.GetComponent<ColorPicker2>().selectedColor;
                    foxMaleColor = colorPicker.GetComponent<ColorPicker2>().selectedColor;

                    foxMaleMat.SetColor("Color_AAC4E4DA", foxMaleColor);
                }
                else
                {
                    foxFemale.color = colorPicker.GetComponent<ColorPicker2>().selectedColor;
                    foxFemaleColor = colorPicker.GetComponent<ColorPicker2>().selectedColor;

                    foxFemaleMat.SetColor("Color_50B22DC6", foxFemaleColor);
                }
            }

            colorPicker.GetComponent<ColorPicker2>().SetColorChanges(false);

            if (colorPicker.activeSelf)
                colorPicker.SetActive(false);
        }
    }

    // Used while loading data from json
    public void UpdateMaterials()
    {
        bunnyMaleMat.color = bunnyMaleColor;
        bunnyFemaleMat.color = bunnyFemaleColor;
        foxMaleMat.SetColor("Color_AAC4E4DA", foxMaleColor);
        foxFemaleMat.SetColor("Color_50B22DC6", foxFemaleColor);
    }

    public void ActiveColorPicker()
    {
        if (colorPicker.activeSelf)
            colorPicker.SetActive(false);
        else
            colorPicker.SetActive(true);
    }

    public void SetSpecies(string newSpecies)
    {
        species = newSpecies;
    }

    public void SetGender(string newGender)
    {
        gender = newGender;
    }
}
