using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUISelector : MonoBehaviour
{
    public MovementController MovementController;
    public SelectableController Selectable;

    private Toggle _toggle;
    private bool _break;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        if (!_toggle) Debug.LogError("Toggle can't be found");


        if (!Selectable) Debug.LogError("Selectable was not assigned");
    }

    private void Start()
    {
        Selectable.OnSelectionChanged.AddListener(OnSelectableStateChanged);
        _toggle.onValueChanged.AddListener(OnToggleStateChanged);
    }

    private void OnToggleStateChanged(bool state)
    {
        if (_break)
        {
            _break = false;
            return;
        }

        _break = true;
        if (state)
        {
            MovementController.SetSelectedObject(Selectable);
        }
        else
        {
            MovementController.SetSelectedObject(null);
        }
    }

    private void OnSelectableStateChanged()
    {
        if (_break)
        {
            _break = false;
            return;
        }

        _break = true;
        _toggle.isOn = Selectable.IsSelected;
    }
}
