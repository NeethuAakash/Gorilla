

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WUThroughBill : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject character;
    public GameObject btnSales;
    public GameObject startTransform;
    public GameObject mainBg;
    public GameObject menuButtons;
    public Sprite salesPanel;
    public Sprite enterName;
    public Sprite newCustomer;
    public Sprite customerDetails;
    public Sprite cartOneItem;
    public Sprite cartManyItem;
    public Sprite bill;
    public Sprite menu;

    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    public List<TimedAction> timedActions;
    public PlayerButtonsManager playerButtonManager;
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
    void SetCharacterToShow(GameObject _gameObject)
    {
        Vector3 pos = _gameObject.transform.position + _gameObject.transform.right - _gameObject.transform.up/2;
        pos.z = -5;
        character.transform.position = pos;
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }

    public void ShowSalesButton()
    {
        character.transform.localScale = new Vector3(1,1,1);
        SetCharacterToShow(btnSales);
    }

    public void ShowSalesPanel()
    {
        menuButtons.SetActive(false);
        character.transform.localScale = new Vector3(2,2,2);
        mainBg.GetComponent<SpriteRenderer>().sprite = salesPanel;
        character.transform.position = startTransform.transform.position;
    }

    public void EnterDetails()
    {
        rawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        ClearRenderTexture();
        videoPlayer.Play();
        // mainBg.GetComponent<SpriteRenderer>().sprite = enterName;
    }

    public void ShowNewCustomerEntri()
    {
        // mainBg.GetComponent<SpriteRenderer>().sprite = newCustomer;
    }

    public void ShowEnteredCustomerDetails()
    {
        // mainBg.GetComponent<SpriteRenderer>().sprite = customerDetails;
    }

    public void ShowPurchasedItem1()
    {
        // mainBg.GetComponent<SpriteRenderer>().sprite = cartOneItem;
    }
    public void ShowPurchasedItems()
    {
        // mainBg.GetComponent<SpriteRenderer>().sprite = cartManyItem;
    }

    public void ShowBill()
    {
        // mainBg.GetComponent<SpriteRenderer>().sprite = bill;
    }

    void OnEnable()
    {
        if(!hasStarted)
            return;
        rawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ResetScreen();
    }
    void ResetScreen()
    {
        mainBg.GetComponent<SpriteRenderer>().sprite = menu;
        menuButtons.SetActive(true);
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
    }
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            rawImage.enabled = false;
            videoPlayer.gameObject.SetActive(false);
            ClearRenderTexture();
            playerButtonManager.onBackButtonPressed(gameObject);
        }
    }
    void ClearRenderTexture()
    {
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.black);

        RenderTexture.active = activeRT;
    }
}

