using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Card", menuName = "Enemy/Enemy Card")]
public class EnemySO : ScriptableObject
{
    [SerializeField] public string enemyName;
    [Range(0, 50)][SerializeField] public int health;
    [Range(0, 20)][SerializeField] public int threatLevel;
    [Range(-1, 15)][SerializeField] public int range;
    [SerializeField] public Defense defense;
    //[SerializeField] public Sprite illustration;
    [SerializeField] public Behaviour behaviour;
}
