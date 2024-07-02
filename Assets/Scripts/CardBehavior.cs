
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour
{
    Card card;
    private float flipValue = 0f;
    private Image spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<Image>();
    }

    public void SetCardMetadata(Card metadata)
    {
        card = metadata;
        card.id = new Guid().ToString();
    }
    public string GetTag()
    {
        return card.tag;
    }
    public string Id()
    {
        return card.id;
    }

    public void OnClick()
    {
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
        spriteRenderer.sprite = null;
        flipValue = 0f;
    }

    public void MarkAsSolved()
    {
        var button = this.GetComponent<Button>();
        button.interactable = false;
    }

}
