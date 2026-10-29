using System;
using System.Collections.Generic;
using UnityEngine;

public class D6Dice : IDice
{
    public int NumOfSides { get; set; }
    public float[] SideValues { get; set; }
    public bool IsUsed { get; set; }
    public Func<bool>[] SideEffects { get; set; }
    public List<int[]> MoveOffsets { get; set; }
    public D6Dice()
    {
        NumOfSides = 6;
        SideValues = new float[] { 1f, 2f, 3f, 4f, 5f, 6f};
        IsUsed = false;
        MoveOffsets = new List<int[]>();
        for (int i = -7; i <= 7; i++)
        {
            for (int q = -7; q <= 7; q++)
            {
                if (i == 0 && q == 0)
                    continue;
                if (i==q || i==-q || (i==0 && q==-1) || (i == 0 && q == 1) || (i == -1 && q == 0) || (i == 1 && q == 0))
                    MoveOffsets.Add(new int[] { i, q });
            }
        }
    }
    public float RollDice()
    {
        throw new System.NotImplementedException();
    }
}
