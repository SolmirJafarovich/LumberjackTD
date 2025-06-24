using UnityEngine;



[System.Serializable]
public struct MinMaxInt
{
    public int Min;
    public int Max;

    public int GetRandomValue()
    {
        return Random.Range(Min, Max + 1);
    }
}