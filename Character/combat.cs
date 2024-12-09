namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Combat : Node
{
    Model model;
    static Dictionary<string,int> inputsPriority = new Dictionary<string, int>{
        {"light_attack_pressed" , 1},
        {"heavy_attack_pressed" , 2}
    };
    public override void _Ready()
    {
        model = GetParent<Model>();
    }
    
    public InputPackage Contextualize(InputPackage input)
    {
        TranslateInput(input);
        FilterWithResources(input);
        return input;
    }
    public void FilterWithResources(InputPackage input)
    {
        if(model.resources.statuses.Contains("fatique"))
        {
            input.inputActions.Remove("sprint");
        }
    }
    public void TranslateInput(InputPackage input)
    {
        if(input.combatActions.Count > 0)
        {
            input.combatActions.Sort(CombatActionPrioritySort);
            String bestAction = input.combatActions[0];
            String translatedStateName = model.activeWeapon.basicAttacks[bestAction];
            input.inputActions.Add(translatedStateName);
        }
    }

    static int CombatActionPrioritySort(string a, string b)
    {
        if(inputsPriority[a] > inputsPriority[b])
            return 1;
        else if(inputsPriority[a] < inputsPriority[b])
            return -1;
        else
            return 0;
    }
}
