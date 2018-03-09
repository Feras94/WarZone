using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public Transform WaypointObject;

    private SelectableController _currentSelection;

    private void Awake()
    {
        if (!WaypointObject) Debug.LogError("Waypoint not assigned!", this);
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var hit = CheckHit();
        if (hit.HasValue)
        {
            var selectable = hit.Value.collider.GetComponent<SelectableController>();
            if (selectable)
            {
                SetSelectedObject(selectable);
            }
            else if (_currentSelection)
            {
                _currentSelection.Agent.SetDestination(hit.Value.point);
            }
            else
            {
                SetSelectedObject(null);
            }
        }
        else
        {
            SetSelectedObject(null);
        }
    }

    public void SetSelectedObject(SelectableController selectable)
    {
        if (_currentSelection) _currentSelection.Deselect();
        _currentSelection = selectable;
        if (_currentSelection) _currentSelection.Select();
    }

    private void LateUpdate()
    {
        UpdateWaypoint();
    }

    private void UpdateWaypoint()
    {
        if (_currentSelection && !_currentSelection.HasArrived())
        {
            WaypointObject.position = new Vector3(_currentSelection.Agent.destination.x, WaypointObject.position.y, _currentSelection.Agent.destination.z);
            WaypointObject.gameObject.SetActive(true);
        }
        else
        {
            WaypointObject.gameObject.SetActive(false);
        }
    }

    private RaycastHit? CheckHit()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit;
        }

        return null;
    }

    public void ClearSelection()
    {
        this.SetSelectedObject(null);
    }
}
