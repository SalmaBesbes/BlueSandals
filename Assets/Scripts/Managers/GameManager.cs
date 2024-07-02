

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : EventManager
{
    [SerializeField]
    private Config config;
    public static GameManager Instance { get; private set; }

    private List<CardBehavior> cards;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        TriggerEvent("InitializeGame");
    }

    public List<Card> GetCardsOptions()
    {
        return config.CardsOptions;
    }

    public GameObject GetCardPrefab()
    {
        return config.cardPrefab;
    }

    public List<Card> PickRandomUniqueCards(int number)
    {
        System.Random random = new System.Random();
        return config.CardsOptions.OrderBy(x => random.Next()).Distinct().Take(number).ToList();
    }


    public Level GetSelectedLevel()
    {
        return config.level;
    }

    public void SetCurrentLevel(Level level)
    {
        config.level = level;
    }

    public Layout GetLayout()
    {
        switch (config.level)
        {
            case Level.easy:
                return new Layout(2, 2);
            case Level.Medium:
                return new Layout(2, 3);
            case Level.Hard:
                return new Layout(5, 6);
            default:
                return new Layout(2, 2);
        }
    }

    public void SetCardsList(List<CardBehavior> list)
    {
        cards = list;
    }

    public List<CardBehavior> GetCardsList()
    {
        return cards;
    }
}