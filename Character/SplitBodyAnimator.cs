namespace CharacterController;
using Godot;
using System;

public partial class SplitBodyAnimator : Node
{
    [Export]
    public Model model;

    [Export]
    public Skeleton3D skeleton;

    private AnimationPlayer torso_animator;
    private AnimationPlayer legsAnimator;

    private bool fullBodyMode = true;
    private float synchronization_delta = 0.01f;

    public override void _Ready()
    {
        torso_animator = GetNode<AnimationPlayer>("Torso");
        legsAnimator = GetNode<AnimationPlayer>("Legs");
    }

    public override void _Process(double delta)
    {
    }

    public void UpdateBodyAnimations()
    {
        UpdatePlaymode();
        SetAnimations();
    }

    public void UpdateLegsAnimation()
    {
        UpdatePlaymode();
        SetLegsAnimation(model.legs.currentLegsState.animation);
    }

    private void SetAnimations()
    {
        if (fullBodyMode)
        {
            SetLegsAnimation(model.currentState.animation);
            SetTorsoAnimation(model.currentState.animation);
            SynchronizeIfNeeded();
        }
        else
        {
            SetLegsAnimation(model.legs.currentLegsState.animation);
            SetTorsoAnimation(model.currentState.animation);
        }
    }

    private void SetLegsAnimation(string animation)
    {
        legsAnimator.Play(animation + "_legs");
    }

    private void SetTorsoAnimation(string animation)
    {
        torso_animator.Play(animation + "_torso");
    }

    private void SynchronizeIfNeeded()
    {
        if (Math.Abs(torso_animator.CurrentAnimationPosition - legsAnimator.CurrentAnimationPosition) > synchronization_delta)
        {
            torso_animator.Seek(legsAnimator.CurrentAnimationPosition);
        }
    }

    private void UpdatePlaymode()
    {
        if (model.currentState is TorsoPartialState)
        {
            fullBodyMode = false;
        }
        else
        {
            fullBodyMode = true;
        }
    }

    public void SetSpeedScale(float speed)
    {
        legsAnimator.SpeedScale = speed;
        torso_animator.SpeedScale = speed;
    }

    public void ResetTorsoAnimation()
    {
        torso_animator.Seek(0);
    }

    public void ResetLegsAnimation()
    {
        legsAnimator.Seek(0);
    }
}