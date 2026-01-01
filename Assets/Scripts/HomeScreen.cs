

using UnityEngine;
using System.Collections;


public class HomeScreen : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject gorillaScreen;
    public GameObject character;
    public GameObject btnGorilla;
    public GameObject[] highlightBtn;
    bool isAudioPlaying;

    void Awake()
    {
        character.GetComponent<VoiceTrigger>().Play();
        isAudioPlaying = true;
    }

    void Update()
    {
        if(!audioSource.isPlaying && isAudioPlaying)
        // if(isAudioPlaying)//  CHEAT
        {
            isAudioPlaying = false;
            character.GetComponent<Animator>().SetBool("isJumpingUp",true);
            StartCoroutine(MoveSprite(character,btnGorilla));
        }
    }

    IEnumerator MoveSprite(GameObject _gameObject,GameObject _targetObject)
{
    Vector3 startPosition = _gameObject.transform.position;
    Vector3 endPosition = new Vector3(_targetObject.transform.position.x - 1.1f,_targetObject.transform.position.y - 1.5f,_gameObject.transform.position.z);
    Vector3 startScale = _gameObject.transform.localScale;
    Vector3 endScale = new Vector3(2f,2f,1f);

    float t = 0;

    while (t < 1)
    {
        t += Time.deltaTime * .4f;
        _gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, t);
        _gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, t);

        yield return null;
    }
    character.GetComponent<Animator>().SetBool("isJumpingUp",false);
    // Invoke("LoadGorillaScreen",2f);
    LoadGorillaScreen();
}

void LoadGorillaScreen()
    {
        gameObject.SetActive(false);
        gorillaScreen.SetActive(true);
    }

}

