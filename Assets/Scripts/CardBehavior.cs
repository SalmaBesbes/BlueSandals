
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
    void Start()
    {
        spriteRenderer.sprite = card.sprite;
    }

    public void SetCardMetadata(Card metadata)
    {
        card = metadata;

    }

    // Update is called once per frame
    void Update()
    {
        if (flipValue < 1)
        {
            flipValue += 0.05f;
            this.transform.localScale = new Vector3(flipValue, this.transform.localScale.y, this.transform.localScale.z);

        }
        if (flipValue < 0.5)
        {
            spriteRenderer.sprite = card.sprite;
        }
    }
}
