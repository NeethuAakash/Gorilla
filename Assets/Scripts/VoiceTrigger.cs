using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    public VoiceID voiceID;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        // Debug.Log("Play voice");
        VoiceManager.Instance.PlayVoice(voiceID, audioSource);
    }
}
