using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    private int score;
    private int comboCount;
    private int attempsCount;
    private int resolvedCount;
    private List<CardBehavior> selectedCards = new List<CardBehavior>();

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI attempsText;


    void Start()
    {
        GameManager.Instance.RegisterForOnEventOccured((this, "CardsGotGenerated"), Init);
        GameManager.Instance.OnLoad.Register(this, (savedData) =>
        {
            score = savedData.Score;
            comboCount = savedData.Combo;
            attempsCount = savedData.Attemps;
            resolvedCount = savedData.resolvedCount;
            attempsText.text = "Attemps: " + attempsCount;
            scoreText.text = "Score: " + score;
            comboText.text = "Combo: " + comboCount;

        });

    }

    void Init()
    {
        GameManager.Instance.GetCardsList().ForEach(card =>
                {
                    card.OnFlip.Register(this, (card) =>
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
            GameManager.Instance.playCorrectSound();

            resolvedCount++;
            comboCount++;
            score = score + comboCount;
            scoreText.text = "Score: " + score;

            selectedCards.ForEach(card => card.MarkAsSolved());
            selectedCards.Clear();

            var pairCount = GameManager.Instance.GetLayout().GetPairCount();
            if (resolvedCount == pairCount)
            {
                GameManager.Instance.playGameOverSound();
                resolvedCount = 0;
                GameManager.Instance.TriggerEvent("GameOver");
            };
        }
        else
        {
            GameManager.Instance.playWrongSound();

            comboCount = 0;
            StartCoroutine(UnFlipSelectedCards());
        }
        comboText.text = "Combo: " + comboCount;

        SaveData.currentSave.Attemps = attempsCount;
        SaveData.currentSave.Combo = comboCount;
        SaveData.currentSave.Score = score;
        SaveData.currentSave.resolvedCount = resolvedCount;

    }

    IEnumerator UnFlipSelectedCards()
    {
        yield return new WaitForSeconds(0.2f);
        selectedCards.ForEach(card => card.UnFlip());
        selectedCards.Clear();
    }

}
