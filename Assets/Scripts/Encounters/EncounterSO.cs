using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSO : ScriptableObject
{
    [Range(1,3)][SerializeField] public int level;
    [Space]
    [SerializeField] public List<EnemySO> enemies;
    //[SerializeField] public List<InanimateEntity> inanimateEntities;
}
