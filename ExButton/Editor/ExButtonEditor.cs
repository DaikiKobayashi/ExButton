using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.UI;
using System.Reflection;
using System;

namespace ExTools
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ExButton))]
    public class ExButtonEditor : SelectableEditor
    {
        class Propertys
        {
            public Propertys(SerializedObject serializedObject)
            {
                image = serializedObject.FindProperty("_image");
                animator = serializedObject.FindProperty("_animator");
                transitionType = serializedObject.FindProperty("_transitionType");
                colorTransition = serializedObject.FindProperty("_colorTransition");
                animationTransition = serializedObject.FindProperty("_animationTransition");
                spriteSwapTransition = serializedObject.FindProperty("_spriteSwapTransition");

                navigation = serializedObject.FindProperty("m_Navigation");

            }

            public SerializedProperty image { get; set; }

            public SerializedProperty animator { get; set; }

            public SerializedProperty transitionType { get; set; }

            public SerializedProperty colorTransition { get; set; }

            public SerializedProperty animationTransition { get; set; }

            public SerializedProperty spriteSwapTransition { get; set; }

            public SerializedProperty navigation { get; set; }
        }

        Propertys propertys = null;

        GUIContent m_VisualizeNavigation = new GUIContent("Visualize", "Show navigation flows between selectable UI elements.");

        public override void OnInspectorGUI()
        {
            var style = GUI.skin.box;

            serializedObject.Update();

            propertys = new Propertys(serializedObject);

            using (var v = new EditorGUILayout.VerticalScope(style))
            {
                EditorGUILayout.PropertyField(propertys.image);
            }

            EditorGUILayout.Space();

            using (var v = new EditorGUILayout.VerticalScope(style))
            {
                EditorGUILayout.PropertyField(propertys.transitionType);

                EditorGUI.indentLevel++;

                switch (propertys.transitionType.enumValueIndex)
                {
                    case 0:
                        break;
                    case 1:
                        EditorGUILayout.PropertyField(propertys.colorTransition);
                        break;
                    case 2:
                        EditorGUILayout.PropertyField(propertys.animator);
                        if (propertys.animator.objectReferenceValue == null)
                        {
                            style.normal.textColor = Color.red;
                            EditorGUILayout.LabelField("Please set 'Animator Component'.", style);
                        }
                        EditorGUILayout.PropertyField(propertys.animationTransition);
                        break;
                    case 3:
                        EditorGUILayout.PropertyField(propertys.spriteSwapTransition);
                        break;
                    default:
                        break;
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            using (var v = new EditorGUILayout.VerticalScope(style))
            {
                EditorGUILayout.PropertyField(propertys.navigation);
            }

            var field = typeof(ExButtonEditor).BaseType.GetField("s_ShowNavigation", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(
                this, GUILayout.Toggle((bool)field.GetValue(this), m_VisualizeNavigation, EditorStyles.miniButton));

            serializedObject.ApplyModifiedProperties();
        }
    }
}