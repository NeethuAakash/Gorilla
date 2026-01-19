

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class Purchase : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject character;
    public GameObject HL_purchase;
    public GameObject startTransform;
    public GameObject HL_supplier;
    public GameObject HL_AddSplr;
    public GameObject HL_Save;
    public GameObject menuBtns;
    public GameObject positionBR;
    public GameObject HL_invoiceNO;
    public GameObject HL_Date;
    public GameObject HL_Barcode;
    public GameObject HL_BarcodeMarked;
    public GameObject HL_NewProduct;
    public GameObject HL_Search;
    public GameObject HL_PayOptions;
    public GameObject HL_SaveBill;
    public GameObject HL_Print;
    public GameObject HL_PrintBarcode;


    public GameObject bg;
    public Sprite spr_mainbg;
    public Sprite spr_purchaseBg;
    public Sprite spr_splrList;
    public Sprite spr_addSplr;
    public Sprite spr_oneItem;
    public Sprite spr_twoItems;

    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public RenderTexture renderTexture;
    public VideoClip vid_entri1;
    public VideoClip vid_entri2;
    public VideoClip vid_print;
    public VideoClip vid_printBarcode;

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

    public void Highlight_purchase()
    {
        HL_purchase.SetActive(true);
        SetCharacterToShow(HL_purchase);
        character.transform.localScale = new Vector3(1,1,1);
    }

    public void ShowPurchase()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_purchaseBg;
        menuBtns.SetActive(false);
        character.transform.localScale = new Vector3(2,2,2);
        character.transform.position = positionBR.transform.position;
    }

    public void Highlight_Supplier()
    {
        HL_supplier.SetActive(true);
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(HL_supplier);
    }

    public void ShowSupplierList()
    {
        HL_supplier.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_splrList;
        character.transform.localScale = new Vector3(2,2,2);
        character.transform.position = positionBR.transform.position;
    }
    
    public void HideSuppierList()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_purchaseBg;
    }

    public void Highlight_AddSupplier()
    {
        HL_AddSplr.SetActive(true);
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(HL_AddSplr);
    }

    public void ShowAddSupplier()
    {
        HL_AddSplr.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_addSplr;
        character.transform.localScale = new Vector3(2,2,2);
        character.transform.position = positionBR.transform.position;
    }

    public void HideAddSupplier()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_purchaseBg;
    }

    public void Highlight_invoiceNumber()
    {
        HL_invoiceNO.SetActive(true);
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(HL_invoiceNO);
    }

    public void HighLight_Date()
    {
        HL_invoiceNO.SetActive(false);
        HL_Date.SetActive(true);
    }

    public void HighLight_Search()
    {
        HL_Date.SetActive(false);
        HL_Search.SetActive(true);
    }
    public void HighLight_Barcode()
    {
        HL_Search.SetActive(false);
        HL_Barcode.SetActive(true);
        // SetCharacterToShow(HL_Barcode);
    }

    public void ShowBarcodeMarked()
    {
        HL_Barcode.SetActive(false);
        HL_BarcodeMarked.SetActive(true);
    }

    public void ShowProductEntry1Video()
    {
        HL_BarcodeMarked.SetActive(false);
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_entri1;
        videoPlayer.playbackSpeed = 3;
        videoPlayer.Play();
    }

    public void ShowBgWith1Entry()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_oneItem;
    }

    public void HighLight_Newproduct()
    {
        character.transform.position = positionBR.transform.position;
        character.transform.localScale = new Vector3(2,2,2);
        HL_NewProduct.SetActive(true);
    }

    public void ShowProductEntri2Video()
    {
        HL_NewProduct.SetActive(false);
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_entri2;
        videoPlayer.playbackSpeed = 2.2f;
        videoPlayer.Play();
    }

    public void ShowBgWith2Entries()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_twoItems;
    }

    public void HighLight_PaymentOptions()
    {
        HL_PayOptions.SetActive(true);
    }

    public void HighLight_Save()
    {
        HL_PayOptions.SetActive(false);
        HL_SaveBill.SetActive(true);
    }

    public void HighLight_Print()
    {
        HL_SaveBill.SetActive(false);
        HL_Print.SetActive(true);
    }

    public void ShowPrintVideo()
    {
        HL_Print.SetActive(false);
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_print;
        videoPlayer.playbackSpeed = .5f;
        videoPlayer.Play();
    }
    
    public void HighLight_PrintBarcode()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        HL_PrintBarcode.SetActive(true);
    }

    public void ShowPrintBarcodeVideo()
    {
        HL_PrintBarcode.SetActive(false);
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_printBarcode;
        videoPlayer.playbackSpeed = 1.5f;
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
    void OnEnable()
    {
        if(!hasStarted)
            return;
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ClearRenderTexture();
        ResetScreen();
    }
    void ResetScreen()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
        bg.GetComponent<SpriteRenderer>().sprite = spr_mainbg;
        HL_purchase.SetActive(false);
        menuBtns.SetActive(true);
        HL_supplier.SetActive(false);
        HL_AddSplr.SetActive(false);
        HL_invoiceNO.SetActive(false);
        HL_Date.SetActive(false);
        HL_Barcode.SetActive(false);
        HL_BarcodeMarked.SetActive(false);
        HL_Search.SetActive(false);
        HL_NewProduct.SetActive(false);
        HL_PayOptions.SetActive(false);
        HL_SaveBill.SetActive(false);
        HL_Print.SetActive(false);
        HL_PrintBarcode.SetActive(false);
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

