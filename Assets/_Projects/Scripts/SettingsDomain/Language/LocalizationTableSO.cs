using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LocalizedString
{
    public LocalizationKey Key;

    [TextArea(1, 3)] public string English;
    [TextArea(1, 3)] public string Portuguese;
}

[CreateAssetMenu(fileName = "NewLocalizationTable", menuName = "Data/Localization Table")]
public class LocalizationTableSO : ScriptableObject
{
    [Header("Translation Database")]
    [SerializeField] private List<LocalizedString> _entries = new List<LocalizedString>();

    public string GetText(LocalizationKey key, GameLanguage currentLanguage)
    {
        if (key == LocalizationKey.None) return "";

        foreach (var entry in _entries)
        {
            if (entry.Key == key)
            {
                return currentLanguage == GameLanguage.Portuguese ? entry.Portuguese : entry.English;
            }
        }

        Debug.LogWarning($"[Localization] Translation Key not found in SO: {key}", this);
        return $"[{key}]";
    }
}
