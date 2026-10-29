using System;
using System.Collections.Generic;
using UnityEngine;

public class D2Dice : IDice
{
    public int NumOfSides { get; set; }
    public float[] SideValues { get; set; }
    public bool IsUsed { get; set; }
    public Func<bool>[] SideEffects { get; set; }
    public List<int[]> MoveOffsets { get; set; }
    public D2Dice()
    {
        NumOfSides = 2;
        SideValues = new float[] { 1f, 2f };
        IsUsed = false;
        MoveOffsets = new List<int[]>();
        for (int i = -1; i <= 1; i++)
        {
            for (int q = -1; q <= 1; q++)
            {
                if (i == 0 && q == 0)
                    continue;
                MoveOffsets.Add(new int[]{i,q});
            }
        }
    }
    public float RollDice()
    {
        throw new System.NotImplementedException();
    }
}
