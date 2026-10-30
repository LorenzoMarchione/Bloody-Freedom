using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Required to check scene names

[RequireComponent(typeof(CanvasGroup))]
public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mainMixer;

    [Header("UI Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider voicesSlider;

    [Header("Settings Panel & Buttons")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsOpenButton;
    [SerializeField] private Button settingsCloseButton;

    [Header("Scene Settings")]
    [Tooltip("Type the exact name of your Main Menu scene here.")]
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Change this to your actual main menu scene name!

    // These match the exposed parameter names in your Audio Mixer
    private const string MusicParam = "MusicVolume";
    private const string SFXParam = "SFXVolume";
    private const string AmbientParam = "AmbientVolume";
    private const string VoicesParam = "VoicesVolume";

    private const float DefaultMusicDB = -7f;
    private const float DefaultSFXDB = 0f;
    private const float DefaultAmbientDB = -6f;
    private const float DefaultVoicesDB = 0f;

    void Start()
    {
        InitializeSliderVisuals(musicSlider, MusicParam);
        InitializeSliderVisuals(sfxSlider, SFXParam);
        InitializeSliderVisuals(ambientSlider, AmbientParam);
        InitializeSliderVisuals(voicesSlider, VoicesParam);

        StartCoroutine(ApplySavedVolumesDelayed());

        if (musicSlider != null) musicSlider.onValueChanged.AddListener(value => SetVolume(MusicParam, value, DefaultMusicDB));
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(value => SetVolume(SFXParam, value, DefaultSFXDB));
        if (ambientSlider != null) ambientSlider.onValueChanged.AddListener(value => SetVolume(AmbientParam, value, DefaultAmbientDB));
        if (voicesSlider != null) voicesSlider.onValueChanged.AddListener(value => SetVolume(VoicesParam, value, DefaultVoicesDB));

        if (settingsOpenButton != null) settingsOpenButton.onClick.AddListener(OpenSettings);
        if (settingsCloseButton != null) settingsCloseButton.onClick.AddListener(CloseSettings);

        // Ensure settings panel starts closed
        if (settingsPanel != null) settingsPanel.SetActive(false);

        // Check which scene we just loaded into and set the cursor accordingly
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == mainMenuSceneName)
        {
            // Main menu: Always keep cursor free and visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Gameplay scene: Lock cursor by default when starting the level
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ToggleSettings();
        }
    }

    private System.Collections.IEnumerator ApplySavedVolumesDelayed()
    {
        yield return null;
        ApplySavedVolumeToMixer(MusicParam, musicSlider, DefaultMusicDB);
        ApplySavedVolumeToMixer(SFXParam, sfxSlider, DefaultSFXDB);
        ApplySavedVolumeToMixer(AmbientParam, ambientSlider, DefaultAmbientDB);
        ApplySavedVolumeToMixer(VoicesParam, voicesSlider, DefaultVoicesDB);
    }

    private void ApplySavedVolumeToMixer(string parameterName, Slider slider, float defaultDB)
    {
        if (slider == null) return;
        float savedValue = slider.value;
        float targetDB = savedValue <= 0.0001f ? -80f : Mathf.Clamp(defaultDB + (Mathf.Log10(savedValue / 0.5f) * 20f), -80f, 20f);
        mainMixer.SetFloat(parameterName, targetDB);
    }

    private void SetVolume(string parameterName, float sliderValue, float defaultDB)
    {
        float targetDB = sliderValue <= 0.0001f ? -80f : Mathf.Clamp(defaultDB + (Mathf.Log10(sliderValue / 0.5f) * 20f), -80f, 20f);
        mainMixer.SetFloat(parameterName, targetDB);
        PlayerPrefs.SetFloat(parameterName, sliderValue);
    }

    private void InitializeSliderVisuals(Slider slider, string parameterName)
    {
        if (slider == null) return;
        float savedValue = PlayerPrefs.GetFloat(parameterName, 0.5f);
        slider.SetValueWithoutNotify(savedValue);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);

            // Unlock cursor so player can interact with UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);

            // Only lock the cursor back if we are NOT on the main menu
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName != mainMenuSceneName)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void ToggleSettings()
    {
        if (settingsPanel != null)
        {
            if (settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                OpenSettings();
            }
        }
    }
}