using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour {

    private static MoneySystem _instance;
    public int money;
    public int baseIncome = 15000;
    public int actualIncome;


    public static MoneySystem instance
    {
        get
        {
            if (_instance == null)
            {
                if (GameObject.Find("MoneySystem"))
                {
                    GameObject g = GameObject.Find("MoneySystem");
                    if (g.GetComponent<MoneySystem>())
                    {
                        _instance = g.GetComponent<MoneySystem>();
                    }
                    else
                    {
                        _instance = g.AddComponent<MoneySystem>();
                    }
                }
                else
                {
                    GameObject g = new GameObject();
                    g.name = "MoneySystem";
                    _instance = g.AddComponent<MoneySystem>();
                }
            }

            return _instance;
        }


        set
        {
            _instance = value;
        }
    }

    // Use this for initialization
    void Awake () {

        //Make sure the Gameobject is named MoneySystem.
        gameObject.name = "MoneySystem";
        _instance = this;

        //Load the saved money from last game
        //AddMoney(PlayerPrefs.GetInt("MoneySave", 0));

        //Update Money
        actualIncome = baseIncome;
        SaveMoney();
    }



    //while reality exists, save money every saveInterval.
    public void SaveMoney()
    {
            PlayerPrefs.SetInt("MoneySave", instance.money);
    }

    //Checks if you have enough money to buy item with cost, if you do buy it and return true. Otherwise, return false.
    public bool BuyItem(int cost)
    {
        if (instance.money >= cost)
        {
            instance.money -= cost;
            SaveMoney();
            return true;
        }
        else
        {
            return false;
        }
    }

    //Simply return the balance
    public static int GetMoney()
    {
        return instance.money;
    }

    //Add some money to the balance.
    public void AddMoney(int amount)
    {
        instance.money += amount;
        SaveMoney();
    }


}


