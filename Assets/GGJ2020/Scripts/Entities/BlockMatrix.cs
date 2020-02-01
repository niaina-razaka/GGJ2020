﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockMatrix
{
    public List<int[]>[] Blocks { get; set; } = new List<int[]>[]
    {
        new List<int[]>()
        {
            new int[]{ 1, 1, 1, 1 },
            new int[]{ 0, 1, 1, 0 },
            new int[]{ 0, 0, 1, 0 }
        },
        new List<int[]>()
        {
            new int[]{ 1, 1, 1, 1, 1 },
            new int[]{ 0, 1, 1, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0 },
            new int[]{ 0, 1, 1, 1, 0 }
        }
    };
}