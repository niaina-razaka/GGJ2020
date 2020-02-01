using System;
using UnityEngine;
[Serializable]
public class Player: MonoBehaviour
{
    public int Life { get; set; } = 5;
    public float Energy { get; set; } = 3;
    public string name { get; private set; } = "Hafah";
}
