using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Level3ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public string Placedobjecttag;
    public Level3Manager level3Manager;
    //public string tag;
    public AudioClip clipGarage; 

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            level3Manager = FindObjectOfType<Level3Manager>();
            // Get the tag of the dropped object
            string droppedObjectTag = eventData.pointerDrag.tag;

            // Print the tag of the dropped object
            Debug.Log("Tag of dropped object: " + droppedObjectTag);
            if(eventData.pointerDrag.tag==Placedobjecttag)
            {
                level3Manager.Increasescore();
                level3Manager.audios[2].PlayOneShot(clipGarage, 0.15f);

            }
            // Move the dropped object to the slot
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            StartCoroutine(ActivateNextQuestionPanel());

            //eventData.pointerDrag.gameObject.SetActive(false);
        }
    }

    private IEnumerator ActivateNextQuestionPanel()
    {
        yield return new WaitForSeconds(0.5f);

        level3Manager.NextQuestion();
    }

}
