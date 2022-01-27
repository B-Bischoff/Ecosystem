using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker2 : MonoBehaviour
{
    public Color selectedColor;

    public bool colorChanged;

    public GameObject selectedGameobjectColor;

    public List<GameObject> LineList = new List<GameObject>(); //Contains every Line

    void Start()
    {
        for (int i = 0; i < LineList.Count; i++) //Looping through Lines
        {
            foreach(RectTransform color in LineList[i].transform) //Accessing each colors
            {
                color.gameObject.AddComponent<PickColor2>(); //Adding PickColor2 script on each color gameObject
                color.gameObject.GetComponent<PickColor2>().SetColorPicker(gameObject); //Assigning this gameobject as "colorPicker" on every color gameObject
                color.gameObject.GetComponent<PickColor2>().SetButtonMethod(); //Assigning method on button component
            }
        }
    }

    public void SetGameobjectColor(GameObject gameobject)
    {
        selectedGameobjectColor = gameobject;

        colorChanged = true; //Telling color just changed

        SetColor();
    }

    private void SetColor()
    { 
        selectedColor = selectedGameobjectColor.GetComponent<RawImage>().color; //Get color from a raw image component
    }

    public bool GetColorChanged() { return colorChanged; }
    public bool SetColorChanges(bool value) => colorChanged = value;
}
