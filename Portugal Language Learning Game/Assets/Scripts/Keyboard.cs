using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Key keyPrefab;
    [SerializeField] private Key backSpacePrefab;

    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float widthPercent;
    [Range(0f, 1f)]
    [SerializeField] private float heigthPercent;
    [Range(0f, 1f)]
    [SerializeField] private float bottomOffset;


    [Header("Keyboard Lines")]
    [SerializeField] private KeyboardLine[] lines;

    [Header("Key Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float keyToLineRatio;
    [Range(0f, 1f)]
    [SerializeField] private float keyXSpacing;

    [Header("Events")]
    public Action<char> onKeyPressed;
    public Action onBackSpacePressed;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        CreateKeys();

        yield return null;

        UpdateRecTransform();
        
        //rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height/3);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRecTransform();
        PlaceKeys();
    }

    private void UpdateRecTransform()
    {
        float width = widthPercent * Screen.width;
        float height = heigthPercent * Screen.height;

        // Configuring the size of the keyboard container
        rectTransform.sizeDelta = new Vector2(width, height);

        //Configure the bottom offset
        Vector2 position;

        position.x = Screen.width/2;
        position.y = bottomOffset*Screen.height+height/2;

        rectTransform.position = position;

    }

    private void CreateKeys()
    {
        for(int i=0; i<lines.Length; i++)
        {
            for(int j=0; j < lines[i].keys.Length; j++)
            {
                char key = lines[i].keys[j];

                if(key=='.')
                {
                    Key keyInstance = Instantiate(backSpacePrefab, rectTransform);
                    keyInstance.GetButton().onClick.AddListener(() => BackSpaceKeyPressed());
                }
                else
                {
                    Key keyInstance = Instantiate(keyPrefab, rectTransform);
                    keyInstance.SetKey(key);

                    keyInstance.GetButton().onClick.AddListener(() => KeyPressed(key));
                }

            }
        }
    }

    private void PlaceKeys()
    {
        int lineCount = lines.Length;
        float lineHeight = rectTransform.rect.height/lineCount;
        float keyWidth = lineHeight * keyToLineRatio;
        float XSpacing = keyXSpacing * lineHeight;

        int currentKeyIndex = 0;

        for(int i=0; i<lineCount; i++)
        {

            bool containsBackSpace = lines[i].keys.Contains(".");

            float halfKeyCount = (float)lines[i].keys.Length / 2;

            if (containsBackSpace)
                halfKeyCount += 0.5f;



            float startX = rectTransform.position.x - (keyWidth+ XSpacing) * halfKeyCount + (keyWidth+ XSpacing) / 2;
            float lineY = rectTransform.position.y + rectTransform.rect.height / 2 - lineHeight / 2 - i * lineHeight;

            for(int j=0; j < lines[i].keys.Length; j++)
            {
                bool isBackSpaceKey = lines[i].keys[j] == '.';

                float keyX = startX + j * (keyWidth+ XSpacing);

                if(isBackSpaceKey)
                    keyX += keyWidth - XSpacing;

                Vector2 keyPosition = new Vector2(keyX, lineY);
                RectTransform keyRectTransform = rectTransform.GetChild(currentKeyIndex).GetComponent<RectTransform>();
                keyRectTransform.position = keyPosition;

                float thisKeyWidth = keyWidth;

                if (isBackSpaceKey)
                    thisKeyWidth = thisKeyWidth * 2;

                keyRectTransform.sizeDelta = new Vector2(keyWidth, keyWidth);
                currentKeyIndex++;
            }
        }

    }

    private void BackSpaceKeyPressed()
    {
        onBackSpacePressed?.Invoke();
    }

    private void KeyPressed(char key)
    {
        onKeyPressed?.Invoke(key);
    }
}

[System.Serializable]
public struct KeyboardLine
{
    public string keys;
}
