

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GorillaScreen : MonoBehaviour
{
    public GameObject character;
    public GameObject gorillaCharacter;
    public GameObject gorillaPopup;
    public GameObject menuBtns;
    public GameObject setDefaultpopup;
    public AudioSource audioSource;
    public GameObject PositionTR;
    public GameObject PCandPN;
    public GameObject saleEntry1;
    public GameObject saleEntry2;
    public GameObject netpay1;
    public GameObject bill_quantity;
    public GameObject bill_discount;
    public GameObject btn_setDefault;
    public GameObject tax_report;
    public GameObject sales_register;
    public GameObject tax_highlight;
    public GameObject popupBg;
    public GameObject mainBg;

    public Sprite salesBg1;
    public Sprite salesBg2;
    public Sprite salesBg3;
    public Sprite billBg;
    public Sprite gorillaBg;
    public Sprite menuBg;
    public Sprite taxBg1;
    public Sprite taxBg2;
    public GameObject Position_BL;
    public GameObject position2;
    public RuntimeAnimatorController newController1;
    public RuntimeAnimatorController newController2;
    public List<TimedAction> timedActions;
    public GameObject featureSelectionScreen;

    public PlayerButtonsManager playerButtonsManager;

    public GameObject startPos;
    void Start()
    {
        // print("gorilla start");
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

    public void ShowGorillaPanel()
    {
        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }

        gorillaPopup.gameObject.SetActive(true);
        gorillaCharacter.transform.position = PositionTR.transform.position;
    }
    public void HighLightcodeAndName()
    {
        PCandPN.SetActive(true);
    }

    public void HideHighLightPC_PN()
    {
        PCandPN.SetActive(false);
    }

    public void ShowSalesPanel()
    {
        popupBg.GetComponent<SpriteRenderer>().sprite = salesBg1;
    }

    public void HighLightEntry1()
    {
        saleEntry1.SetActive(true);
    }

    public void HideEntry1()
    {
        saleEntry1.SetActive(false);
    }

    public void HighLightNetPay1()
    {
        netpay1.SetActive(true);
        popupBg.GetComponent<SpriteRenderer>().sprite = salesBg2;
    }
    public void HideNetPay1()
    {
        netpay1.SetActive(false);
    }
    public void HighLightEntry2()
    {
        saleEntry2.SetActive(true);
    }
    public void HideEntry2()
    {
        saleEntry2.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = salesBg3;
    }

    public void ShowBill()
    {
        popupBg.GetComponent<SpriteRenderer>().sprite = billBg;
        gorillaCharacter.transform.position = Position_BL.transform.position;
        gorillaCharacter.GetComponent<Animator>().runtimeAnimatorController = newController1;
    }

   public void HighLightBillEntries()
    {
        bill_discount.SetActive(true);
        bill_quantity.SetActive(true);
    }
    public void ShowSetDefault()
    {
        bill_discount.SetActive(false);
        bill_quantity.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = gorillaBg;
        btn_setDefault.SetActive(true);
    }

    public void ShowSetDefaultPopup()
    {
        setDefaultpopup.SetActive(true);
    }

    public void HideSetDefaultPopup()
    {
        btn_setDefault.SetActive(false);
        setDefaultpopup.SetActive(false);
        gorillaPopup.SetActive(false);
        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = true;
        }
        character.transform.position = position2.transform.position;
        character.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        character.GetComponent<Animator>().runtimeAnimatorController = newController2;
    }
    public void ShowTaxButton()
    {
        tax_report.SetActive(true);
    }
    public void ShowTaxPage()
    {
        menuBtns.SetActive(false);
        tax_report.SetActive(false);
        mainBg.GetComponent<SpriteRenderer>().sprite = taxBg1;
    }
    public void ShowSaleRegBtn()
    {
        sales_register.SetActive(true);
    }

    public void ShowSaleRegPage()
    {
        sales_register.SetActive(false);
        mainBg.GetComponent<SpriteRenderer>().sprite = taxBg2;
    }
    public void HighlightTaxEntry()
    {
        tax_highlight.SetActive(true);
    }
    public void HideTaxEntry()
    {
        tax_highlight.SetActive(false);
        mainBg.GetComponent<SpriteRenderer>().sprite = menuBg;
        menuBtns.SetActive(true);
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            character.GetComponent<Animator>().SetBool("isVoiceComplete",true);
            Invoke("ExitScreen",10f);
        }
    }

    void ExitScreen()
    {
        playerButtonsManager.onBackButtonPressed(gameObject);
    }

    void OnEnable()
    {
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ResetScreen();
    }

    void ResetScreen()
    {
        gorillaPopup.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = gorillaBg;
        mainBg.GetComponent<SpriteRenderer>().sprite = menuBg;
        menuBtns.SetActive(true);
        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = true;
        }
        
        character.transform.position = startPos.transform.position;
        PCandPN.SetActive(false);
        saleEntry1.SetActive(false);
        netpay1.SetActive(false);
        saleEntry2.SetActive(false);
        bill_discount.SetActive(false);
        bill_quantity.SetActive(false);
        btn_setDefault.SetActive(false);
        setDefaultpopup.SetActive(false);
        tax_report.SetActive(false);
        sales_register.SetActive(false);
        tax_highlight.SetActive(false);
    }

}

