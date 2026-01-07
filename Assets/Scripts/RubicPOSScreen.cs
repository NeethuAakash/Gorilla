

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class RubicPOSScreen: MonoBehaviour
{
    public GameObject character;
    public GameObject rubiksPopup;
    public GameObject popupBg;
    public GameObject HL_AIImage;
    public GameObject HL_Fruits;
    public GameObject HL_wholesale;
    public GameObject updatePrice;
    public GameObject quickPay;
    public GameObject pay;
    public GameObject paymentOptions;
    public Sprite RubicPOSBg;
    public Sprite RubikWithFruits;
    public Sprite RubikWithFruitImages;
    public Sprite bgWith1Item;
    public Sprite bgWithManyItems;
    public Sprite PayBg;
    public Sprite BillBg;

    public PlayerButtonsManager playerButtonsManager;

    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public RawImage rawImage;
    public AudioSource audioSource;
    public List<TimedAction> timedActions;

    public VideoClip addingNewItems;
    public VideoClip creatingAIImages;
    public VideoClip updatingPrice;

    void Awake()
    {
        rawImage.enabled = false;
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
    public void ShowPopup()
    {
        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
        rubiksPopup.SetActive(true);
    }

    public void ShowImages()
    {
        // popupBg.GetComponent<SpriteRenderer>().sprite = RubikWithFruits;
    }

    public void HighlightAIImageAndFruits()
    {
        HL_AIImage.SetActive(true);
        HL_Fruits.SetActive(true);
    }

    public void HideAIImageAndFruits()
    {
        HL_AIImage.SetActive(false);
        HL_Fruits.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = RubikWithFruitImages;

    }

    public void HighLightWholesaleRetail()
    {
        HL_wholesale.SetActive(true);
    }

    public void HideWholeSaleRetail()
    {
        HL_wholesale.SetActive(false);
    }

    public void ShowNewProds()
    {
        videoPlayer.gameObject.SetActive(true);
        rawImage.enabled = true;
        ClearRenderTexture();
        videoPlayer.Play();
    }

    public void PlayNewProds()
    {
        videoPlayer.clip = addingNewItems;
        videoPlayer.playbackSpeed = 15f;
        videoPlayer.Play();
    }

    public void PlayCreatingImages()
    {
        videoPlayer.clip = creatingAIImages;
        videoPlayer.playbackSpeed = 3f;
        videoPlayer.Play();
    }

    public void ShowUpdatePrice()
    {
   
        // updatePrice.SetActive(true);
        videoPlayer.clip = updatingPrice;
        videoPlayer.playbackSpeed = 3.5f;
        videoPlayer.Play();
    }

    public void HideUpdatePrice()
    {
        updatePrice.SetActive(false);
    }

    public void ShowOneItemInList()
    {
        videoPlayer.gameObject.SetActive(false);
        rawImage.enabled = false;
        popupBg.GetComponent<SpriteRenderer>().sprite = bgWith1Item;
    }

    public void ShowManyItemInList()
    {
        popupBg.GetComponent<SpriteRenderer>().sprite = bgWithManyItems;
    }

    public void HighLightQuickPay()
    {
        quickPay.SetActive(true);
    }

    // public void HighlightPay()
    // {
    //     quickPay.SetActive(false);
    //     pay.SetActive(true);
    // }

    // public void ShowPayScreen()
    // {
    //     pay.SetActive(false);
    //     popupBg.GetComponent<SpriteRenderer>().sprite = PayBg;
    // }

    // public void ShowPaymentOptions()
    // {
    //     paymentOptions.SetActive(true);
    // }

    // public void HidePaymentOptions()
    // {
    //     paymentOptions.SetActive(false);
    // }

    public void ShowBill()
    {
        quickPay.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = BillBg;
    }
   
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            playerButtonsManager.onBackButtonPressed(gameObject);
        }
    }
    void OnEnable()
    {
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

    void ResetScreen()
    {
        rubiksPopup.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = RubicPOSBg;

        SkinnedMeshRenderer[] meshes = character.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = true;
        }
    }
}

