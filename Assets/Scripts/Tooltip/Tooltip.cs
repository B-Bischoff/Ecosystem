using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;
    }

    public void Update()
    {
        Vector2 position = Input.mousePosition;

        float pivotX;
        float pivotY;
       
        if (position.x > Screen.width * 0.66f)
            pivotX = 1;
        else
            pivotX = 0;
        if (position.y > Screen.height * 0.66f)
            pivotY = 1;
        else
            pivotY = 0;

        gameObject.GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
}
