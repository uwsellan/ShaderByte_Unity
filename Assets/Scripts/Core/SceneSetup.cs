using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    void Start()
    {
        // Find all components in the scene that require references to/from persistent objects
        Door[] doors = FindObjectsByType<Door>(FindObjectsSortMode.None);
        DialogueTrigger[] triggers = FindObjectsByType<DialogueTrigger>(FindObjectsSortMode.None);
        NPC[] npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);
        Health[] enemies = FindObjectsByType<Health>(FindObjectsSortMode.None);

        GameObject resettable = GameObject.Find("Resettable");

        // Assign the persistent PlayerRespawn to all doors
        foreach (Door door in doors)
        {
            door.Initialise(PlayerRespawn.Instance, SceneLoader.Instance);
        }

        // Assign the persistent DialogueManager to all NPCs
        foreach (DialogueTrigger trigger in triggers)
        {
            trigger.Initialise(DialogueManager.Instance);
        }

        // Assign the persistent UIManager to all NPCs
        foreach (NPC npc in npcs)
        {
            npc.Initialise(UIManager.instance);
        }

        // Assign the persistent player's money behaviour to all enemies (to get their drops)
        foreach(Health health in enemies)
        {
            health.Initialise(Money.instance);
        }

        if(resettable != null)
        {
            PlayerRespawn.Instance.setResettable(resettable);
        }
    }
}

