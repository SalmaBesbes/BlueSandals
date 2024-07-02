using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardGenerator : MonoBehaviour
{
    public RectTransform cardContainer;

    private List<CardBehavior> instanciatedCards = new List<CardBehavior>();

    private GridLayoutGroup gridLayoutGroup;
    void Start()
    {
        gridLayoutGroup = cardContainer.GetComponent<GridLayoutGroup>();
        GameManager.Instance.OnLoad.Register(this, (savedData) => GenerateFromLoadedData(savedData));
        GameManager.Instance.RegisterForSingleOnEventOccured((this, "InitializeGame"), Generate);
    }


    void Generate()
    {
        var layout = GameManager.Instance.GetLayout();
        SaveData.currentSave.layout = layout;

        ConfigureCellsSize(layout);

        var pairCount = layout.GetPairCount();
        var cardOptions = GameManager.Instance.PickRandomUniqueCards(pairCount);
        var cardPrefab = GameManager.Instance.GetCardPrefab();

        for (int i = 0; i < pairCount; i++)
        {
            var info = cardOptions[i];

            for (int c = 0; c < 2; c++)
            {
                var card = Instantiate(cardPrefab, cardContainer.transform);
                card.name = info.tag;
                info.isResolved = false;
                var cardBehavior = card.GetComponent<CardBehavior>();
                cardBehavior.SetCardMetadata(info);
                instanciatedCards.Add(cardBehavior);
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
        StartCoroutine(UnFlipCards());

        GameManager.Instance.SetCardsList(instanciatedCards);
        GameManager.Instance.TriggerEvent("CardsGotGenerated");
    }

    void GenerateFromLoadedData(SaveData savedData)
    {
        var cardPrefab = GameManager.Instance.GetCardPrefab();
        ConfigureCellsSize(savedData.layout);


        instanciatedCards.Clear();
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {
            Destroy(cardContainer.GetChild(i).gameObject);
        }


        savedData.cards.ForEach(info =>
        {
            var card = Instantiate(cardPrefab, cardContainer.transform);
            card.name = info.tag;
            var cardBehavior = card.GetComponent<CardBehavior>();
            cardBehavior.SetCardMetadata(info);
            instanciatedCards.Add(cardBehavior);
        });
        StartCoroutine(UnFlipCards());

        GameManager.Instance.SetCardsList(instanciatedCards);
        GameManager.Instance.TriggerEvent("CardsGotGenerated");
    }

    IEnumerator UnFlipCards()
    {
        yield return new WaitForSeconds(0.5f);
        instanciatedCards.ForEach(card =>
        {
            if (card.GetMetaData().isResolved) card.MarkAsSolved();
            else card.UnFlip();
        });

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
