using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    public Language language;
    public GameObject mainMenuScreen;
    public GameObject welcomeScreen;
    public void SelectLanguage()
    {
        LanguageManager.Instance.SetLanguage(language);
        welcomeScreen.gameObject.SetActive(false);
        mainMenuScreen.gameObject.SetActive(true);
    }
}
