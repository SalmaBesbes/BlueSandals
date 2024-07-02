using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    public int score;
    public int comboCount;
    private List<CardBehavior> selectedCards = new List<CardBehavior>();
    
    void Start()
    {
        GameManager.Instance.RegisterForSingleOnEventOccured((this, "CardsGotGenerated"), Init);
    }

    void Init()
    {
        GameManager.Instance.GetCardsList().ForEach(card =>
                {
                    var behavior = card.GetComponent<CardBehavior>();
                    behavior.OnFlip.Register(this, (card) =>
                    {
                        selectedCards.Add(card);
                        if (selectedCards.Count > 1) CheckSelectedCombination();
                    });
                });
    }


    void CheckSelectedCombination()
    {
        if (selectedCards[0].GetTag() == selectedCards[1].GetTag())
        {
            comboCount++;
            score = score + comboCount;

            selectedCards.ForEach(card => card.MarkAsSolved());
            selectedCards.Clear();
        }
        else
        {
            comboCount = 0;
            StartCoroutine(UnFlipSelectedCards());
        }
    }

    IEnumerator UnFlipSelectedCards()
    {
        yield return new WaitForSeconds(0.2f);
        selectedCards.ForEach(card => card.UnFlip());
        selectedCards.Clear();
    }



}
