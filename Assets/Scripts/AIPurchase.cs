

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class AIPurchase : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject character;
    public GameObject startTransform;
    public GameObject menuBtns;
    public GameObject HL_record;
    public GameObject HL_SIH;
    public GameObject HL_Purchase;
    public GameObject HL_AddSupplier;
    public GameObject HL_Get;
    public GameObject HL_RDGorilla;
    public GameObject positionBR;
    public GameObject HL_UploadFile;
    public GameObject HL_BringToPurchase;
    public GameObject PurchaseBill;
    public GameObject HL_PayOptions;
    public GameObject HL_Save;
    public GameObject HL_Print;
    public GameObject PurchaseBillBig;

    public GameObject bg;
    public Sprite spr_menu;
    public Sprite spr_records;
    public Sprite spr_SIH;
    public Sprite spr_purchase;
    public Sprite spr_addSplr;
    public Sprite spr_filledSplr;
    public Sprite spr_purchaseWithSplrName;
    public Sprite spr_gorilla;
    public Sprite spr_gorillaWithEntry;
    public Sprite spr_BPE;
    public Sprite spr_BPEEdit;
    public Sprite spr_Saved;
    public Sprite spr_SIHFilled;

    bool hasStarted =  false;
    public PlayerButtonsManager playerButtonsManager;
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public RenderTexture renderTexture;
    public VideoClip vid_extractData;
    public VideoClip vid_print;
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

    public void ShowPurchaseBill()
    {
        PurchaseBillBig.SetActive(true);
    }
    public void Highlight_Record()
    {
        PurchaseBillBig.SetActive(false);
        HL_record.SetActive(true);
    }
    public void ShowRecords()
    {
        HL_record.SetActive(false);
        menuBtns.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_records;
    }
    public void Highlight_SIH()
    {
        HL_SIH.SetActive(true);
    }

    public void ShowSIH()
    {
        HL_SIH.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_SIH;
    }

    public void LoadMenu()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_menu;
        menuBtns.SetActive(true);
        HL_Purchase.SetActive(true);
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(HL_Purchase);
    }
    public void LoadPurchase()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_purchase;
        menuBtns.SetActive(false);
        HL_Purchase.SetActive(false);
        character.transform.position = positionBR.transform.position;
        character.transform.localScale = new Vector3(2,2,2);
    }
    public void Highlight_AddSupplier()
    {
        HL_AddSupplier.SetActive(true);
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(HL_AddSupplier);
    }
    public void LoadAddSupplier()
    {
        HL_AddSupplier.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_addSplr;
    }

    public void Highlight_Get()
    {
         HL_Get.SetActive(true);
         SetCharacterToShow(HL_Get);
    }

    public void LoadFilledSupplier()
    {
        HL_Get.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_filledSplr;
    }
    public void Highlight_RDGorilla()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_purchaseWithSplrName;
        HL_RDGorilla.SetActive(true);
        SetCharacterToShow(HL_RDGorilla);
    }

    public void LoadRDGorilla()
    {
        character.transform.position = positionBR.transform.position;
        character.transform.localScale = new Vector3(2,2,2);
        HL_RDGorilla.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_gorilla;
    }
    
    public void Highlight_uploadFile()
    {
        HL_UploadFile.SetActive(true);
    }

    public void ShowExtractVideo()
    {
        HL_UploadFile.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
        rawImage.enabled = true;
        videoPlayer.clip = vid_extractData;
        videoPlayer.playbackSpeed = 3f;
        videoPlayer.Play();
    }

    public void HL_BringToPurchaseEntry()
    {
        videoPlayer.gameObject.SetActive(false);
        rawImage.enabled = false;
        bg.GetComponent<SpriteRenderer>().sprite = spr_gorillaWithEntry;
        HL_BringToPurchase.SetActive(true);
    }

    public void LoadPurchaseEntry()
    {
        HL_BringToPurchase.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_BPE;
    }

    public void CrossCheck()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_BPEEdit;
        PurchaseBill.SetActive(true);
    }

    public void HighlightPayAndSave()
    {
        PurchaseBill.SetActive(false);
        HL_PayOptions.SetActive(true);
        HL_Save.SetActive(true);
    }

    public void Highlight_Print()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_Saved;
        HL_PayOptions.SetActive(false);
        HL_Save.SetActive(false);
        HL_Print.SetActive(true);
    }

    public void ShowPrintVideo()
    {
        HL_Print.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
        rawImage.enabled = true;
        videoPlayer.clip = vid_print;
        videoPlayer.playbackSpeed = 1.5f;
        videoPlayer.Play();
    }

    public void ShowSIHAgain()
    {
        videoPlayer.gameObject.SetActive(false);
        rawImage.enabled = false;
        bg.GetComponent<SpriteRenderer>().sprite = spr_SIHFilled;
    }

    void ResetScreen()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
        bg.GetComponent<SpriteRenderer>().sprite = spr_menu;
        menuBtns.SetActive(true);
        HL_record.SetActive(false);
        HL_SIH.SetActive(false);
        HL_Purchase.SetActive(false);
        HL_AddSupplier.SetActive(false);
        HL_Get.SetActive(false);
        HL_RDGorilla.SetActive(false);
        HL_UploadFile.SetActive(false);
        HL_BringToPurchase.SetActive(false);
        PurchaseBill.SetActive(false);
        HL_PayOptions.SetActive(false);
        HL_Save.SetActive(false);
        HL_Print.SetActive(false);
        PurchaseBillBig.SetActive(false);
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
        ClearRenderTexture();
        ResetScreen();
    }
    void ClearRenderTexture()
    {
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.black);

        RenderTexture.active = activeRT;
    }
    public void SetCharacterToShow(GameObject _gameObject)
    {
        Vector3 pos = _gameObject.transform.position + _gameObject.transform.right - _gameObject.transform.up/2;
        pos.z = -5;
        character.transform.position = pos;
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }
}

