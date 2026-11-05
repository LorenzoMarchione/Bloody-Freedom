using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private GameObject interactionPromptUI;

    private bool isPlayerInRange;

    private void Update()
    {
        if (!isPlayerInRange) return;

        // Check if the 'E' key was pressed using the New Input System
        bool interactPressed = Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame;

        if (interactPressed)
        {
            if (!DialogueManager.Instance.IsDialogueActive)
            {
                if (interactionPromptUI != null) interactionPromptUI.SetActive(false);
                DialogueManager.Instance.StartDialogue(dialogueData);
            }
            else
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null)
        {
            isPlayerInRange = true;
            if (interactionPromptUI != null && !DialogueManager.Instance.IsDialogueActive)
            {
                interactionPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null)
        {
            isPlayerInRange = false;
            if (interactionPromptUI != null) interactionPromptUI.SetActive(false);

            if (DialogueManager.Instance.IsDialogueActive)
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
}