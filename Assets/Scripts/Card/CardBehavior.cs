
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour
{
    Card card;
    public Sprite defaultSprite;
    private float flipValue = 0f;
    private Image spriteRenderer;

    public CustomEvent<CardBehavior> OnFlip = new CustomEvent<CardBehavior>("onFlip");
    void Awake()
    {
        spriteRenderer = GetComponent<Image>();
    }

    public void SetCardMetadata(Card metadata)
    {
        card = metadata;
        spriteRenderer.sprite = card.sprite;

    }
    public Card GetMetaData()
    {
        return card;
    }
    public string GetTag()
    {
        return card.tag;
    }
    public void OnClick()
    {
        OnFlip.Call(this);
        StartCoroutine(Flip());
    }

    IEnumerator Flip()
    {
        while (flipValue < 1)
        {
            flipValue += 0.05f;
            this.transform.localScale = new Vector3(flipValue, this.transform.localScale.y, this.transform.localScale.z);
            if (flipValue < 0.5)
            {
                spriteRenderer.sprite = card.sprite;
            }
            yield return null;
        }
    }


    public void UnFlip()
    {
        spriteRenderer.sprite = defaultSprite;
        flipValue = 0f;
    }

    public void MarkAsSolved()
    {
        var button = this.GetComponent<Button>();
        button.interactable = false;
        card.isResolved = true;
    }

}
