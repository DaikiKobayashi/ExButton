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
                animator = serializedObject.FindProperty("_animator");
                transitionType = serializedObject.FindProperty("_transitionType");
                colorTransition = serializedObject.FindProperty("_colorTransition");
                animationTransition = serializedObject.FindProperty("_animationTransition");
                spriteSwapTransition = serializedObject.FindProperty("_spriteSwapTransition");
            }

            public SerializedProperty image { get; set; }
            
            public SerializedProperty animator { get; set; }

            public SerializedProperty transitionType { get; set; }

            public SerializedProperty colorTransition { get; set; }

            public SerializedProperty animationTransition { get; set; }

            public SerializedProperty spriteSwapTransition { get; set; }
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
                        EditorGUILayout.PropertyField(propertys.animator);
                        if(propertys.animator.objectReferenceValue == null)
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

            serializedObject.ApplyModifiedProperties();
        }
    }
}