
using UnityEngine;
using System.Collections.Generic;

public enum VoiceID
{
    Menu,
    Gorilla,
    AddProduct,
    FeatureSelection,
    RubicPOS,
    Catalogue,
    WUThroughBill,
    DSale,
    BarcodeDesigns,
    InvoiceCreation,
    Purchase,
    AIPurchase,
    SalesmanCommission,
    DiscountAddingShortcutKeys,
    AutomaticDiscountInInvoice
}

[System.Serializable]
public class VoiceData
{
    public VoiceID voiceID;
    public AudioClip english;
    public AudioClip hindi;
    public AudioClip malayalam;

    public AudioClip GetClip(Language lang)
    {
        return lang switch
        {
            Language.English => english,
            Language.Hindi => hindi,
            Language.Malayalam => malayalam,
            _ => english
        };
    }
}

public class VoiceManager : MonoBehaviour
{
   public static VoiceManager Instance;

    public List<VoiceData> voiceDatabase;
    private Dictionary<VoiceID, VoiceData> voiceDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildDictionary();
        }
        else
            Destroy(gameObject);
    }

    void BuildDictionary()
    {
        voiceDict = new Dictionary<VoiceID, VoiceData>();
        foreach (var voice in voiceDatabase)
            voiceDict[voice.voiceID] = voice;
    }

    public void PlayVoice(VoiceID id, AudioSource source)
    {
        // Debug.Log("PlayVoice:source"+source);
        Language lang = LanguageManager.Instance.currentLanguage;
        // Debug.Log("PlayVoice:language"+lang);
        AudioClip clip = voiceDict[id].GetClip(lang);
        // Debug.Log("PlayVoice:clip"+clip);
        source.clip = clip;
        source.Play();
    }
}

