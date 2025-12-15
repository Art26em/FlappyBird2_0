using UnityEngine;

public class CoinGeneratorSettings
{
    public int CoinsCount { get; private set; }
    public Coin Template { get; private set; }
    public GameObject Container { get; private set; }

    public CoinGeneratorSettings(int coinsCount, Coin template, GameObject container)
    {
        CoinsCount = coinsCount;
        Template = template;
        Container = container;
    }
}        

