

using UnityEngine;
using UnityEngine.SceneManagement;

public class FeatureSelectionScreen : MonoBehaviour
{
    bool isStart = true;
    void Start()
    {
        GetComponentInChildren<VoiceTrigger>().Play();
    }
    public void OnFeatureSelected(GameObject _selectedFeature)
    {
        // print("OnFeatureSelected" + _selectedFeature.name);
        gameObject.SetActive(false);
        _selectedFeature.SetActive(true);
    }
    void OnEnable()
    {
        // GetComponentInChildren<AudioSource>().UnPause();
        if(!isStart)
            GetComponentInChildren<VoiceTrigger>().Play();
        else
            isStart = false;
    }
}

