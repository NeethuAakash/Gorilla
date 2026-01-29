

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class DiscountAddingShortKeys : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject character;
    public GameObject menuBtns;
    // public GameObject startTransform;
    // public GameObject popupTransform;
    public GameObject HL_salePOS;
    
    public GameObject bg;
    public Sprite spr_mainbg;
    public Sprite spr_sale;

    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public RawImage rawImage;
    public VideoClip vid_discAmount;
    public VideoClip vid_discPerct;

    bool hasStarted = false; 
    public PlayerButtonsManager playerButtonsManager;   
    public List<TimedAction> timedActions;
    void Start()
    {
        hasStarted = true;
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

    public void Highlight_salePOS()
    {
        HL_salePOS.SetActive(true);
    }

    public void LoadSalePOS()
    {
        HL_salePOS.SetActive(false);
        menuBtns.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_sale;
    }

    public void ShowDiscountAmountVideo()
    {
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_discAmount;
        videoPlayer.playbackSpeed = 1.3f;
        videoPlayer.Play();
    }

    public void ShowDiscountPercentVideo()
    {
        ClearRenderTexture();
        videoPlayer.clip = vid_discPerct;
        videoPlayer.playbackSpeed = 1.3f;
        videoPlayer.Play();
    }




    void ResetScreen()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        HL_salePOS.SetActive(false);
        menuBtns.SetActive(true);
        bg.GetComponent<SpriteRenderer>().sprite = spr_mainbg;
    }
    void Update()
    {
         if (!audioSource.isPlaying)
        {
            // Debug.Log("Audio finished playing");
            rawImage.enabled = false;
            videoPlayer.gameObject.SetActive(false);
            ClearRenderTexture();
            playerButtonsManager.onBackButtonPressed(gameObject);
        }
    }
    void OnEnable()
    {
        if(!hasStarted)
            return;
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ResetScreen();
    }
    void ClearRenderTexture()
    {
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.black);

        RenderTexture.active = activeRT;
    }
    // public void SetCharacterToShow(GameObject _gameObject)
    // {
    //     Vector3 pos = _gameObject.transform.position + _gameObject.transform.right - _gameObject.transform.up/2;
    //     pos.z = -5;
    //     character.transform.position = pos;
    //     character.transform.localScale = new Vector3(1,1,1);
    //     character.GetComponent<Animator>().SetTrigger("doTouch");
    // }
    // void ResetZoom()
    // {
    //     rawImage.uvRect = new Rect(0, 0, 1f, 1f);
    // }
    // public void ClearVideo()
    // {
    //     rawImage.enabled = false;
    //     videoPlayer.gameObject.SetActive(false);
    // }

}

