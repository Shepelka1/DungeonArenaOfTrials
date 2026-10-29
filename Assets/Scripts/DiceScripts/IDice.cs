using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDice
{
    public int NumOfSides { get; }
    public float[] SideValues { get; set; }
    public bool IsUsed { get; set; }
    public Func<bool>[] SideEffects { get; set; }
    public List<int[]> MoveOffsets { get; set; }
    public float RollDice();
}
