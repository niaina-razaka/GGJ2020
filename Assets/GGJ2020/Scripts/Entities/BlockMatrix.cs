﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockMatrix
{
    public List<int[]>[] Blocks { get; set; } = new List<int[]>[]
    {
        new List<int[]>()
        {
            new int[]{ 1, 1, 0, 1, 1, 1, 0, 1, 1 },
            new int[]{ 0, 0, 0, 0, 1, 1, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 5, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        },
        new List<int[]>()
        {
            new int[]{ 1, 1, 0, 0, 0, 0, 0, 0, 0 },
            new int[]{ 0, 1, 0, 1, 1, 0, 1, 1, 1 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 5, 0 }
        },
        new List<int[]>()
        {
            new int[]{ 1, 1, 0, 1, 1, 1, 1, 1, 1 },
            new int[]{ 1, 0, 0, 0, 0, 0, 5, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[]{ 1, 1, 0, 1, 1, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[]{ 0, 0, 1, 0, 0, 0, 0, 0, 0 }
        },
        new List<int[]>()
        {
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            new int[]{ 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            new int[]{ 0, 1, 0, 0, 0, 1, 1, 0, 0 }
        },
          new List<int[]>()
        {
            new int[]{ 1, 1, 1, 1, 0, 0, 1, 1, 0 , 0 , 0 , 1 , 1 , 1 , 1 , 1 , 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 , 1 , 1 , 0 , 0 , 5 , 0 , 0 , 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0 , 0 , 0 , 1 , 1 , 1 , 0 , 1 }
        },
        new List<int[]>()
        {
            new int[]{ 1, 1, 1, 0, 0, 0, 0, 1, 1 , 1 , 1 , 1 , 1 , 1 , 1 , 0 , 0 , 1 , 1},
            new int[]{ 0, 5, 0, 0, 1, 1, 0, 0, 0 , 1 , 1 , 1 , 0 , 5 , 0 , 0 , 0 , 0 , 0},
            new int[]{ 0, 0, 0, 0, 0, 0, 1, 1, 0 , 0 , 5 , 0 , 0 , 1 , 1 , 1 , 0 , 0, 0 },
            new int[]{ 0, 1, 1, 0, 0, 0, 0, 0, 0 , 0 , 0 , 0 , 0 , 0 , 5 , 0 , 0 , 0 , 0},
            new int[]{ 1, 0, 0, 0, 1, 1, 0, 0, 0 , 1 , 1 , 1 , 0 , 1 , 1 , 0 , 1 , 1 , 1}
        },
        new List<int[]>()
        {
            new int[]{ 1, 1, 1, -2, -2, 1, 1, 1, 1 },
            new int[]{ 0, 5, 0, 0, 0, 0, 0, 5, 0 },
            new int[]{ 0, 0, 0, -2, -2, 0, 0, 0, 0 }
        },
        new List<int[]>()
        {
            new int[]{ 1, 1, 0, 0, 1, 1, -2, -1, -1, 1, 1, 0, 0, 0, 0, -2, 1 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0 }
        } 
    };
}