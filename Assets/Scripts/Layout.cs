using System;

public enum Level
{
    easy,
    Medium,
    Hard
}

[Serializable]

public class Layout
{
    public int row;
    public int column;

    public Layout(int row, int col)
    {
        this.row = row;
        this.column = col;
    }

    public int GetPairCount()
    {
        return this.row * this.column / 2;
    }
}
