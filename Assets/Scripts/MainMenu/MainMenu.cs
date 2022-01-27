using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, selection, premade, terrain, bunny, fox, start, options; //Container UI
    public GameObject terrainPreview, bunnyPreview, foxPreview, mainMenuBackground;

    public Camera camera;

    public Toggle fullscreenToggle;

    public float animTime; //Dotween transition duration

    public List<TMP_InputField> terrainFields, bunnyFields, foxFields; //Contains all the TMPRO Input fields

    private List<GameObject> _containers = new List<GameObject>();

    private int _currentContainer = 0; // 0 : main menu | 1 : select | 2 : premade | 3 : terrain | 4 : bunny | 5 : fox | 6 : start | 7 : Options | 8 : Credits
    private int _terrainFieldIndex, _bunnyFieldIndex, _foxFieldIndex;

    private Vector3 _camInitPos;
    private Quaternion _camInitRot;

    public void Start()
    {
        _containers.Add(mainMenu);
        _containers.Add(selection);
        _containers.Add(premade);
        _containers.Add(terrain);
        _containers.Add(bunny);
        _containers.Add(fox);
        _containers.Add(start);
        _containers.Add(options);

        camera.GetComponent<CamTerrainPreview>().enabled = false;
        terrainPreview.SetActive(false);
        bunnyPreview.SetActive(false);
        foxPreview.SetActive(false);

        _camInitPos.x = 10;
        _camInitPos.y = 0;
        _camInitPos.z = -5;
        _camInitRot = camera.transform.rotation;
    }



    public void Update()
    {
        //Press Tab to change selected input filed
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            switch(_currentContainer)
            {
                case 1: //Terrain
                    if (terrainFields.Count <= _terrainFieldIndex)
                        _terrainFieldIndex = 0;

                    terrainFields[_terrainFieldIndex].Select();
                    _terrainFieldIndex++;
                    break;
                case 2: //Bunny
                    if (bunnyFields.Count <= _bunnyFieldIndex)
                        _bunnyFieldIndex = 0;

                    bunnyFields[_bunnyFieldIndex].Select();
                    _bunnyFieldIndex++;
                    break;
                case 3: //Fox
                    if (foxFields.Count <= _foxFieldIndex)
                        _foxFieldIndex = 0;

                    foxFields[_foxFieldIndex].Select();
                    _foxFieldIndex++;
                    break;
            }
        }
    }

     /* Function used to keep track of the current container displayed in order to enable or disable gameobjects such as terrainPreview or animalPreview when it is required */
    public void SetContainerIndex(int index) => _currentContainer = index;

    public void Navigate(string move)
    {
        /* Moves all the container to navigate through the UI according to the input */ 
        switch (move) {
            case "up":
                foreach (GameObject container in _containers) {
                    container.transform.DOMoveY(container.transform.position.y - Screen.height, animTime);
                }
                break;
            case "down":
                foreach (GameObject container in _containers) {
                    container.transform.DOMoveY(container.transform.position.y + Screen.height, animTime);
                }
                break;
            case "right":
                foreach (GameObject container in _containers) {
                    container.transform.DOMoveX(container.transform.position.x - Screen.width, animTime);
                }
                break;
            case "left":
                foreach (GameObject container in _containers) {
                    container.transform.DOMoveX(container.transform.position.x + Screen.width, animTime);
                }
                break;
        }

        EnableGameObject();
    }

    private void EnableGameObject()
    {
        /* Enable or disable gameobjects according to the current container shown */

        //Display Terrain 
        if ((_currentContainer >= 3 && _currentContainer <= 6) && !terrainPreview.activeSelf)//terrain container & terrainPreview (GameObject) is not already active
        {
            terrainPreview.SetActive(true);
            camera.GetComponent<CamTerrainPreview>().enabled = true;
        }
        else if ((_currentContainer <= 2) && terrain.activeSelf)
        {
            terrainPreview.SetActive(false);
            camera.GetComponent<CamTerrainPreview>().enabled = false;
        }

        //Display bunnyPreview
        if (_currentContainer == 4 && !bunnyPreview.activeSelf)
        {
            SetAnimalPreviewPosition(bunnyPreview, .25f);
            bunnyPreview.SetActive(true);
        }
        else
            bunnyPreview.SetActive(false);

        //Display foxPreview
        if (_currentContainer == 5 && !foxPreview.activeSelf)
        {
            SetAnimalPreviewPosition(foxPreview, .1f);
            foxPreview.SetActive(true);
        }
        else
            foxPreview.SetActive(false);

        //Diplay main menu background
        if (_currentContainer == 0)
        {
            camera.transform.position = _camInitPos;
            camera.transform.rotation = _camInitRot;
            mainMenuBackground.SetActive(true);
        }
        else
            mainMenuBackground.SetActive(false);
    }

    private void SetAnimalPreviewPosition(GameObject animal, float yOffset)
    {
        float xPos = terrainPreview.GetComponent<MeshCreator>().xSize / 6 / 2;
        float zPos = terrainPreview.GetComponent<MeshCreator>().zSize / 6 / 2;
        float yPos = TerrainDatas.heightMap[TerrainDatas.zSize / 2, TerrainDatas.xSize / 2] * TerrainDatas.heightMultiplier + yOffset;

        animal.transform.position = new Vector3(xPos, yPos, zPos);
    }

    public void Quit() => Application.Quit();

    public void OpenULR() => Application.OpenURL("https://b-bischoff.github.io/web/");

    public void ToggleFullscreen()
    {
        // Toggle fullscreen mode
        Screen.fullScreen = !Screen.fullScreen;

        // Windowed : Screen.fullscreen = true
        // Fullscreen : Screen.fullscreen = false
        fullscreenToggle.isOn = !Screen.fullScreen;
        Screen.SetResolution(1920, 1080, !Screen.fullScreen);
    }
}
