using System;

[Serializable]
public class Emoji
{
    /// <summary>
    /// For example: \U0001F63C for 😼
    /// </summary>
    public string code;

    public override string ToString() => code;
}