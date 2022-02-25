using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Cards Setup
    public GameObject[] Cards;
    List<int> possibleIndex = new List<int> {0, 1, 2, 3, 4 ,5, 0 ,1 ,2 ,3 ,4 ,5};
    private int shuffleNum;

    private int cardBinary = 0;
    private Card cardOne;
    private Card cardTwo;
    private bool isCardsTheSame;
    public bool isPunished = false;
    private int CorrectCounter = 0;
	private float timer;
	public TextMeshProUGUI timerTxt;
	public GameObject winScreen;
    public TextMeshProUGUI finishTimeTxt;
    private bool isGameFinished = false;

    private void Start()
    {
        timer = 0;
        timerTxt.text = timer.ToString("0.00");
        winScreen.SetActive(false);

        float xStart = -9;
        float yStart = 7;
        float zStart = 2;

        for(int r = 0; r < 3; r++)
        {
            for(int c = 0; c < 4; c++)
            {
                shuffleNum = Random.Range(0, possibleIndex.Count);
                GameObject tempCard = Instantiate(Cards[possibleIndex[shuffleNum]],new Vector3(xStart, yStart, zStart), Quaternion.identity);
                possibleIndex.Remove(possibleIndex[shuffleNum]);
                xStart = xStart + 6;
            }
            xStart = -9;
            zStart = zStart -8;
        }
    }

    public void StoreCards(Card id)
    {
        if(cardBinary == 0)
        {
            cardOne = id;
            cardBinary++;
        }
        else if(cardBinary == 1)
        {
            cardTwo = id;
            InterpretCards(cardOne, cardTwo);
            cardBinary--;
        }
    }

    void InterpretCards(Card c1, Card c2)
    {
        if(c1.cardID == c2.cardID)
        {
            CorrectCounter++;
            cardOne = null;
            cardTwo = null;

            if(CorrectCounter >= 6)
            {
                isGameFinished = true;
            }
        }
        else if(c1.cardID != c2.cardID)
        {
            isPunished = true;
            Invoke("ResetCards", 2f);
        }
    }

    void ResetCards()
    {
        isPunished = false;
        cardOne.FlipBack();
        cardTwo.FlipBack();
        cardOne = null;
        cardTwo = null;
    }

    void FixedUpdate()
    {
        if(!isGameFinished)
        {
            timer += Time.deltaTime;
            timerTxt.text = timer.ToString("0.00");
        }
        else if(isGameFinished)
        {
            finishTimeTxt.text = "Time: " + timer.ToString("0.00");
            winScreen.SetActive(true);

            if(Input.GetButton("Jump"))
            {
                SceneManager.LoadScene("Game");
            }
            if(Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
        }
    }
}
