using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceDatabase", menuName = "Dice/Dice Database")]
public class DiceDatabase : ScriptableObject
{
    [SerializeField] private List<Die> dice = new List<Die>();

    public IReadOnlyList<Die> Dice => dice;

    private static DiceDatabase instance;

    public static DiceDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                string typeName = nameof(DiceDatabase);

                instance = Resources.Load<DiceDatabase>(typeName);
                if(instance == null)
                {
                    instance = CreateInstance<DiceDatabase>();
                    string resourcesFolder = "Assets/Resources";
                    if (!AssetDatabase.IsValidFolder(resourcesFolder))
                    {
                        AssetDatabase.CreateFolder("Assets", "Resources");
                    }

                    string assetPath = $"{resourcesFolder}/{typeName}.asset";
                    AssetDatabase.CreateAsset(instance, assetPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
            return instance;
        }
    }

    public Die GetDie(string id)
    {
        return dice.FirstOrDefault(dice => dice.ID == id);
    }

    private void OnValidate()
    {
        bool changed = false;

        HashSet<string> idSet = new();
        foreach(Die die in dice)
        {
            if(string.IsNullOrEmpty(die.ID) || idSet.Contains(die.ID))
            {
                die.GenerateNewID();
                changed = true;
            }

            idSet.Add(die.ID);
        }

        if (changed)
        {
            EditorUtility.SetDirty(this);
        }
    }
}
