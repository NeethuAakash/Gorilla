

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WUThroughBill : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject character;
    public GameObject btnSales;

    public List<TimedAction> timedActions;

    void Awake()
    {
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
}

