using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains player info, scripts GameManager | Enemy | BuildManager have reference to PlayerStats
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    
    //Variable for money in the begining
    public int startMoney;
    public static int Rounds;
    

    public static int Lives;
    public int startLives = 20;

    
    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }
}
