using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public bool isStartMenu;
    public string firstFloorScene = "Floor1 - Tutorial";
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public Button loadButton;
    public TMP_Text statusText;
    public MenuAudio audioSettings;
    public PlayerControls playerControls;
    [Tooltip("Optional additional interaction scripts to disable during pause.")]
    public Behaviour[] gameplayControls = new Behaviour[0];

    private bool paused;
    private bool loading;
    private bool playerWasEnabled;
    private bool[] previousEnabled;
    private float previousTimeScale = 1f;
    private CursorLockMode previousCursorLock;
    private bool previousCursorVisible;

    private void Awake() { GameManager.GetOrCreate(); }

    private void Start()
    {
        if (mainPanel == null || settingsPanel == null)
        {
            Debug.LogError("Assign Main Panel and Settings Panel on GameMenu.", this);
            enabled = false;
            return;
        }
        settingsPanel.SetActive(false);
        mainPanel.SetActive(isStartMenu);
        if (isStartMenu)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bool valid = MenuSave.TryRead(out _, out string error);
            if (loadButton != null) loadButton.interactable = valid;
            ShowStatus(valid ? "" : error);
        }
        else FindPlayer();
    }

    private void FindPlayer()
    {
        if (playerControls == null) playerControls = FindAnyObjectByType<PlayerControls>();
    }

    private void Update()
    {
        if (loading) return;
        bool pressed = Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
        if (!isStartMenu && playerControls != null)
        {
            var input = playerControls.GetComponent<PlayerInput>();
            var action = input != null && input.actions != null ? input.actions.FindAction("Pause", false) : null;
            pressed |= action != null && action.WasPressedThisFrame();
        }
        if (!pressed) return;
        if (settingsPanel.activeSelf) CloseSettings();
        else if (!isStartMenu) { if (paused) Resume(); else Pause(); }
    }

    public void Pause()
    {
        if (isStartMenu || paused || loading || Time.timeScale == 0f) return;
        FindPlayer();
        if (playerControls == null) return;
        paused = true;
        previousTimeScale = Time.timeScale;
        previousCursorLock = Cursor.lockState;
        previousCursorVisible = Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        playerWasEnabled = playerControls.enabled;
        playerControls.enabled = false; // Existing OnDisable stops footsteps.
        previousEnabled = new bool[gameplayControls.Length];
        for (int i = 0; i < gameplayControls.Length; i++)
        {
            var control = gameplayControls[i];
            if (control == null || control == playerControls || control == this || control == audioSettings) continue;
            previousEnabled[i] = control.enabled;
            control.enabled = false;
        }
        mainPanel.SetActive(true);
    }

    public void Resume()
    {
        if (!paused) return;
        SaveVolume();
        settingsPanel.SetActive(false);
        mainPanel.SetActive(false);
        RestoreControls();
    }

    private void RestoreControls()
    {
        if (!paused) return;
        paused = false;
        Time.timeScale = previousTimeScale;
        Cursor.lockState = previousCursorLock;
        Cursor.visible = previousCursorVisible;
        if (playerControls != null) playerControls.enabled = playerWasEnabled;
        for (int i = 0; i < gameplayControls.Length; i++)
        {
            var control = gameplayControls[i];
            if (control != null && control != playerControls && control != this && control != audioSettings)
                control.enabled = previousEnabled[i];
        }
    }

    private void OnDisable() { RestoreControls(); }

    public void OpenSettings()
    {
        if (loading || (!isStartMenu && !paused)) return;
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        SaveVolume();
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void StartNewGame()
    {
        if (!isStartMenu || loading || !CanLoad(firstFloorScene)) return;
        GameManager.GetOrCreate().ResetProgress();
        // The previous disk save is preserved until this run is saved.
        LoadScene(firstFloorScene);
    }

    public void LoadGame()
    {
        if (!isStartMenu || loading) return;
        if (!MenuSave.TryRead(out GameSaveData data, out string error))
        {
            ShowStatus(error);
            return;
        }
        GameManager.GetOrCreate().RestoreProgress(data);
        LoadScene(data.scene);
    }

    private bool CanLoad(string scene)
    {
        if (!string.IsNullOrEmpty(scene) && Application.CanStreamedLevelBeLoaded(scene)) return true;
        ShowStatus("Scene is not in the build scene list: " + scene);
        return false;
    }

    private void LoadScene(string scene)
    {
        SaveVolume();
        loading = true;
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(scene);
    }

    public void SaveAndQuit()
    {
        if (isStartMenu || !paused || loading) return;
        FindPlayer();
        if (playerControls == null) { ShowStatus("Cannot save: overworld player was not found."); return; }
        var data = GameManager.GetOrCreate().CaptureProgress(
            SceneManager.GetActiveScene().name, playerControls.transform.position);
        if (!MenuSave.TryWrite(data, out string error)) { ShowStatus(error); return; }
        QuitGame();
    }

    public void QuitGame()
    {
        SaveVolume();
        RestoreControls();
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SaveVolume()
    {
        if (audioSettings != null) audioSettings.SaveSettings();
        else PlayerPrefs.Save();
    }

    private void ShowStatus(string message)
    {
        if (statusText != null) statusText.text = message;
        else if (!string.IsNullOrEmpty(message)) Debug.LogWarning(message, this);
    }
}
