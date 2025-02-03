using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private BoxCollider _boxCollider;
    public MeshRenderer meshRenderer;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ResizeCollider()
    {
        _boxCollider.size = _meshFilter.sharedMesh.bounds.size;
    }
}
