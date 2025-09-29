using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    private enum SelectionType
    {
        Node,
        Unit
    }

    public EncounterSO encounterData;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private List<Player> players = new List<Player>();
    [SerializeField] private Player aggro;

    private void Start()
    {
        SetupEncounter();
        StartCoroutine(StartEncounter());
    }

    // setup encounter data for nodes (sets up the nodes, spawns the enemies and inanimate events, etc.)
    private void SetupEncounter()
    {
        /*
        foreach (EnemySO enemy in encounterData.enemies)
        {
            // spawn enemies with the appropriate data at the appropriate points
        }
        */

        aggro = players[0]; // Make player choose aggro in future when/if there's multiple
        return;
    }

    // begins by allowing the player to choose his spawn locations, then begins combat
    private IEnumerator StartEncounter()
    {
        // Allow player to choose starting positing depending on door of entry (bottom by default)

        while (true)
        {
            // Begins combat with enemies doing their behaviour (can start with movement for now for simplicity)
            yield return EnemyActivation();
            // After enemies, the player is allowed to make their decisions (change with backup, move, card actions (1 per card unless otherwise stated))
            yield return PlayerActivation();
            // If a player has died, or all of the enemies are defeated, the encounter ends.
            
        }
    }

    // Activates all of the enemies, doing each behaviour in sequential order for all enemies at the same time
    private IEnumerator EnemyActivation()
    {
        yield return new WaitForFixedUpdate();
        // TODO: Change this to activate each enemy behaviour action sequentially all at the same time (all enemies do 0, all enemies do 1, etc.)
        //yield return enemies[0].EnemyData.behaviour.actions[0].Perform(enemies[0]);
        /*
        foreach(Action action in enemies[0].EnemyData.behaviour.actions)
        {
            yield return action?.Perform(enemies[0]);
        }
        */
        bool isFinished = false;
        int i = 0;
        while (!isFinished)
        {
            isFinished = true;
            foreach (Enemy enemy in enemies)
            {
                Behaviour behaviour = enemy.EnemyData.behaviour;
                if (behaviour.actions.ElementAtOrDefault(i % behaviour.actions.Count) != null && 
                    (i+1) <= (behaviour.repeats * behaviour.actions.Count)) // repeat implementation
                {
                    yield return behaviour.actions[i % behaviour.actions.Count]?.Perform(this, enemy);
                    isFinished = false;
                }
            }
            i++;
        }

        yield return null; // Wait for next frame
    }

    // Activates the player to choose their actions in whatever order they desire
    private IEnumerator PlayerActivation()
    {
        yield return new WaitForFixedUpdate();
        yield return players[0].moveAction.Perform(this, players[0]);
        yield return null; // Wait for next frame
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
    public List<Player> GetPlayers()
    {
        return players;
    }
    public Player GetAggro()
    {
        return aggro;
    }
}
