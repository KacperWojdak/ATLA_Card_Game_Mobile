using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Deck))]
public class DeckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Deck deck = (Deck)target;

        // Hero
        SerializedProperty heroProp = serializedObject.FindProperty("heroCard");
        EditorGUILayout.PropertyField(heroProp, new GUIContent("Hero Card"));

        // Attack
        SerializedProperty attackCardsProp = serializedObject.FindProperty("attackCards");
        ShowCards(attackCardsProp, "Attack Cards", 10);

        // Defense
        SerializedProperty defenseCardsProp = serializedObject.FindProperty("defenseCards");
        ShowCards(defenseCardsProp, "Defense Cards", 10);

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowCards(SerializedProperty listProperty, string label, int maxCount)
    {
        EditorGUILayout.PropertyField(listProperty, new GUIContent(label), true);
        if (listProperty.isExpanded)
        {
            EditorGUI.indentLevel++;

            int currentSize = listProperty.arraySize;
            int newSize = Mathf.Clamp(EditorGUILayout.IntField("Size", currentSize), 0, maxCount);
            if (newSize != currentSize)
            {
                listProperty.arraySize = newSize;
            }

            for (int i = 0; i < listProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(elementProperty, new GUIContent("Card " + (i + 1)));
            }

            EditorGUI.indentLevel--;
        }
    }
}