using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DiceIDSelectionAttribute))]
public class DiceIDSelectionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        DiceDatabase database = DiceDatabase.Instance;

        List<string> options = new List<string> { };
        List<string> ids = new List<string> { };
        foreach(Die die in database.Dice)
        {
            options.Add(die.Type);
            ids.Add(die.ID);
        }

        int index = ids.IndexOf(property.stringValue);
        if (index == -1) index = 0;

        index = EditorGUI.Popup(position, label.text, index, options.ToArray());

        property.stringValue = ids[index];
        EditorGUI.EndProperty();
    }
}
