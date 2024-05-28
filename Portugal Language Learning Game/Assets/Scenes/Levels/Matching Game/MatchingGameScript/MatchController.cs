using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public AudioSource audioSound;

    public AudioClip matchClip;
    public AudioClip notmatchClip;

    public string sceneName;

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
        Debug.Log("You clicked " + tag);

        int currentGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = currentGuessIndex;
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            Debug.Log(firstGuessPuzzle);
        }
        else if (!secondGuess && currentGuessIndex != firstGuessIndex) // Check if the same button is not clicked twice
        {
            secondGuess = true;
            secondGuessIndex = currentGuessIndex;
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;
            Debug.Log(secondGuessPuzzle);
            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(0.5f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            btns[firstGuessIndex].image.color = Color.green;
            btns[secondGuessIndex].image.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            audioSound.PlayOneShot(matchClip);
            CheckIfTheGameFinished();
        }
        else
        {
            btns[firstGuessIndex].image.color = Color.red;
            btns[secondGuessIndex].image.color = Color.red;
            audioSound.PlayOneShot(notmatchClip);
            yield return new WaitForSeconds(1f);
            btns[firstGuessIndex].image.color = new Color(1, 1, 1, 1); // Reset button color to original
            btns[secondGuessIndex].image.color = new Color(1, 1, 1, 1); // Reset button color to original
            
            btns[firstGuessIndex].image.sprite = bgImage[firstGuessIndex];
            btns[secondGuessIndex].image.sprite = bgImage[secondGuessIndex];
            
        }
        yield return new WaitForSeconds(0.5f);
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
            StartCoroutine("openFinalCutscene");
        }
    }

    IEnumerator openFinalCutscene()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(sceneName);

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
