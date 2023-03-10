using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string EventDescription;
}

public class GatheringGameEvent : GameEvent
{
    public string itemName;
    public GatheringGameEvent(string name)
    {
        this.itemName = name;
    }
}

public class CraftingGameEvent : GameEvent
{
    public string itemName;
    public CraftingGameEvent(string name)
    {
        this.itemName = name;
    }
}