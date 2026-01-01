

using UnityEngine;

public class Welcome : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject mainMenuScreen;

    public GameObject character;

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            character.GetComponent<Animator>().SetBool("isVoiceComplete",true);
            // Debug.Log("Audio finished playing");
            // mainMenuScreen.SetActive(true);      //TODO:Uncomment
            // gameObject.SetActive(false); 
        }
    }
}

