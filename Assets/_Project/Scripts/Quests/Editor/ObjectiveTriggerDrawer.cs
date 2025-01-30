#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Agrispace.Quests
{
    [CustomPropertyDrawer(typeof(ObjectiveTrigger))]
    public class ObjectiveTriggerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty questProperty = property.FindPropertyRelative("QuestTarget");
            SerializedProperty statusProperty = property.FindPropertyRelative("ObjectiveStatusToApply");
            SerializedProperty objectiveNumberProperty = property.FindPropertyRelative("ObjectiveNumber");

            int lineSpacing = 2;

            Rect firstLinePosition = position;
            firstLinePosition.height = base.GetPropertyHeight(questProperty, label);

            Rect secondLinePosition = position;
            secondLinePosition.y = firstLinePosition.y + firstLinePosition.height + lineSpacing;
            secondLinePosition.height = base.GetPropertyHeight(statusProperty, label);

            Rect thirdLinePosition = position;
            thirdLinePosition.y = secondLinePosition.y + secondLinePosition.height + lineSpacing;
            thirdLinePosition.height = base.GetPropertyHeight(objectiveNumberProperty, label);

            EditorGUI.PropertyField(firstLinePosition, questProperty, new GUIContent("Quest"));
            EditorGUI.PropertyField(secondLinePosition, statusProperty, new GUIContent("Status"));

            thirdLinePosition = EditorGUI.PrefixLabel(thirdLinePosition, new GUIContent("Objective"));

            Quest quest = questProperty.objectReferenceValue as Quest;

            if (quest != null && quest.Objectives.Count > 0)
            {
                string[] objectiveNames = quest.Objectives.Select(o => o.Name).ToArray();

                int selectedObjective = objectiveNumberProperty.intValue;

                if (selectedObjective >= quest.Objectives.Count)
                {
                    selectedObjective = 0;
                }

                int newSelectedObjective = EditorGUI.Popup(thirdLinePosition, selectedObjective, objectiveNames);

                if (newSelectedObjective != selectedObjective)
                {
                    objectiveNumberProperty.intValue = newSelectedObjective;
                }
                else
                {
                    using (new EditorGUI.DisabledGroupScope(true))
                    {
                        EditorGUI.Popup(thirdLinePosition, 0, new[] { "-" });
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int lineCount = 3;

            int lineSpacing = 2;

            float lineHeight = base.GetPropertyHeight(property, label);

            return (lineHeight * lineCount) + (lineSpacing * (lineCount - 1));
        }
    }
}
#endif