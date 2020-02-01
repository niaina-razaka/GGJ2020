using System;
using UnityEngine;
[Serializable]
public class Player: MonoBehaviour
{
    public int Life { get; set; } = 4;
    public float Energy { get; set; } = 3;
    public string name { get; private set; } = "Hafah";
}
