

using UnityEngine;

public class Welcome : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject character;

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            character.GetComponent<Animator>().SetBool("isVoiceComplete",true);
            // Debug.Log("Audio finished playing");
        }
    }
}

