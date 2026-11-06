using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI dialogueBodyText;

    [Header("Audio")]
    [SerializeField] private AudioSource voiceAudioSource; // AudioSource to play voice lines

    [Header("Choice UI References")]
    [SerializeField] private GameObject choiceContainer;
    [SerializeField] private GameObject choiceButtonPrefab;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.03f;

    private Queue<string> sentences = new Queue<string>();
    private Queue<AudioClip> voiceClips = new Queue<AudioClip>(); // Queue to hold voice tracks
    private Coroutine typingCoroutine;
    private bool isTyping;
    private string currentSentence;
    private DialogueData currentData;

    public bool IsDialogueActive { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);
        if (choiceContainer != null) choiceContainer.SetActive(false);
    }

    public void StartDialogue(DialogueData data)
    {
        IsDialogueActive = true;
        currentData = data;
        dialoguePanel.SetActive(true);
        if (choiceContainer != null) choiceContainer.SetActive(false);

        speakerNameText.text = data.speakerName;

        sentences.Clear();
        foreach (string sentence in data.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Queue up the matching audio clips
        voiceClips.Clear();
        if (data.voiceClips != null)
        {
            foreach (AudioClip clip in data.voiceClips)
            {
                voiceClips.Enqueue(clip);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueBodyText.text = currentSentence;
            isTyping = false;

            if (sentences.Count == 0 && currentData.choices != null && currentData.choices.Length > 0)
            {
                ShowChoices();
            }
            return;
        }

        if (sentences.Count == 0)
        {
            if (currentData.choices != null && currentData.choices.Length > 0)
            {
                ShowChoices();
            }
            else
            {
                EndDialogue();
            }
            return;
        }

        // Stop previous audio before playing new line
        if (voiceAudioSource != null && voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }

        // Play voice clip if available
        if (voiceClips.Count > 0)
        {
            AudioClip currentClip = voiceClips.Dequeue();
            if (currentClip != null && voiceAudioSource != null)
            {
                voiceAudioSource.PlayOneShot(currentClip);
            }
        }

        currentSentence = sentences.Dequeue();
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueBodyText.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueBodyText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (sentences.Count == 0 && currentData.choices != null && currentData.choices.Length > 0)
        {
            ShowChoices();
        }
    }

    private void ShowChoices()
    {
        choiceContainer.SetActive(true);
        SetCursorState(false);

        foreach (Transform child in choiceContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (DialogueChoice choice in currentData.choices)
        {
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContainer.transform);
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = choice.choiceText;

            Button button = buttonObj.GetComponent<Button>();
            DialogueChoice selectedChoice = choice;
            button.onClick.AddListener(() => OnChoiceSelected(selectedChoice));
        }
    }

    private void OnChoiceSelected(DialogueChoice choice)
    {
        choiceContainer.SetActive(false);

        if (choice.grantsEquipment && PlayerData.Instance != null)
        {
            PlayerData.Instance.GrantEquipment();
        }

        if (choice.requiresEquipmentCheck)
        {
            bool hasEquipment = PlayerData.Instance != null && PlayerData.Instance.hasEquipment;

            if (!hasEquipment)
            {
                if (choice.failedEquipmentDialogue != null)
                {
                    StartDialogue(choice.failedEquipmentDialogue);
                }
                else
                {
                    EndDialogue();
                }
                return;
            }
        }

        if (!string.IsNullOrEmpty(choice.sceneToLoad))
        {
            EndDialogue();
            SceneManager.LoadScene(choice.sceneToLoad);
            return;
        }

        if (choice.nextDialogue != null)
        {
            StartDialogue(choice.nextDialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        IsDialogueActive = false;
        dialoguePanel.SetActive(false);
        if (choiceContainer != null) choiceContainer.SetActive(false);

        // Stop voice audio when dialogue ends
        if (voiceAudioSource != null && voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }

        SetCursorState(true);
    }

    private void SetCursorState(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}