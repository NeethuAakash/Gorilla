using UnityEngine;

public class AppManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            Debug.Log("App Paused");
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Debug.Log("App Resumed");
        }
    }

    void OnApplicationQuit()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
}
