using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SelectableController : MonoBehaviour
{
    public Material SelectedMaterial;
    public NavMeshAgent Agent { get; private set; }
    public bool IsSelected { get; set; }

    private Material _originalMaterial;
    private MeshRenderer _meshRenderer;

    public UnityEvent OnSelectionChanged;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (!_meshRenderer) Debug.LogError("Can't find mesh renderer", this);

        Agent = GetComponent<NavMeshAgent>();
        if (!Agent) Debug.LogError("Can't find navmesh agent", this);

        _originalMaterial = _meshRenderer.material;
    }

    public void Select()
    {
        Debug.Log("Selected");
        _meshRenderer.material = SelectedMaterial;
        IsSelected = true;
        OnSelectionChanged.Invoke();
    }

    public void Deselect()
    {
        Debug.Log("De-Selected");
        _meshRenderer.material = _originalMaterial;
        IsSelected = false;
        OnSelectionChanged.Invoke();
    }

    public bool HasArrived()
    {
        var distance = Vector3.Distance(transform.position, Agent.destination);
        return distance < 0.75f;
    }
}
