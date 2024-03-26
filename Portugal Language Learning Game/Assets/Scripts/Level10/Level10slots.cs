using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Level10slots : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public string Placedobjecttag;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            string droppedObjectTag = eventData.pointerDrag.tag;

            // Print the tag of the dropped object
            Debug.Log("Tag of dropped object: " + droppedObjectTag);

            // Move the dropped object to the slot
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
