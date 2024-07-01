using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CardList")]
public class CardList : ScriptableObject
{
    public GameObject prefab;
    public List<Texture2D> Textures;
    [SerializeField]
    public List<Card> CardsOptions;

    private void Generate()
    {
        CardsOptions.Clear();
        foreach (var texture in Textures)
        {
            Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture));
            if (data != null)
            {
                foreach (Object obj in data)
                {
                    if (obj.GetType() == typeof(Sprite))
                    {
                        Sprite sprite = obj as Sprite;
                        Card card = new Card();
                        card.tag = sprite.name;
                        card.sprite = sprite;
                        CardsOptions.Add(card);

                    }
                }
            }
        }

    }
}
