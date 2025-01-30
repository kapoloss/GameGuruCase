using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)] // Tüm MonoBehaviour türevlerine uygulanacak
[CanEditMultipleObjects]
public class ButtonAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Varsayılan Inspector çizimini yap
        base.OnInspectorGUI();
        
        // Hedef objeye MonoBehaviour olarak ulaş
        MonoBehaviour mono = target as MonoBehaviour;
        if (mono == null) return;
        
        // Bu MonoBehaviour'ün tipinde tanımlı tüm metodları al
        MethodInfo[] methods = mono.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (MethodInfo method in methods)
        {
            // Metodun üzerinde ButtonAttribute var mı?
            var attribute = (ButtonAttribute)method.GetCustomAttributes(typeof(ButtonAttribute), true).FirstOrDefault();
            if (attribute == null)
                continue;

            // Buton yazısı olarak ya attribute'taki label, ya da metodun adını kullan
            string buttonLabel = string.IsNullOrEmpty(attribute.ButtonLabel) 
                ? method.Name 
                : attribute.ButtonLabel;

            // Butonu çiz
            if (GUILayout.Button(buttonLabel))
            {
                // Metodun parametresi yoksa çalıştır
                method.Invoke(mono, null);
            }
        }
    }
}