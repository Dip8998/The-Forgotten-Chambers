using UnityEngine;

[CreateAssetMenu(fileName = "InstructionData", menuName = "ScriptableObjects/InstructionData")]
public class InstructionData : ScriptableObject
{
    public string instructionKey; 
    [TextArea] public string[] instructionLines;

    public string GetRandomLine()
    {
        if (instructionLines.Length == 0) return "No instructions set.";
        return instructionLines[Random.Range(0, instructionLines.Length)];
    }
}

