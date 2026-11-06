using UnityEngine;

[System.Serializable]
public struct DialogueChoice
{
    public string choiceText;
    public DialogueData nextDialogue;
    public string sceneToLoad;
    public bool requiresEquipmentCheck;
    public DialogueData failedEquipmentDialogue;

    [Header("Reward / Action")]
    public bool grantsEquipment;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string speakerName;
    [TextArea(3, 10)]
    public string[] sentences;

    [Header("Voiceover Audio Clips")]
    public AudioClip[] voiceClips; // Match each clip to the corresponding sentence index

    [Header("Choices (Optional)")]
    public DialogueChoice[] choices;
}