using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Layout(int row, int col)
    {
        this.row = row;
        this.column = col;
    }
}

public class BoardGenerator : MonoBehaviour
{

    public RectTransform cardContainer;
    public GameObject cardPrefab;

    public Level level = Level.easy;

    public List<GameObject> cardList;


    private GridLayoutGroup gridLayoutGroup;
    // Start is called before the first frame update
    void Start()
    {
        gridLayoutGroup = cardContainer.GetComponent<GridLayoutGroup>();
    }

    void Generate()
    {
        EmptyGrid();
        var layout = GetLayout();
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = layout.row;

        float width = cardContainer.rect.width;
        float height = cardContainer.rect.height;

        float cellWidth = (width - (gridLayoutGroup.padding.left + gridLayoutGroup.padding.right) - (gridLayoutGroup.spacing.x * (layout.column - 1))) / layout.column;
        float cellHeight = (height - (gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom) - (gridLayoutGroup.spacing.y * (gridLayoutGroup.constraintCount - 1))) / gridLayoutGroup.constraintCount;

        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);

        for (int r = 0; r < layout.row; r++)
        {
            for (int c = 0; c < layout.column; c++)
            {
                var card = Instantiate(cardPrefab, this.transform);
                cardList.Add(card);
            }
        }

    }
    public void EmptyGrid()
    {
        foreach (var card in cardList)
        {
            Destroy(card);
        }
        cardList.Clear();
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
