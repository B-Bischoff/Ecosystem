using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Terrain_MainMenu : MonoBehaviour
{
    public MeshCreator terrain;
    public TextMeshProUGUI zoomTxt, xSizeTxt, zSizeTxt;
    public TMP_InputField xOffset, zOffset, seed;
    public Slider xSizeSlider, zSizeSlider, zoomSlider;

    public int zSize, xSize, seedNum;
    public float xOffsetValue, zOffsetValue, zoom;

    public void Start()
    {
        xSizeTxt.text = terrain.xSize.ToString();
        xSizeSlider.value = terrain.xSize;
        zSizeTxt.text = terrain.zSize.ToString();
        zSizeSlider.value = terrain.zSize;
        xOffset.text = terrain.offset.x.ToString();
        zOffset.text = terrain.offset.y.ToString();
        seed.text = terrain.seed.ToString();
        zoomTxt.text = terrain.noiseScale.ToString();
        zoomSlider.value = terrain.noiseScale;
    }


    public void SetXSizeFloat(float newXSize)
    {
        terrain.xSize = (int)newXSize;
        xSizeTxt.text = newXSize.ToString();

        Camera.main.GetComponent<CamTerrainPreview>().FindTargetPos();
        Camera.main.GetComponent<CamTerrainPreview>().ForceCamReposition();

        xSize = (int)newXSize;

        UpdateMesh();
    }

    public void SetZSizeFloat(float newZSize)
    {
        terrain.zSize = (int)newZSize;
        zSizeTxt.text = newZSize.ToString();

        Camera.main.GetComponent<CamTerrainPreview>().FindTargetPos();
        Camera.main.GetComponent<CamTerrainPreview>().ForceCamReposition();

        zSize = (int)newZSize;

        UpdateMesh();
    }

    public void SetXOffset(string newXOffset)
    {
        float.TryParse(newXOffset, out float input);
        terrain.offset.x = input;

        xOffsetValue = input;
    }

    public void SetZOffset(string newZOffset)
    {
        float.TryParse(newZOffset, out float input);
        terrain.offset.y = input;

        zOffsetValue = input;
    }
    public void PlusMinusXOffsetValue(float value)
    {
        terrain.offset.x += value;
        xOffset.text = terrain.offset.x.ToString();
        xOffsetValue = terrain.offset.x;
        UpdateMesh();
    }
    public void PlusMinusZOffsetValue(float value)
    {
        terrain.offset.y += value;
        zOffset.text = terrain.offset.y.ToString();
        zOffsetValue = terrain.offset.y;
        UpdateMesh();
    }

    public void PlusMinusZSize(int value)
    {
        if (value + terrain.zSize > 254 || value + terrain.zSize < 32)
            value = 0;

        terrain.zSize += value;
        zSizeTxt.text = terrain.zSize.ToString();

        Camera.main.GetComponent<CamTerrainPreview>().FindTargetPos();
        Camera.main.GetComponent<CamTerrainPreview>().ForceCamReposition();

        zSize = terrain.zSize;

        UpdateMesh();
    }

    public void PlusMinusXSize(int value)
    {
        if (value + terrain.xSize > 254 || value + terrain.xSize < 32)
            value = 0;

        terrain.xSize += value;
        xSizeTxt.text = terrain.xSize.ToString();

        Camera.main.GetComponent<CamTerrainPreview>().FindTargetPos();
        Camera.main.GetComponent<CamTerrainPreview>().ForceCamReposition();

        xSize = terrain.xSize;

        UpdateMesh();
    }

    public void SetZoom(float newZoom)
    {
        terrain.noiseScale = newZoom;
        zoomTxt.text = terrain.noiseScale.ToString();

        zoom = newZoom;
    }

    public void SetSeed(string newSeed)
    {
        int.TryParse(newSeed, out int input);
        terrain.seed = input;
        seedNum = input;
    }

    public void PlusMinusSeed(int value)
    {
        terrain.seed += value;
        seed.text = terrain.seed.ToString();

        seedNum = terrain.seed;

        UpdateMesh();
    }


    public void UpdateMesh() => terrain.CreateMesh();

    public void SavePlayerPrefs()
    {
        if (zSize <= 0) 
            zSize = 127;
        if (xSize <= 0)
            xSize = 127;
        if (zoom <= 0)
            zoom = 40;

        PlayerPrefs.SetFloat("TerrainXOffset", xOffsetValue);
        PlayerPrefs.SetFloat("TerrainZOffset", zOffsetValue);
        PlayerPrefs.SetFloat("TerrainZoom", zoom);
        PlayerPrefs.SetInt("TerrainZSize", zSize);
        PlayerPrefs.SetInt("TerrainXSize", xSize);
        PlayerPrefs.SetInt("TerrainSeed", seedNum);
    }

    public void LoadPlayerPrefs()
    {
        xOffsetValue = PlayerPrefs.GetFloat("TerrainXOffset");
        xOffset.text = xOffsetValue.ToString();

        zOffsetValue = PlayerPrefs.GetFloat("TerrainZOffset");
        zOffset.text = zOffsetValue.ToString();

        zoom = PlayerPrefs.GetFloat("TerrainZoom");
        zoomSlider.value = zoom;
        zoomTxt.text = zoom.ToString();

        zSize = PlayerPrefs.GetInt("TerrainZSize");
        zSizeSlider.value = zSize;
        zSizeTxt.text = zSize.ToString();

        xSize = PlayerPrefs.GetInt("TerrainXSize");
        xSizeSlider.value = xSize;
        xSizeTxt.text = xSize.ToString();

        seedNum = PlayerPrefs.GetInt("TerrainSeed");
        seed.text = seedNum.ToString();
    }
}
