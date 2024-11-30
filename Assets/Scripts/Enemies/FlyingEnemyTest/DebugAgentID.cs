using UnityEngine;
using UnityEngine.AI;

public class DebugAgentTypes : MonoBehaviour
{
    // This will run when the script starts to debug all agent types
    void Start()
    {
        DebugAgentTypesList();
    }

    public static void DebugAgentTypesList()
    {
        // Get the total number of NavMesh agent settings
        int count = NavMesh.GetSettingsCount();
        
        // Loop through each setting and print its name and ID
        for (int i = 0; i < count; i++)
        {
            // Get the agent type ID for the setting at index i
            int agentTypeID = NavMesh.GetSettingsByIndex(i).agentTypeID;
            
            // Get the name corresponding to the agent type ID
            string agentName = NavMesh.GetSettingsNameFromID(agentTypeID);
            
            // Print the agent type ID and its corresponding name to the console
            Debug.Log($"Agent Type ID: {agentTypeID}, Name: {agentName}");
        }
        
    }
}