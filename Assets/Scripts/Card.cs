using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum FlipState {FRONT, BACK};
    private GameManager gameManager;

    public int cardID;
    public FlipState cardSide;

    void Awake()
    {
        this.transform.Rotate(0, 0, 180, Space.Self);
        cardSide = FlipState.BACK;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    public void OnMouseDown()
    {
        if(!gameManager.isPunished)
        {
            if(cardSide == FlipState.BACK)
            {
                this.transform.Rotate(0, 0, -180, Space.Self);
                cardSide = FlipState.FRONT;
                gameManager.SendMessage("StoreCards", this);
            }
        }
    }

    public void FlipBack()
    {
        if(cardSide == FlipState.FRONT)
        {
            this.transform.Rotate(0, 0, -180, Space.Self);
            cardSide = FlipState.BACK;
        }
    }
}
