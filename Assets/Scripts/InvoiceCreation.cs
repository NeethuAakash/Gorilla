

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class InvoiceCreation : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject character;
    public GameObject HL_salePOS;
    public GameObject startTransform;
    public GameObject menuBtns;
    public GameObject positionBR;
    public GameObject HL_add_customer;
    public GameObject HL_name_phone;
    public GameObject HL_availQuantity;
    public GameObject HL_quickPay;

    public GameObject bg;
    public Sprite spr_salesbg;
    public Sprite spr_mainbg;
    public Sprite spr_addCusbg;
    public Sprite spr_nameEntri;
    public Sprite spr_nameSelected;
    public Sprite spr_productSearch;
    public Sprite spr_Entri1;
    public Sprite spr_Entri2;
    public Sprite spr_available;

    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public VideoClip videoClip;

    public List<TimedAction> timedActions;
    public PlayerButtonsManager playerButtonsManager;
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

    public void Highlight_SalePOS()
    {
        character.transform.localScale = new Vector3(1,1,1);
        HL_salePOS.SetActive(true);
        SetCharacterToShow(HL_salePOS);
    }

    public void ShowSalesPanel()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_salesbg;
        menuBtns.SetActive(false);
        character.transform.position = positionBR.transform.position;
        // character.transform.localScale = positionBR.transform.localScale;
    }

    public void HighLight_customerAdd()
    {
        HL_add_customer.SetActive(true);
        SetCharacterToShow(HL_add_customer);
    }

    public void ShowAddCustomer()
    {
        HL_add_customer.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_addCusbg;
        character.transform.position = positionBR.transform.position;
    }

    public void Highlight_nameAndPhone()
    {
        HL_name_phone.SetActive(true);
        bg.GetComponent<SpriteRenderer>().sprite = spr_salesbg;
    }

    public void TypeName()
    {
        HL_name_phone.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_nameEntri;
    }

    public void SelectName()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_nameSelected;
    }
    public void productSearch()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_productSearch;
    }
    public void ShowAvailableQuantity()
    {
        HL_availQuantity.SetActive(true);
        bg.GetComponent<SpriteRenderer>().sprite = spr_available;
    }

    public void HideAvailableQuantity()
    {
        HL_availQuantity.SetActive(false);
    }
    public void ShowEntri1()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_Entri1;
    }

    public void PlayBarcodeVideo()
    {
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.playbackSpeed = 1f;
        videoPlayer.Play();
    }

    public void ShowEntri2()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_Entri2;
    }

    public void HighlightQuickPay()
    {
        HL_quickPay.SetActive(true);
    }

    public void PlayBillVideo()
    {
        HL_quickPay.SetActive(false);
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = videoClip;
        videoPlayer.playbackSpeed = 0.8f;
        videoPlayer.Play();
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
    void OnEnable()
    {
        if(!hasStarted)
            return;
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ResetScreen();
    }

    void ResetScreen()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        HL_salePOS.SetActive(false);
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
        bg.GetComponent<SpriteRenderer>().sprite = spr_mainbg;
        menuBtns.SetActive(true);
        HL_add_customer.SetActive(false);
        HL_name_phone.SetActive(false);
        HL_availQuantity.SetActive(false);
        HL_quickPay.SetActive(false);
    }
    void ClearRenderTexture()
    {
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.black);

        RenderTexture.active = activeRT;
    }
}

