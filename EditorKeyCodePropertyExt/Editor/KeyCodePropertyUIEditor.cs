using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(KeyCodeUI))]
    public class KeyCodeUIDrawer : PropertyDrawer
    {
        #region Properties

        const float clearButtonWidth = 50;

        #endregion

        #region GUI

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = EditorGUI.PrefixLabel(position, label);

            int id = GUIUtility.GetControlID((int)position.x, FocusType.Keyboard, position);

            string propertyLabel = ((KeyCode)property.intValue).ToString();

            if (GUIUtility.keyboardControl == id)
            {
                propertyLabel += " (press any key)";
                GUI.color = new Color(0.4f, 0.65f, 1f, 1f);
                position.width -= clearButtonWidth;
            }

            if (GUI.Button(position, propertyLabel, EditorStyles.popup))
            {
                GUIUtility.keyboardControl = id;
                Event.current.Use();
            }

            GUI.color = Color.white;

            position.x += position.width;
            position.width = clearButtonWidth;

            if (GUIUtility.keyboardControl == id && GUI.Button(position, "None"))
            {
                property.enumValueIndex = (int)KeyCode.None;
                Event.current.Use();
                GUIUtility.keyboardControl = -1;

            }

            if (GUIUtility.keyboardControl == id && Event.current.type == EventType.KeyUp)
            {
                if (Event.current.keyCode != KeyCode.None) property.enumValueIndex = KeyCodeToEnum(property, Event.current.keyCode);
                Event.current.Use();
                GUIUtility.keyboardControl = -1;
            }
            else if (GUIUtility.keyboardControl == id && Event.current.isKey)
                Event.current.Use();

            GUI.color = Color.white;
        }

        public int KeyCodeToEnum(SerializedProperty keyCodeProperty, KeyCode keyCode)
        {
            string[] keyCodeNames = keyCodeProperty.enumNames;
            for (int i = 0; i < keyCodeNames.Length; i++)
                if (keyCodeNames[i].CompareTo(keyCode.ToString()) == 0) return i;

            return 0;
        }

        #endregion
    }