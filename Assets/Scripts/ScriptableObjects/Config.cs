using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/config")]
public class Config : ScriptableObject
{
    public Level level;
    public GameObject cardPrefab;
    [SerializeField]
    public List<Card> CardsOptions;


}