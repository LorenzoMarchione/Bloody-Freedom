using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class OnClickUI : MonoBehaviour, IPointerEnterHandler
{
    [Header("Audio Settings")]
    [Tooltip("Sound that plays when the mouse hovers over the button.")]
    [SerializeField] private AudioClip hoverSound;

    [Tooltip("Sound that plays when the button is clicked.")]
    [SerializeField] private AudioClip clickSound;

    private Button uiButton;
    private AudioSource audioSource;

    void Awake()
    {
        // Get references to the components
        uiButton = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        // Configure AudioSource settings for UI sounds
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 0 makes it 2D sound (ideal for UI)

        // Automatically register the click listener
        if (uiButton != null)
        {
            uiButton.onClick.AddListener(PlayClickSound);
        }
    }

    // This triggers automatically when the mouse pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}