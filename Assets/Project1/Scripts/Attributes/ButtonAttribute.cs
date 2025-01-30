using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method)]
public class ButtonAttribute : Attribute
{
    public readonly string ButtonLabel;

    /// <summary>
    /// Label parametresi boş bırakılırsa, method ismini buton üstünde gösterir.
    /// </summary>
    /// <param name="buttonLabel">Buton üzerine yazılacak metin.</param>
    public ButtonAttribute(string buttonLabel = null)
    {
        ButtonLabel = buttonLabel;
    }
}