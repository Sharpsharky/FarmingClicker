using UnityEngine;
using UnityEditor;

namespace InfiniteValue
{
    [CustomPropertyDrawer(typeof(DisplayOption))]
    public class DisplayOptionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(rect, label, property);
            property.intValue = (byte)(object)EditorGUI.EnumFlagsField(rect, label, (DisplayOption)property.intValue);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}