

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddProductScreen: MonoBehaviour
{
    public GameObject character;

    public GameObject addProdsPopup;

    public GameObject addProdscharacter;

    public GameObject Position;
    public GameObject entries;
    public GameObject btn_settings;
    public GameObject entriHL;
    public GameObject barcodeHL;
    public GameObject btn_quickPay;
    public Sprite settingsBg;
    public Sprite salesBg;
    public Sprite salesOneItem;
    public Sprite salesManyItems;
    public Sprite BillBg;
    public GameObject popupBg;

    public AudioSource audioSource;
    public List<TimedAction> timedActions;

    void Awake()
    {
        StartCoroutine(PlayVoiceWithTimedActions());

        // Invoke("ShowPopup",5f);
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
    public void ShowPopup()
    {
        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
        addProdsPopup.SetActive(true);
        addProdscharacter.transform.position = Position.transform.position;
    }

    public void HighlightEntries()
    {
        entries.SetActive(true);
    }

    public void HideEntries()
    {
        entries.SetActive(false);
    }

    public void HighlightSettingsBtn()
    {
        btn_settings.SetActive(true);
    }

    public void ShowSettingsScreen()
    {
        btn_settings.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = settingsBg;
    }

    public void HideSettingsScreen()
    {
        popupBg.GetComponent<SpriteRenderer>().sprite = salesBg;
    }

    public void HighlightNameAndBarcode()
    {
        entriHL.SetActive(true);
        barcodeHL.SetActive(true);
    }
    public void HideNameAndBarcode()
    {
        entriHL.SetActive(false);
        barcodeHL.SetActive(false);
    }

    public void ShowOneItemInSales()
    {
        popupBg.GetComponent<SpriteRenderer>().sprite = salesOneItem;
    }
    public void ShowManyItemInSales()
    {
        popupBg.GetComponent<SpriteRenderer>().sprite = salesManyItems;
    }

    public void HighLightQuickPay()
    {
        btn_quickPay.SetActive(true);
    }

    public void ShowBill()
    {
        btn_quickPay.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = BillBg;
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
    void OnEnable()
    {
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ResetScreen();
    }

    void ResetScreen()
    {
        addProdsPopup.SetActive(false);
        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = true;
        }
    }
}

