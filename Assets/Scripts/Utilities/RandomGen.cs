using UnityEngine;

public class RandomGen
{
    private int min;
    private int max;

  public RandomGen (int min, int max)
    {
        this.min = min;
        this.max = max;
    }


    public int GetNbr()
    {
        return Random.Range(min, max);  //TODO change to better randomizer
    }

}