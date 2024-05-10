/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragDropLevel8 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;                 //reference to Canvas
    private RectTransform rectTransform;   //reference to RectTransform
    private CanvasGroup canvasGroup;       //reference to CanvasGroup
    private Vector2 originalPosition;      //Reference to the position it is at the start of the scene
    public bool isDraggable = true;        //isDraggable bool to check whether you can drag the gameobject 
    public bool isPlaceCorrect = false;        //isDraggable bool to check whether you can drag the gameobject 

    [SerializeField]
    private string stag;                    //tag of the gameobject where object will be placed


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if isDraggable is false than return the function and cannot drag
        if (!isDraggable) return;
        //make the draggable object little transparent
        canvasGroup.alpha = 0.75f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if isDraggable is false than return the function and cannot drag
        if (!isDraggable) return;
        //You can drag the object and it should drag with pointer
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if isDraggable is false than return the function and cannot drag
        if (!isDraggable) return;

        //make the draggable object opaque again
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (eventData.hovered.Count > 0)
        {
            foreach (GameObject hoveredObject in eventData.hovered)
            {
                if (hoveredObject.CompareTag(stag))
                {
                    // If dropped onto a slot, snap to its position
                    rectTransform.anchoredPosition = hoveredObject.GetComponent<RectTransform>().anchoredPosition;
                    RectTransform hoveredRectTransform = hoveredObject.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = hoveredRectTransform.sizeDelta;
                    //make is draggable true

                    // If dropped onto a slot, get the Placedobjecttag from the ItemSlot component
                    IngridentSlot itemSlot = hoveredObject.GetComponent<IngridentSlot>();
                    if (itemSlot != null)
                    {
                        // Get the Placedobjecttag from the ItemSlot component
                        string placed_ObjectTag = itemSlot.Placedobjecttag;
                        // Do something with the placedObjectTag

                        // Check if the placed object tag matches the tag of this object
                        if (string.Equals(placed_ObjectTag, gameObject.tag))
                        {
                            isPlaceCorrect = true;
                            //Debug.Log("Object placed correctly!");
                        }
                        else
                        {
                            isPlaceCorrect = false;
                            //Debug.Log("Object placed incorrectly!");
                        }
                    }

                    isDraggable = false;
                    return; // Exit the loop once a valid slot is found
                }
            }
        }
        // If not dropped onto a slot, return to original position
        rectTransform.anchoredPosition = originalPosition;
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
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragDropLevel8 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;                 // Reference to Canvas
    private RectTransform rectTransform;   // Reference to RectTransform
    private CanvasGroup canvasGroup;       // Reference to CanvasGroup
    private Vector2 originalPosition;      // Reference to the position it is at the start of the scene
    public bool isDraggable = true;        // IsDraggable bool to check whether you can drag the gameobject 
    public bool isPlaceCorrect = false;    // IsPlaceCorrect bool to check whether the object is placed correctly
    [SerializeField]
    private string stag;                    // Tag of the gameobject where object will be placed

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
        // You can drag the object and it should drag with pointer
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
                if (hoveredObject.CompareTag(stag))
                {
                    // If dropped onto a slot, snap to its position
                    if(hoveredObject.gameObject.GetComponentInChildren<VerticalLayoutGroup>())
                    {
                        RectTransform hoveredRectTransform = hoveredObject.GetComponent<RectTransform>();
                        rectTransform.SetParent(hoveredRectTransform); // Set draggable object's parent to the slot
                        rectTransform.anchoredPosition = Vector2.zero; // Reset position relative to the slot
                        
                    }
                    else
                    {
                        rectTransform.anchoredPosition = hoveredObject.GetComponent<RectTransform>().anchoredPosition;
                        RectTransform hoveredRectTransform = hoveredObject.GetComponent<RectTransform>();
                        rectTransform.sizeDelta = hoveredRectTransform.sizeDelta;
                    }

                    // If dropped onto a slot, get the Placedobjecttag from the ItemSlot component
                    IngridentSlot itemSlot = hoveredObject.GetComponent<IngridentSlot>();
                    if (itemSlot != null)
                    {
                        // Get the Placedobjecttag from the ItemSlot component
                        string placed_ObjectTag = itemSlot.Placedobjecttag;
                        // Do something with the placedObjectTag

                        
                        // Check if the placed object tag matches the tag of this object
                        if (string.Equals(placed_ObjectTag, gameObject.tag))
                        {
                            isPlaceCorrect = true;
                            // Debug.Log("Object placed correctly!");
                        }
                        /*
                        else if(string.Equals(GetComponentInParent<IngridentSlot>().Placedobjecttag,gameObject.tag))
                        {

                        }
                        */
                        else
                        {
                            isPlaceCorrect = false;
                            //Debug.Log("Object placed incorrectly!");
                        }
                    }

                    isDraggable = false;
                    return; // Exit the loop once a valid slot is found
                }
            }
        }
        // If not dropped onto a slot, return to original position
        //rectTransform.SetParent(canvas.transform); // Reset parent to canvas
        rectTransform.anchoredPosition = originalPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("OnPointerDown");
    }

    public void ResetToOriginalPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
        isDraggable = true;
        isPlaceCorrect = false;
    }
}
