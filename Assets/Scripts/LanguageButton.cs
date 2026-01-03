using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    public Language language;
    public GameObject featureSelectionScreen;
    public GameObject welcomeScreen;
    public void SelectLanguage()
    {
        LanguageManager.Instance.SetLanguage(language);
        welcomeScreen.gameObject.SetActive(false);
        featureSelectionScreen.gameObject.SetActive(true);
    }
}
