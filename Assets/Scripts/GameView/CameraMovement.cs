using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _animalTarget;
    [SerializeField] private float zoomSpeed = 6;
    [SerializeField] private float movementSpeed;

    private Vector3 _previousPosition;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _animalTargetOffset;
    [SerializeField] private float _offset = -15;
    [SerializeField] private float _smoothSpeed = .15f;

    [SerializeField] private GameObject displayCarac;

    public void Awake() => _cam = GetComponent<Camera>();

    public void Start()
    {
        FindTargetPos();
    }

    public void Update()
    {
        if (!IsPointerOverUIElements())
        {
            //Zoom
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                if (_offset < 0)
                {
                    _offset += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
                    _cam.transform.position = _targetPosition;
                    _cam.transform.Translate(new Vector3(0, 0, _offset));
                    _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
                }
                else //Clamping the offset to -1 => Can't enter "in" the terrain with the camera
                    _offset = -1;
            }

            //Rotate with left click
            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))//Left click & not right click
            {
                _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                Vector3 direction = _previousPosition - _cam.ScreenToViewportPoint(Input.mousePosition);

                _cam.transform.position = _targetPosition;

                _cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                _cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, relativeTo: Space.World);
                _cam.transform.Translate(new Vector3(0, 0, _offset));

                _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            }

            //Translate with right click
            if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))//Right click & not left click
            {
                _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
                if (_animalTarget != null)
                {
                    _animalTarget.GetComponent<Animals>().SetOutline(false);
                    _animalTarget.GetComponent<UI_WorldSpace>().SetShowCanvas(false);
                }
                _animalTarget = null;
            }
            if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
            {
                Vector3 direction = _previousPosition - _cam.ScreenToViewportPoint(Input.mousePosition);

                _cam.transform.Translate(direction * movementSpeed);

                RaycastHit hit;
                var ray = _cam.transform.forward;

                if (Physics.Raycast(_cam.transform.position, ray, out hit))
                {
                    Debug.DrawRay(_cam.transform.position, _cam.transform.forward * 100f, Color.yellow);
                    _targetPosition = hit.point;
                }

                _offset = -Vector3.Distance(_targetPosition, _cam.transform.position);
                _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            }

            //Getting animal to focus
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Animal")
                    {
                        if (_animalTarget != null)
                        {
                            _animalTarget.GetComponent<Animals>().SetOutline(false);
                            _animalTarget.GetComponent<UI_WorldSpace>().SetShowCanvas(false);
                        }

                        _animalTarget = hit.transform.gameObject;
                        _animalTarget.GetComponent<Animals>().SetOutline(true);
                        _animalTarget.GetComponent<UI_WorldSpace>().SetShowCanvas(true);
                        CalculateAnimalOffset();

                        //Updating DisplayCarac texts (gameview ui)
                        displayCarac.GetComponent<DisplayCarac>().SetAnimalTarget(_animalTarget);
                        displayCarac.GetComponent<DisplayCarac>().DisplayAnimalCarac();
                    }
                }
            }
        }
    }

    public void FixedUpdate()
    {
        //Focus animal
        if (_animalTarget != null)
        {
            _targetPosition = _animalTarget.transform.position;

            _cam.transform.position = _targetPosition;
            _cam.transform.Translate(new Vector3(0, 0, _offset));

            transform.LookAt(_animalTarget.transform);
        }
    }
    
    private void CalculateAnimalOffset()
    {
        _animalTargetOffset = _cam.transform.position - _animalTarget.transform.position;
    }

    private void FindTargetPos() //Method used to focus the terrain
    {
        _targetPosition.x = TerrainDatas.xSize / TerrainDatas.mapScale / 2; //2 allows to target the middle of the object
        _targetPosition.z = TerrainDatas.zSize / TerrainDatas.mapScale / 2;
        _targetPosition.y = _target.position.y;
    }

    private bool IsPointerOverUIElements()
    {
        //Allows to know if the pointer is over a gameObject that has a RectTransform component
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
            if (result.gameObject.GetComponent<RectTransform>() != null)
                return true;

        return false;
    }
}
