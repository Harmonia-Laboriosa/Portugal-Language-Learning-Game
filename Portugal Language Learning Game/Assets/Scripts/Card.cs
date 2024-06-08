using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Image cardImage;

    [SerializeField]
    private Sprite faceSprite, backSprite;

    private bool coroutineAllowed, isFacedUp;

    private void Start()
    {
        cardImage = GetComponent<Image>();
        cardImage.sprite = backSprite; // Start with the back sprite
        coroutineAllowed = true;
        isFacedUp = false;
    }

    public void OnCardClick()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(FlipCard());
        }
    }

    private IEnumerator FlipCard()
    {
        coroutineAllowed = false;

        float flipSpeed = 10f;
        float scaleX = transform.localScale.x;

        // First half of the flip
        while (scaleX > 0)
        {
            scaleX -= Time.deltaTime * flipSpeed;
            transform.localScale = new Vector3(scaleX, 1, 1);
            yield return null;
        }

        // Change the sprite at the halfway point
        cardImage.sprite = isFacedUp ? backSprite : faceSprite;

        // Second half of the flip
        while (scaleX < 1)
        {
            scaleX += Time.deltaTime * flipSpeed;
            transform.localScale = new Vector3(scaleX, 1, 1);
            yield return null;
        }

        // Ensure the final scale is exactly 1
        transform.localScale = new Vector3(1, 1, 1);

        isFacedUp = !isFacedUp;
        coroutineAllowed = true;
    }
}
