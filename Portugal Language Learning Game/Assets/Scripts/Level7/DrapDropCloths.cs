using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DrapDropCloths : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;                 //reference to Canvas
    private RectTransform rectTransform;   //reference to RectTransform
    private CanvasGroup canvasGroup;       //reference to CanvasGroup
    private Vector2 originalPosition;      //Reference to the position it is at the start of the scene
    public bool isDraggable = true;        //isDraggable bool to check whether you can drag the gameobject 
    public bool isPlaceCorrect = false;    //isDraggable bool to check whether you can drag the gameobject 

    [SerializeField]
    private string tag;                    //tag of the gameobject where object will be placed


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // If isDraggable is false then return the function and cannot drag
        if (!isDraggable) return;

        // Make the draggable object slightly transparent
        canvasGroup.alpha = 0.75f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // If isDraggable is false then return the function and cannot drag
        if (!isDraggable) return;

        // Drag the object with the pointer
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // If isDraggable is false then return the function and cannot drag
        if (!isDraggable) return;

        // Make the draggable object opaque again
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (eventData.hovered.Count > 0)
        {
            foreach (GameObject hoveredObject in eventData.hovered)
            {
                if (hoveredObject.CompareTag(tag))
                {
                    // If dropped onto a slot, snap to its position
                    rectTransform.anchoredPosition = hoveredObject.GetComponent<RectTransform>().anchoredPosition;
                    RectTransform hoveredRectTransform = hoveredObject.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = hoveredRectTransform.sizeDelta;

                    // If dropped onto a slot, check if placement is correct
                    HumanSlot  itemSlot = hoveredObject.GetComponent<HumanSlot>();
                    if (itemSlot != null)
                    {
                        string placed_ObjectTag = itemSlot.Placedobjecttag;
                        isPlaceCorrect = string.Equals(placed_ObjectTag, gameObject.tag);
                    }

                    // If placement is correct, object is no longer draggable
                    isDraggable = !isPlaceCorrect;

                    return; // Exit the loop once a valid slot is found
                }
            }
        }

        // If not dropped onto a slot, return to original position
        rectTransform.anchoredPosition = originalPosition;

        // Reset draggable if placement is incorrect
        isDraggable = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void ResetToOriginalPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
        isDraggable = true;
        isPlaceCorrect = false;
    }
}
