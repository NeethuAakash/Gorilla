

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;

public class BarcodeDesigns : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject character;
    public GameObject HL_barcodeMenu;
    public GameObject bg;
    public GameObject startTransform;
    public GameObject menuButtons;
    public GameObject HL_generalBarcode;
    public GameObject btnT1;
    public GameObject btnT2;
    public GameObject btnT8;
    public GameObject btnT16;
    public GameObject HL_apply;
    public GameObject HL_printPreview;

    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public RenderTexture renderTexture;
    public VideoClip clipT17;
    public VideoClip clipT2;

    public PlayerButtonsManager playerButtonsManager;

    public Sprite menuBg;
    public Sprite barcodeBg;
    public Sprite barcodeT1;
    public Sprite barcodeT2;
    public Sprite barcodeT8;
    public Sprite barcodeT16;
    
    public List<TimedAction> timedActions;
    bool hasStarted = false;
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
    public void HighLightBarcodeMenu()
    {
        HL_barcodeMenu.SetActive(true);
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(HL_barcodeMenu);
    }

    public void ShowBarcodeScreen()
    {
        HL_barcodeMenu.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = barcodeBg;
        menuButtons.SetActive(false);
    }

    public void HighLightGeneralBarcode()
    {
        HL_generalBarcode.SetActive(true);
        SetCharacterToShow(HL_generalBarcode);
    }

    public void ShowGeneralBarcodeScreen()
    {
        HL_generalBarcode.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = barcodeT1;
        character.transform.position = startTransform.transform.position;
    }

    public void ShowDesignT1()
    {
        btnT1.SetActive(true);
    }
    public void ShowDesignT2()
    {
        btnT1.SetActive(false);
        btnT16.SetActive(false);
        btnT2.SetActive(true);
        bg.GetComponent<SpriteRenderer>().sprite = barcodeT2;
        SetCharacterToShow(btnT2);
    }
     public void ShowDesignT8()
    {
        btnT2.SetActive(false);
        btnT8.SetActive(true);
        bg.GetComponent<SpriteRenderer>().sprite = barcodeT8;
        SetCharacterToShow(btnT8);
    }
    public void ShowDesignT16()
    {
        btnT8.SetActive(false);
        btnT16.SetActive(true);
        bg.GetComponent<SpriteRenderer>().sprite = barcodeT16;
        SetCharacterToShow(btnT16);
    }
    public void Highlight_apply()
    {
        btnT2.SetActive(false);
        HL_apply.SetActive(true);
        SetCharacterToShow(HL_apply);
    }
    public void Highlight_printPreview()
    {
        HL_apply.SetActive(false);
        HL_printPreview.SetActive(true);
        SetCharacterToShow(HL_printPreview);
    }

    public void ShowT2Video()
    {
        rawImage.enabled = true;
        ClearRenderTexture();
        videoPlayer.clip = clipT2;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }
    
    public void ShowDesignT17()
    {
        ClearRenderTexture();
        videoPlayer.clip = clipT17;
        videoPlayer.playbackSpeed = 1.4f;
        videoPlayer.Play();
    }

    void OnEnable()
    {
        if(!hasStarted)
            return;
        ResetScreen();
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
    }

    void ResetScreen()
    {
        HL_barcodeMenu.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = menuBg;
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
        menuButtons.SetActive(true);
        HL_generalBarcode.SetActive(false);
        HL_apply.SetActive(false);
        HL_printPreview.SetActive(false);
        btnT1.SetActive(false);
        btnT2.SetActive(false);
        btnT8.SetActive(false);
        btnT16.SetActive(false);
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
    public void SetCharacterToShow(GameObject _gameObject)
    {
        Vector3 pos = _gameObject.transform.position + _gameObject.transform.right - _gameObject.transform.up/2;
        pos.z = -5;
        character.transform.position = pos;
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }
    void ClearRenderTexture()
    {
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.black);

        RenderTexture.active = activeRT;
    }
}

