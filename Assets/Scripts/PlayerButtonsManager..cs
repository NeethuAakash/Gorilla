using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerButtonsManager : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject featureSelectionScreen;
    public GameObject languageSelectionScreen;

    // public void TogglePause()
    // {
    //     if (IsPaused)
    //         ResumeGame();
    //     else
    //         PauseGame();
    // }

    // void PauseGame()
    // {
    //     Time.timeScale = 0f;
    //     AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

    //     foreach (AudioSource audio in audios)
    //     {
    //         audio.Pause();
    //     }
    //     IsPaused = true;
    // }

    // void ResumeGame()
    // {
    //     Time.timeScale = 1f;
    //     AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

    //     foreach (AudioSource audio in audios)
    //     {
    //         audio.UnPause();
    //     }
    //     IsPaused = false;
    // }

    public void onBackButtonPressed(GameObject currentScreen)
    {
        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource audio in audios)
        {
            audio.Pause();
        }

        if (currentScreen.name.Equals("FeatureSelection"))
        {
            currentScreen.SetActive(false);
            languageSelectionScreen.SetActive(true);
            return;
        }
        currentScreen.SetActive(false);
        featureSelectionScreen.SetActive(true);
        // print("onBackButtonPressed");
    }
}
