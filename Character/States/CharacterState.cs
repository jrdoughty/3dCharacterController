namespace CharacterController;

using System;
using System.Collections.Generic;
using Godot;

public partial class CharacterState : Node
{
    protected Player player;
    private string animation;
    public int staminaCost = 0;
    public string stateName;

    public override void _Ready()
    {
        player = GetParent<Model>().GetParent<Player>();
    }
    static public Dictionary<string,int> statePriority = new Dictionary<string, int>
    {
        {"Idle", 0},
        {"Walk", 1},
        {"Run", 2},
        {"Jump", 3},
        {"attack", 4},
        {"hurt", 5},
        {"die", 6}
    };
    public void TryForceMove(String state)
    {
        //if(statePriority[state] > statePriority[animation])
        {
            //player.SwitchState(state);
        }
    }
    public Dictionary<string,int> GetStatePrioritys()
    {
        return statePriority;
    }

    public int PrioritySort(string actionA, string actionB)
    {
        if(statePriority[actionA] > statePriority[actionB])
            return 1;
        else if(statePriority[actionA] < statePriority[actionB])
            return -1;
        else
            return 0;
    }

    public virtual string CheckRelevance(InputPackage input)
    {
        return "";
    }

    public virtual void Update(InputPackage input, double delta)
    {
        
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}
