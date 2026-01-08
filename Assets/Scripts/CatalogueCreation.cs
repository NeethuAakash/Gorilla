

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
public class CatalogueCreation : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerButtonsManager playerButtonsManager;
    public GameObject character;

    public GameObject startTransform;
    public GameObject popup;
    public GameObject popupBg;
    public GameObject btnProdCatalogue;
    public GameObject position_TR;
    public GameObject HL_ticks;
    public GameObject HL_category;
    public GameObject btnBulkImageUpdate;
    public GameObject btnCatalogueStyle;

    public RenderTexture renderTexture;
    public VideoPlayer styleSelectionVidPlayer;
    public RawImage styleSelectionrawImage;
    public Sprite bgutilitiesView;
    public Sprite bgCatalogueView;
    public Sprite bgCatalogueWithAllImages;
    public VideoClip makingCatalog;
    public List<TimedAction> timedActions;

   void Start()
    {
        styleSelectionrawImage.enabled = false;
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
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            styleSelectionVidPlayer.gameObject.SetActive(false);
            styleSelectionrawImage.enabled = false;
            playerButtonsManager.onBackButtonPressed(gameObject);
        }
    }

    public void ShowPopup()
    {
        character.GetComponent<Animator>().SetBool("isPopup",true);
        popup.SetActive(true);
    }

    public void HighlightProdCatalogueBtn()
    {
        btnProdCatalogue.SetActive(true);
        SetCharacterPositionTo(btnProdCatalogue);
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }

    void SetCharacterPositionTo(GameObject _gameObject)
    {
        Vector3 pos = _gameObject.transform.position + _gameObject.transform.right;
        pos.z = -5;
        character.transform.position = pos;
    }
    public void HideProdCatalogueBtn()
    {
        btnProdCatalogue.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = bgCatalogueView;
        character.transform.position = position_TR.transform.position;
        character.gameObject.transform.localScale = new Vector3(1,1,1);
    }

    public void HL_categorySelection()
    {
        HL_category.SetActive(true);
        SetCharacterPositionTo(HL_category);
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }

    public void hide_categorySelection()
    {
        HL_category.SetActive(false);
    }

    public void HL_tickMarks()
    {
        HL_ticks.SetActive(true);
        SetCharacterPositionTo(HL_ticks);
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }

    public void HL_bulkImageUpdate()
    {
        HL_ticks.SetActive(false);
        btnBulkImageUpdate.SetActive(true);
        SetCharacterPositionTo(btnBulkImageUpdate);
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }
    public void ShowUpdatedImages()
    {
        btnBulkImageUpdate.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = bgCatalogueWithAllImages;
    }

    public void HL_catStyleBtn()
    {
        btnCatalogueStyle.SetActive(true);
        SetCharacterPositionTo(btnCatalogueStyle);
        character.GetComponent<Animator>().SetTrigger("doTouch");
    }

    public void ShowCatalogStyleSelection()
    {
        btnCatalogueStyle.SetActive(false);
        styleSelectionVidPlayer.gameObject.SetActive(true);
        styleSelectionrawImage.enabled = true;
        ClearRenderTexture();
        styleSelectionVidPlayer.Play();
    }

    public void ShowCatalogMaking()
    {
        styleSelectionVidPlayer.clip = makingCatalog;
        styleSelectionVidPlayer.playbackSpeed = 2.5f;
        styleSelectionVidPlayer.Play();
    }

    public void HideCatalogMaking()
    {
        styleSelectionVidPlayer.gameObject.SetActive(false);
        styleSelectionrawImage.enabled = false;
    }

    void OnEnable()
    {
        audioSource.UnPause();
        StartCoroutine(PlayVoiceWithTimedActions());
        ResetScreen();
    }
    void ResetScreen()
    {
        popup.SetActive(false);
        popupBg.GetComponent<SpriteRenderer>().sprite = bgutilitiesView;
        character.transform.position = startTransform.transform.position;
        character.transform.localScale = startTransform.transform.localScale;
    }
    void ClearRenderTexture()
    {
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.black);

        RenderTexture.active = activeRT;
    }
}

