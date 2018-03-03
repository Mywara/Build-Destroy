﻿using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{

    public new string name;
    public string cost;
    public Sprite image;
    public GameObject blockPrefab;

}