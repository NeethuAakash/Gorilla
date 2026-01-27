

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class SalesmanCommission : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject character;
    public GameObject menuBtns;
    public GameObject startTransform;
    public GameObject popupTransform;
    public GameObject HL_masterEntry;
    public GameObject HL_salesperson;
    public GameObject HL_save;
    public GameObject HL_new;
    public GameObject HL_utilities;
    public GameObject HL_PBE;
    public GameObject HL_salesmanPercent;
    public GameObject HL_Cell;
    public GameObject HL_SalePOS;
    public GameObject HL_Salesman;
    public GameObject HL_Commission;
    public GameObject HL_QuickPay;
    public GameObject HL_reports;
    public GameObject HL_SPL;
    public GameObject HL_inventory;
    public GameObject HL_SPP;
    
    
    public GameObject bg;
    public Sprite spr_mainbg;
    public Sprite spr_masterEntry;
    public Sprite spr_salesperson;
    public Sprite spr_SM1filled;
    public Sprite spr_SM1Saved;
    public Sprite spr_SM2Empty;
    public Sprite spr_SM2saved;
    public Sprite spr_utilities;
    public Sprite spr_PBE;
    public Sprite spr_sale;
    public Sprite spr_saleFilled;
    public Sprite spr_saleBill;
    public Sprite spr_reports;
    public Sprite spr_SPL;
    public Sprite spr_inventory;
    public Sprite spr_SPP;

    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public RawImage rawImage;
    public VideoClip vid_addCommission;
    public VideoClip vid_selling;
    public VideoClip vid_ledger;
    public VideoClip vid_salesPersonPayment;
    public VideoClip vid_ledger2;

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

        int index = 0;//31;//cheat

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

    public void Highlight_masterEntry()
    {
        HL_masterEntry.SetActive(true);
    }
    public void LoadMasterEntry()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_masterEntry;
        HL_masterEntry.SetActive(false);
        menuBtns.SetActive(false);
    }

    public void Highlight_SalesPerson()
    {
        HL_salesperson.SetActive(true);
        SetCharacterToShow(HL_salesperson);
    }

    public void LoadSalesperson()
    {
        HL_salesperson.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_salesperson;
        character.transform.localScale = popupTransform.transform.localScale;
        character.transform.position = popupTransform.transform.position;
    }

    public void FillSM1()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_SM1filled;
    }

    public void Highlight_save()
    {
        HL_save.SetActive(true);
        SetCharacterToShow(HL_save);
    }

    public void ShowSaved1()
    {
        HL_save.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_SM1Saved;
    }

    public void Highlight_New()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_SM2Empty;
        HL_new.SetActive(true);
        SetCharacterToShow(HL_new);
    }

    public void HideNew()
    {
        HL_new.SetActive(false);
    }

    public void ShowSaved2()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_SM2saved;
    }

    public void LoadMenu()
    {
        print("time:"+audioSource.time);
        character.transform.localScale = startTransform.transform.localScale;
        character.transform.position = startTransform.transform.position;
        bg.GetComponent<SpriteRenderer>().sprite = spr_mainbg;
        menuBtns.SetActive(true);
    }

    public void Highlight_Utilities()
    {
        HL_utilities.SetActive(true);
    }

    public void LoadUtilities()
    {
        HL_utilities.SetActive(false);
        menuBtns.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_utilities;
    }

    public void Highlight_ProductBulkEditor()
    {
        HL_PBE.SetActive(true);
        SetCharacterToShow(HL_PBE);
    }

    public void Load_ProductBulkEditor()
    {
        HL_PBE.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_PBE;
        character.transform.localScale = popupTransform.transform.localScale;
        character.transform.position = popupTransform.transform.position;
    }

    public void Highlight_salesmanPercent()
    {
        HL_salesmanPercent.SetActive(true);
        SetCharacterToShow(HL_salesmanPercent);
    }

    public void Hide_salesmanPercent()
    {
        HL_salesmanPercent.SetActive(false);
    }

    public void Highlight_Cell()
    {
        HL_Cell.SetActive(true);
        SetCharacterToShow(HL_Cell);
    }

    public void ShowCommissionAddingVideo()
    {
        HL_Cell.SetActive(false);
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_addCommission;
        videoPlayer.playbackSpeed = 1.5f;
         rawImage.uvRect = new Rect(0, .5f, .5f, 0.5f);
         Invoke("ResetZoom",8f);
        videoPlayer.Play();
    }
    public void Highlight_SalePOS()
    {
        HL_SalePOS.SetActive(true);
    }
   
    public void LoadSalePOS()
    {
        HL_SalePOS.SetActive(false);
        menuBtns.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_sale;
        character.transform.localScale = popupTransform.transform.localScale;
        character.transform.position = popupTransform.transform.position;
    }

    public void Highlight_Salesman()
    {
        HL_Salesman.SetActive(true);
        SetCharacterToShow(HL_Salesman);
    }

    public void ShowSellingVideo()
    {
        HL_Salesman.SetActive(false);
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_selling;
        videoPlayer.playbackSpeed = 3f;
        videoPlayer.Play();
    }
    public void Highlight_CommisionInSales()
    {
        ClearVideo();
        bg.GetComponent<SpriteRenderer>().sprite = spr_saleFilled;
        HL_Commission.SetActive(true);
        SetCharacterToShow(HL_Commission);
    }

    public void Highlight_QuickPay()
    {
        HL_Commission.SetActive(false);
        HL_QuickPay.SetActive(true);
        SetCharacterToShow(HL_QuickPay);
    }
    public void HideQuickPay()
    {
        HL_QuickPay.SetActive(false);
    }

    public void LoadBill()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_saleBill;

        character.transform.localScale = popupTransform.transform.localScale;
        character.transform.position = popupTransform.transform.position;
    }

    public void Highlight_reports()
    {
        // print("Highlight_reports");
        HL_reports.SetActive(true);
    }

    public void LoadReports()
    {
        // print("LoadReports");
        HL_reports.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_reports;
        menuBtns.SetActive(false);
    }

    public void Highlight_SPL()
    {
        HL_SPL.SetActive(true);
        SetCharacterToShow(HL_SPL);
    }

    public void LoadSPL()
    {
        HL_SPL.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_SPL;
    }

    public void ShowLedgerVideo()
    {
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_ledger;
        videoPlayer.playbackSpeed = 1.3f;
        videoPlayer.Play();
    }

    public void Highlight_Inventory()
    {
        HL_inventory.SetActive(true);
    }

    public void LoadInventory()
    {
        HL_inventory.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_inventory;
        menuBtns.SetActive(false);
    }
    public void Highlight_SalePersonPayment()
    {
        HL_SPP.SetActive(true);
        SetCharacterToShow(HL_SPP);
    }

    public void LoadSalePersonPayment()
    {
        HL_SPP.SetActive(false);
        bg.GetComponent<SpriteRenderer>().sprite = spr_SPP;

        character.transform.localScale = popupTransform.transform.localScale;
        character.transform.position = popupTransform.transform.position;
    }

    public void ShowPaymentVideo()
    {
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_salesPersonPayment;
        videoPlayer.playbackSpeed = 2.2f;
        videoPlayer.Play();
        Invoke("Zoom",5f);
    }
    public void Zoom()
    {
        rawImage.uvRect = new Rect(0, .25f, .5f, 0.3f);
         Invoke("ResetZoom",3f);
    }

    public void LoadLedgerAgain()
    {
        bg.GetComponent<SpriteRenderer>().sprite = spr_SPL;
    }

    public void ShowLedger2Video()
    {
        ClearRenderTexture();
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = vid_ledger2;
        videoPlayer.playbackSpeed = 2.2f;
        videoPlayer.Play();
    }







   
   
   
   
   
   
   
    void ResetScreen()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
        bg.GetComponent<SpriteRenderer>().sprite = spr_mainbg;
        menuBtns.SetActive(true);
        HL_masterEntry.SetActive(false);
        HL_salesperson.SetActive(false);
        HL_save.SetActive(false);
        HL_new.SetActive(false);
        HL_utilities.SetActive(false);
        HL_PBE.SetActive(false);
        HL_salesmanPercent.SetActive(false);
        HL_Cell.SetActive(false);
        HL_SalePOS.SetActive(false);
        HL_Salesman.SetActive(false);
        HL_Commission.SetActive(false);
        HL_QuickPay.SetActive(false);
        HL_reports.SetActive(false);
        HL_SPL.SetActive(false);
        HL_inventory.SetActive(false);
        HL_SPP.SetActive(false);
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
    public void SetCharacterToShow(GameObject _gameObject)
    {
        Vector3 pos = _gameObject.transform.position + _gameObject.transform.right - _gameObject.transform.up/2;
        pos.z = -5;
        character.transform.position = pos;
        character.transform.localScale = new Vector3(1,1,1);
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }
    void ResetZoom()
    {
        rawImage.uvRect = new Rect(0, 0, 1f, 1f);
    }
    public void ClearVideo()
    {
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
    }

}

