using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Agrispace.Quests
{
    [CustomEditor(typeof(Quest))]
    public class QuestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("QuestName"), new GUIContent("Name"));
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Reward"), new GUIContent("Reward"));

            EditorGUILayout.LabelField("Objectives");

            SerializedProperty objectiveList = serializedObject.FindProperty("Objectives");

            EditorGUI.indentLevel += 1;

            for (int i = 0; i < objectiveList.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PropertyField(objectiveList.GetArrayElementAtIndex(i), includeChildren: true);

                if (GUILayout.Button("Up", EditorStyles.miniButtonLeft, GUILayout.Width(25)))
                {
                    objectiveList.MoveArrayElement(i, i - 1);
                }

                if (GUILayout.Button("Down", EditorStyles.miniButtonMid, GUILayout.Width(40)))
                {
                    objectiveList.MoveArrayElement(i, i + 1);
                }

                if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(25)))
                {
                    objectiveList.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel -= 1;

            if (GUILayout.Button("Add Objective"))
            {
                objectiveList.arraySize += 1;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}
#endif