using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level9Manager : MonoBehaviour
{
    public RectTransform[] tiles; // Array to hold the tiles
    public RectTransform emptyTile; // Reference to the empty tile
    public float slideSpeed = 5f; // Speed of sliding

    private bool isMoving; // Flag to check if a tile is currently moving

    void Start()
    {
        // Set the empty tile initially
        emptyTile = tiles[tiles.Length - 1];
    }

    void Update()
    {
        // Check for player input and move tiles accordingly
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveTile(emptyTile.anchoredPosition + Vector2.right * emptyTile.sizeDelta.x);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveTile(emptyTile.anchoredPosition + Vector2.left * emptyTile.sizeDelta.x);
            }
        }
    }

    void MoveTile(Vector2 targetPosition)
    {
        // Find the tile to move based on the target position
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Vector2.Distance(tiles[i].anchoredPosition, targetPosition) < 0.1f)
            {
                // Move the tile to the empty tile's position
                StartCoroutine(SlideTile(tiles[i], emptyTile.anchoredPosition));
                emptyTile.anchoredPosition = tiles[i].anchoredPosition;
                return;
            }
        }
    }

    IEnumerator SlideTile(RectTransform tile, Vector2 targetPosition)
    {
        isMoving = true;
        while (Vector2.Distance(tile.anchoredPosition, targetPosition) > 0.1f)
        {
            tile.anchoredPosition = Vector2.MoveTowards(tile.anchoredPosition, targetPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }
}