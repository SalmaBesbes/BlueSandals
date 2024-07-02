using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gameplay : MonoBehaviour
{

    public List<CardBehavior> selectedCards;

    public EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject)
        {

            Debug.Log(eventSystem.currentSelectedGameObject);
            Debug.Log(selectedCards.Count);
            var card = eventSystem.currentSelectedGameObject.GetComponent<CardBehavior>();
            selectedCards.Add(card);
            eventSystem.SetSelectedGameObject(null);

            if (selectedCards.Count > 1)
            {
                Debug.Log(selectedCards[0].GetTag() == card.GetTag());

                if (selectedCards[0].GetTag() == card.GetTag())
                {
                    selectedCards[0].MarkAsSolved();
                    card.MarkAsSolved();
                }
                else
                {
                    card.UnFlip();
                    selectedCards[0].UnFlip();
                    selectedCards.Clear();
                }
            }
        }

    }
}
