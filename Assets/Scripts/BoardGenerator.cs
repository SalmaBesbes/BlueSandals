using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public enum Level
{
    easy,
    Medium,
    Hard
}


public class Layout
{
    public int row;
    public int column;
    public int pairCount { get; }

    public Layout(int row, int col)
    {
        this.row = row;
        this.column = col;
        this.pairCount = row * col / 2;
    }
}

public class BoardGenerator : MonoBehaviour
{

    public CardList config;
    public RectTransform cardContainer;
    public GameObject cardPrefab;

    public Level level = Level.easy;

    public List<GameObject> cardList;



    private GridLayoutGroup gridLayoutGroup;
    // Start is called before the first frame update
    void Start()
    {
        gridLayoutGroup = cardContainer.GetComponent<GridLayoutGroup>();
        Generate();
    }

    void Generate()
    {
        var layout = GetLayout();
        var cardOptions = PickCards(layout.pairCount);

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = layout.row;

        float width = cardContainer.rect.width;
        float height = cardContainer.rect.height;

        float cellWidth = (width - (gridLayoutGroup.padding.left + gridLayoutGroup.padding.right) - (gridLayoutGroup.spacing.x * (layout.column - 1))) / layout.column;
        float cellHeight = (height - (gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom) - (gridLayoutGroup.spacing.y * (gridLayoutGroup.constraintCount - 1))) / gridLayoutGroup.constraintCount;

        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);


        for (int i = 0; i < layout.pairCount; i++)
        {
            var info = cardOptions[i];

            for (int c = 0; c < 2; c++)
            {
                var card = Instantiate(cardPrefab, this.transform);
                var cardBehavior = card.GetComponent<CardBehavior>();
                cardBehavior.SetCardMetadata(info);
                cardList.Add(card);
            }
        }

        System.Random random = new System.Random();
        cardList = cardList.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].transform.SetSiblingIndex(i);
        }

    }

    private List<Card> PickCards(int numberOfElements)
    {
        System.Random random = new System.Random();
        return config.CardsOptions.OrderBy(x => random.Next()).Distinct().Take(numberOfElements).ToList();
    }

    private Layout GetLayout()
    {
        switch (level)
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
}
