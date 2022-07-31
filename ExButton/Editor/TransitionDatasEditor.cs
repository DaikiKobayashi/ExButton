using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ExTools
{
    [CustomPropertyDrawer(typeof(ColorTransition))]
    public class ColorTransitionEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var normal = property.FindPropertyRelative("normalColor");
            var hover = property.FindPropertyRelative("hoverColor");
            var pressed = property.FindPropertyRelative("pressedColor");

            position.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, normal);

            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, hover);

            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, pressed);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }
    }

    [CustomPropertyDrawer(typeof(AnimationTransition))]
    public class AnimationTransitionEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var normal = property.FindPropertyRelative("normalName");
            var hover = property.FindPropertyRelative("hoverName");
            var pressed = property.FindPropertyRelative("pressedName");

            position.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, normal);

            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, hover);

            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, pressed);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }
    }
}