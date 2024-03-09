using UnityEngine;
using TMPro;

public class DebugDisplay : MonoBehaviour
{
    private static DebugDisplay instance;
    public static DebugDisplay Instance { get { return instance; } }
    public TextMeshProUGUI debugTextUI; // Assign 
    private string debugText = "";
    private bool showDisplay = true;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // void Update(){
    //     if (Input.GetKeyDown(KeyCode.F1)) // Use F1 or any other key to toggle
    //     {
    //         DebugDisplay.Instance.ToggleDisplay();
    //     }
    // }


    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        debugText = logString + "\n" + debugText;
        // limit the number of logged messages to keep the display manageable
        if(debugText.Length > 5000)
        {
            debugText = debugText.Substring(0, 5000);
        }

         // Update the TextMeshPro text with the new log message
        if (debugTextUI != null)
        {
            debugTextUI.text = debugText;
        }
    }
    // Call this method to toggle the display on and off.
    public void ToggleDisplay()
    {
        showDisplay = !showDisplay;
    }
}
