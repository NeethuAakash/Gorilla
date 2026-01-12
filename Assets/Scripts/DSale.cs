

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;

public class DSale : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject character;
    public GameObject btnSalePOS;
    public GameObject bg;
    public GameObject startTransform;
    public GameObject menuButtons;
    public GameObject HL_DSale;
    public Sprite salesBg;
    public Sprite DsaleBg;
    public Sprite menuBg;
    public PlayerButtonsManager playerButtonsManager;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public List<TimedAction> timedActions;

    void Awake()
    {
        StartCoroutine(PlayVoiceWithTimedActions());
    }
    IEnumerator PlayVoiceWithTimedActions()
    {
        character.GetComponent<VoiceTrigger>().Play();

        int index = 0;

        while (audioSource.isPlaying && index < timedActions.Count)
        {
            if (audioSource.time >= timedActions[index].time)
            {
                timedActions[index].action.Invoke();
                index++;
            }

            yield return null;
        }
    }
    void SetCharacterToShow(GameObject _gameObject)
    {
        Vector3 pos = _gameObject.transform.position + _gameObject.transform.right - _gameObject.transform.up/2;
        pos.z = -5;
        character.transform.position = pos;
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }

    public void HighLightSalePOS()
    {
        btnSalePOS.SetActive(true);
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(btnSalePOS);
    }

    public void ShowPopup()
    {
        menuButtons.SetActive(false);
        btnSalePOS.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = salesBg;
    }

    public void HL_Dsale()
    {
        HL_DSale.SetActive(true);
        SetCharacterToShow(HL_DSale);
    }
    public void ShowDsaleWindow()
    {
        bg.GetComponent<SpriteRenderer>().sprite = DsaleBg;
    }

    public void StartVideo()
    {
        HL_DSale.SetActive(false);
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            rawImage.enabled = false;
            videoPlayer.gameObject.SetActive(false);
            ClearRenderTexture();
            playerButtonsManager.onBackButtonPressed(gameObject);
        }
    }
    void OnEnable()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ResetScreen();
    }
    void ResetScreen()
    {
        bg.GetComponent<SpriteRenderer>().sprite = menuBg;
        
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
       
        menuButtons.SetActive(true);
        btnSalePOS.SetActive(false);
        HL_DSale.SetActive(false);
    }
    void ClearRenderTexture()
    {
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.black);

        RenderTexture.active = activeRT;
    }
}

