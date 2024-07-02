using System.Collections.Generic;

public class SaveData
{

    private static SaveData _currentSave;

    public static SaveData currentSave
    {
        get
        {
            if (_currentSave == null)
            {
                _currentSave = new SaveData();
            }
            return _currentSave;
        }
    }

    public int resolvedCount;
    public int Score;
    public int Attemps;
    public int Combo;
    public Level level;
    public Layout layout;
    public List<Card> cards = new List<Card>();

}
