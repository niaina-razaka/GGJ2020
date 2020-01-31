using System;

[Serializable]
public class Player
{
    public int Life { get; set; } = 5;
    public float Energy { get; set; } = 3;
    public string name { get; private set; } = "Hafah";
}
