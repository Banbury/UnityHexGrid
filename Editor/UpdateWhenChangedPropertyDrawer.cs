using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(UpdateWhenChangedAttribute))]
public class UpdateWhenChangedPropertyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(position, property, label, true);
        property.serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck()) {
            (property.serializedObject.targetObject as IInitialize).Init();
        }
    }
}
