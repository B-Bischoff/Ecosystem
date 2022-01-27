using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamTerrainPreview : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _target;
    
    [SerializeField] private float zoomSpeed = 6;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float _offset = -15;

    [SerializeField] private Vector3 _previousPosition, _targetPosition;

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
                else
                    _offset = -1;
            }

            //Rotate w/ left click
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

            //Translate w/ right click
            if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))//Right click & not left click
            {
                _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
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
        }
    }

    public void FindTargetPos()
    {
        _targetPosition.x = TerrainDatas.xSize / 6 / 2; //6 -> personnal scale : see "MeshCreator.cs"
        _targetPosition.z = TerrainDatas.zSize / 6 / 2;
        _targetPosition.y = _target.transform.position.y;
    }

    public void ForceCamReposition()
    {
        _cam.transform.position = _targetPosition;
        _cam.transform.Translate(new Vector3(0, 0, _offset));
    }

    private bool IsPointerOverUIElements()
    {
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
