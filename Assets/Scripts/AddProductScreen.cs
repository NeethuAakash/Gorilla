

using UnityEngine;

public class AddProductScreen: MonoBehaviour
{
    public GameObject character;

    public GameObject addProdsPopup;
    public GameObject salePOSScreen;

    public GameObject addProdscharacter;

    public GameObject Position;

    public AudioSource audioSource;

    void Awake()
    {
        character.GetComponent<VoiceTrigger>().Play();
        Invoke("ShowPopup",5f);
    }
    void ShowPopup()
    {
        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
        addProdsPopup.SetActive(true);
        addProdscharacter.transform.position = Position.transform.position;
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
            salePOSScreen.SetActive(true);
        }
    }
}

