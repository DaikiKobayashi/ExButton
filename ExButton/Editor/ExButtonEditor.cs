using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ExTools
{
    [CustomEditor(typeof(ExButton))]
    public class ExButtonEditor : Editor
    {
        class Propertys
        {
            public Propertys(SerializedObject serializedObject)
            {
                image = serializedObject.FindProperty("_image");
                transitionType = serializedObject.FindProperty("_transitionType");
                colorTransition = serializedObject.FindProperty("_colorTransition");
                animationTransition = serializedObject.FindProperty("_animationTransition");
            }

            public SerializedProperty image { get; set; }

            public SerializedProperty transitionType { get; set; }

            public SerializedProperty colorTransition { get; set; }
            public SerializedProperty animationTransition { get; set; }
        }

        Propertys propertys = null;

        public override void OnInspectorGUI()
        {
            if (propertys == null)
                propertys = new Propertys(serializedObject);
            var style = GUI.skin.box;

            serializedObject.Update();

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
                        EditorGUILayout.PropertyField(propertys.animationTransition);
                        break;
                    default:
                        break;
                }
            
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}