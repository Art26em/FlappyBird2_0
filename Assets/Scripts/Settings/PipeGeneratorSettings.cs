using UnityEngine;

public class PipeGeneratorSettings
{
    public int PipesCount { get; private set; }
    public Pipes Template { get; private set; }
    public GameObject Container { get; private set; }

    public PipeGeneratorSettings(int pipesCount, Pipes template, GameObject container)
    {
        PipesCount = pipesCount;
        Template = template;
        Container = container;
    }
}