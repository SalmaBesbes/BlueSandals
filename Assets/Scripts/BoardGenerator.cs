using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardGenerator : MonoBehaviour
{
    public RectTransform cardContainer;

    private List<GameObject> instanciatedCards = new List<GameObject>();

    private GridLayoutGroup gridLayoutGroup;
    void Start()
    {
        gridLayoutGroup = cardContainer.GetComponent<GridLayoutGroup>();
        GameManager.Instance.RegisterForSingleOnEventOccured((this, "InitializeGame"), Generate);
    }

    void Generate()
    {
        var layout = GameManager.Instance.GetLayout();

        ConfigureCellsSize(layout);

        var pairCount = layout.GetPairCount();
        var cardOptions = GameManager.Instance.PickRandomUniqueCards(pairCount);
        var cardPrefab = GameManager.Instance.GetCardPrefab();

        for (int i = 0; i < pairCount; i++)
        {
            var info = cardOptions[i];

            for (int c = 0; c < 2; c++)
            {
                var card = Instantiate(cardPrefab, this.transform);
                card.name = info.tag;
                var cardBehavior = card.GetComponent<CardBehavior>();
                cardBehavior.SetCardMetadata(info);
                instanciatedCards.Add(card);
            }
        }

        var random = new System.Random();
        for (int i = instanciatedCards.Count - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);
            var temp = instanciatedCards[i];
            instanciatedCards[i] = instanciatedCards[randomIndex];
            instanciatedCards[randomIndex] = temp;
        }

        for (int i = 0; i < instanciatedCards.Count; i++)
        {
            instanciatedCards[i].transform.SetSiblingIndex(i);
        }

        GameManager.Instance.SetCardsList(instanciatedCards);
        GameManager.Instance.TriggerEvent("CardsGotGenerated");
    }

    private void ConfigureCellsSize(Layout layout)
    {

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = layout.row;

        float width = cardContainer.rect.width;

        float cellSize = (width -
        (gridLayoutGroup.padding.left + gridLayoutGroup.padding.right) -
        (gridLayoutGroup.spacing.x * (layout.column - 1)))
        / layout.column;

        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
    }

}
