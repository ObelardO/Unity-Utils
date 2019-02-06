// © 2018 ObelardO aka Vladislav Trubitsyn
// obelardos@gmail.com
// https://obeldev.ru
// MIT License

using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
public static class EditorHerarchyStateModifier
{
    #region Init

    static EditorHerarchyStateModifier()
    {
        EditorApplication.hierarchyWindowItemOnGUI =
            (EditorApplication.HierarchyWindowItemCallback)
                Delegate.Combine(EditorApplication.hierarchyWindowItemOnGUI,
                    (EditorApplication.HierarchyWindowItemCallback)DrawHierarchyExt);
    }

    #endregion

    #region GUI

    private static void DrawHierarchyExt(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);
        if (gameObject == null) return;
        Rect toggleRect = new Rect(selectionRect.x + selectionRect.width - 18f, selectionRect.y, 16f, 16f);
        bool objectState = EditorGUI.Toggle(toggleRect, gameObject.activeSelf);
        if (objectState != gameObject.activeSelf) gameObject.SetActive(objectState);
    }

    #endregion

}