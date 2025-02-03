using System;
using UnityEngine;

public class CutPlatformResult : EventArgs
{
    public bool IsSuccessful { get; private set; }
    public GameObject FallenPart { get; private set; }
    public Platform UpdatedPlatform { get; private set; }
    public float AccuracyRate { get; private set; }

    public CutPlatformResult(bool isSuccessful, GameObject fallenPart, Platform updatedPlatform, float accuracyRate)
    {
        IsSuccessful = isSuccessful;
        FallenPart = fallenPart;
        UpdatedPlatform = updatedPlatform;
        AccuracyRate = accuracyRate;
    }
}