using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingGoal : Task.TaskGoal
{
    public string item;

    public override string GetDescription()
    {
        return $"Craft a {item}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<CraftingGameEvent>(OnCrafting);
    }

    private void OnCrafting(CraftingGameEvent eventInfo)
    {
        if (eventInfo.itemName == item)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
