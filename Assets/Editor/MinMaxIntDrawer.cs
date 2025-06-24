using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMaxInt))]
public class MinMaxIntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Begin property
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Calculate rects
        float halfWidth = position.width / 2;
        Rect minRect = new Rect(position.x, position.y, halfWidth - 5, position.height);
        Rect maxRect = new Rect(position.x + halfWidth + 5, position.y, halfWidth - 5, position.height);

        // Find the fields
        var minProp = property.FindPropertyRelative("Min");
        var maxProp = property.FindPropertyRelative("Max");

        // Labels
        EditorGUIUtility.labelWidth = 30;
        EditorGUI.PropertyField(minRect, minProp, new GUIContent("Min"));
        EditorGUI.PropertyField(maxRect, maxProp, new GUIContent("Max"));

        EditorGUI.EndProperty();
    }
}
