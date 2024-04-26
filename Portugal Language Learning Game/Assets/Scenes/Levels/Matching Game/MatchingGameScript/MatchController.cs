using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchController : MonoBehaviour
{
    [SerializeField] private Sprite[] bgImage;
    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>();
    private bool firstGuess;
    private bool secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    
    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;
    public GameObject EndGamePanel;
    private void Awake()
    {
        //puzzles = Resources.LoadAll<Sprite>("MSprites");
    }

    // Start is called before the first frame update
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count/2;
        EndGamePanel.SetActive(false);
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for(int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage[i];
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for(int i = 0;i <looper;i++)
        {
            if(index==looper)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(()=> PuzzlePick());
        }
    }

    public void PuzzlePick()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.tag;
        //Debug.Log("You clicked " + tag);
        if(!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;
            StartCoroutine(CheckIfThePuzzlesMatch());
        }

    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(0.25f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.25f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameFinished(); 
            //Debug.Log("Matched");
        }
        else
        {
            yield return new WaitForSeconds(0.25f);

            btns[firstGuessIndex].image.sprite = bgImage[firstGuessIndex];
            btns[secondGuessIndex].image.sprite = bgImage[secondGuessIndex];
        }
        yield return new WaitForSeconds(0.25f);
        firstGuess = secondGuess = false;


    }

    void CheckIfTheGameFinished()
    {
        countCorrectGuesses++;
        if(countCorrectGuesses==gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("It took you " + countGuesses+" guesses.");
            EndGamePanel.SetActive(true);
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

}
