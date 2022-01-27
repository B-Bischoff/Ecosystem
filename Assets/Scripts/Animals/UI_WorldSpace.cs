using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WorldSpace : MonoBehaviour
{
    [SerializeField]
    private Image _thirstForeGroundImage, _hungerForeGroundImage;
    [SerializeField]
    private Canvas _canvas;

    private float _thirst, _hunger;

    private void Start()
    {
        _canvas.enabled = false;
    }

    private void Update()
    {
        if (_canvas.enabled)
        {
            _thirst = gameObject.GetComponent<Animals>().getThirst() / 100;
            _hunger = gameObject.GetComponent<Animals>().getHunger() / 100;

            _thirstForeGroundImage.fillAmount = Mathf.Lerp(0, 1, _thirst);
            _hungerForeGroundImage.fillAmount = Mathf.Lerp(0, 1, _hunger);
        }
    }

    public void SetShowCanvas(bool value) => _canvas.enabled = value;
}
