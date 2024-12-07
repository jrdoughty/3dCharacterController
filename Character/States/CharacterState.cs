namespace CharacterController;

using System;
using System.Collections.Generic;
using Godot;

public partial class CharacterState : Node
{
    public Player player;
    public SplitBodyAnimator animator;
    public Skeleton3D skeleton;
	public HumanoidResources resources;
    public Combat combat;
    public StateDataRepository stateDataRepo;
    public HumanStates container;
    public AreaAwareness area_awareness;
    public Legs legs;

    [Export]
    public string animation;
    [Export]
    public string stateName;
    [Export]
    public int priority = 0;
    [Export]
    public string backendAnimation;
    [Export]
    public float trackingAngularSpeed = 10.0f;
    private float gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
    [Export]
    public float staminaCost = 0f;

    public List<Combo> combos = new List<Combo>();
    
    private double enterStateTime;
    private Vector3 initial_position;
    private float frameLength = 0.016f;

    private bool hasQueuedMove = false;
    private string queuedMove = "nonexistent queued move, drop error please";

    private bool hasForcedMove = false;
    private string forcedMove = "nonexistent forced move, drop error please";

    private float DURATION;


    public override void _Ready()
    {
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
    

    public void SetPlayer(Player p)
    {
        player = p;
    }
    public Dictionary<string,int> GetStatePrioritys()
    {
        return statePriority;
    }



    public virtual string CheckRelevance(InputPackage input)
    {
        if (AcceptsQueueing())
        {
            CheckCombos(input);
        }

        if (hasQueuedMove && TransitionsToQueued())
        {
            TryForceState(queuedMove);
            hasQueuedMove = false;
        }

        if (hasForcedMove)
        {
            hasForcedMove = false;
            return forcedMove;
        }

        return DefaultLifecycle(input);
    }

    private void CheckCombos(InputPackage input)
    {
        foreach (Combo combo in combos)
        {
            if (combo.IsTriggered(input) && resources.CanBePaid(container.GetStateByName(combo.triggeredState)))
            {
                hasQueuedMove = true;
                queuedMove = combo.triggeredState;
            }
        }
    }
    public string BestInputThatCanBePaid(InputPackage input)
    {
        input.inputActions.Sort((a, b) => container.GetStateByName(a).priority.CompareTo(container.GetStateByName(b).priority));
        foreach (string action in input.inputActions)
        {
            if (resources.CanBePaid(container.GetStateByName(action)))
            {
                if (container.GetStateByName(action) == this)
                {
                    return "okay";
                }
                else
                {
                    return action;
                }
            }
        }
        return "throwing because for some reason input.actions doesn't contain even idle";
    }

    public virtual void Update(InputPackage input, double delta)
    {
        if (TracksInputVector())
        {
            ProcessInputVector(input, delta);
        }
        Update(input, delta);
    }
    public void ProcessInputVector(InputPackage input, double delta)
    {
        Vector3 inputDirection = (player.cameraMount.Basis * new Vector3(-input.inputDirection.X, 0, -input.inputDirection.Y)).Normalized();
        Vector3 face_direction = player.Basis.Z;
        float angle = face_direction.SignedAngleTo(inputDirection, Vector3.Up);
        player.RotateY(Mathf.Clamp(angle, (float)(-trackingAngularSpeed * delta), (float)(trackingAngularSpeed * delta)));
    }

    public void UpdateResources(double delta)
    {
        resources.Update(delta);
    }

    public void MarkEnterState()
    {
        enterStateTime = Time.GetUnixTimeFromSystem();
    }

    public double GetProgress()
    {
        double now = Time.GetUnixTimeFromSystem();
        return now - enterStateTime;
    }

    public bool WorksLongerThan(float time)
    {
        return GetProgress() >= time;
    }

    public bool WorksLessThan(float time)
    {
        return GetProgress() < time;
    }

    public bool WorksBetween(double start, double finish)
    {
        double progress = GetProgress();
        return progress >= start && progress <= finish;
    }

    public bool TransitionsToQueued()
    {
        return stateDataRepo.GetTransitionsToQueued(backendAnimation, GetProgress());
    }

    public bool AcceptsQueueing()
    {
        return stateDataRepo.GetAcceptsQueueing(backendAnimation, GetProgress());
    }

    public bool TracksInputVector()
    {
        return stateDataRepo.TracksInputVector(backendAnimation, GetProgress());
    }
/*
    public float TimeTilUnlocking()
    {
        if (TracksInputVector())
        {
            return 0;
        }
        return stateDataRepo.TimeTilNextControllableFrame(backendAnimation, GetProgress());
    }
*/
    public bool IsVulnerable()
    {
        return stateDataRepo.GetVulnerable(backendAnimation, GetProgress());
    }

    public bool IsInterruptable()
    {
        return stateDataRepo.GetInterruptable(backendAnimation, GetProgress());
    }

    public bool IsParryable()
    {
        return stateDataRepo.GetParryable(backendAnimation, GetProgress());
    }

    public Vector3 GetRootPositionDelta(double delta_time)
    {
        return stateDataRepo.GetRootDeltaPos(backendAnimation, GetProgress(), delta_time);
    }

    public bool RightWeaponHurts()
    {
        return stateDataRepo.GetRightWeaponHurts(backendAnimation, GetProgress());
    }

    public string DefaultLifecycle(InputPackage input)
    {
        if (WorksLongerThan(DURATION))
        {
            return BestInputThatCanBePaid(input);
        }
        return "okay";
    }

    public void OnEnterState()
    {
        initial_position = player.GlobalPosition;
        resources.PayResourceCost(this);
        MarkEnterState();
        OnEnterStateInternal();
        animator.UpdateBodyAnimations();
    }

    protected virtual void OnEnterStateInternal()
    {
        // Override in derived classes
    }

    public void OnExitState()
    {
        OnExitStateInternal();
    }

    protected virtual void OnExitStateInternal()
    {
        // Override in derived classes
    }

    public void AssignCombos()
    {
        foreach (Node child in GetChildren())
        {
            if (child is Combo combo)
            {
                combos.Add(combo);
                combo.state = this;
            }
        }
    }

    public HitData FormHitData(Weapon weapon)
    {
        GD.Print("someone tries to get hit by default Move");
        return HitData.Blank();
    }

    public void ReactOnHit(HitData hit)
    {
        if (!IsVulnerable())
        {
            GD.Print("hit is here, but still the roll");
        }
        if (IsVulnerable())
        {
            resources.LoseHealth(hit.damage);
        }
        if (IsInterruptable())
        {
            if (hit.effects.ContainsKey("pushback") && (bool)hit.effects["pushback"])
            {
                area_awareness.lastPushbackVector = (Vector3)hit.effects["pushback_direction"];
                TryForceState("pushback");
            }
            else
            {
                TryForceState("staggered");
            }
        }
    }

    public void ReactOnParry(HitData hit)
    {
        TryForceState("parried");
    }

    public void TryForceState(string new_forcedMove)
    {
        if (!hasForcedMove)
        {
            hasForcedMove = true;
            forcedMove = new_forcedMove;
        }
        else if (container.GetStateByName(new_forcedMove).priority >= container.GetStateByName(forcedMove).priority)
        {
            forcedMove = new_forcedMove;
        }
    }
}
