using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    private int score;
    private int comboCount;
    private int attempsCount;
    private List<CardBehavior> selectedCards = new List<CardBehavior>();

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI attempsText;


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
        attempsCount++;
        attempsText.text = "Attemps: " + attempsCount;
        if (selectedCards[0].GetTag() == selectedCards[1].GetTag())
        {
            comboCount++;
            score = score + comboCount;
            scoreText.text = "Score: " + score;

            selectedCards.ForEach(card => card.MarkAsSolved());
            selectedCards.Clear();
        }
        else
        {
            comboCount = 0;
            StartCoroutine(UnFlipSelectedCards());
        }
        comboText.text = "Combo: " + comboCount;

    }

    IEnumerator UnFlipSelectedCards()
    {
        yield return new WaitForSeconds(0.2f);
        selectedCards.ForEach(card => card.UnFlip());
        selectedCards.Clear();
    }



}
