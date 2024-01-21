using System;
using Godot;

public partial class FlyingEnemy : CharacterBody2D
{
    [Export]
    private float speed = 30.0f;

    [Export]
    private float attackRange = 50.0f;

    [Export]
    private Player player;

    private AnimationPlayer animationPlayer;
    private Sprite2D sprite2D;
    private Timer attackTimer;

    public override void _Ready()
    {
        base._Ready();

        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        attackTimer = GetNode<Timer>("AttackTimer");

        attackTimer.Timeout += OnAttackTimerTimeout;

        animationPlayer.Play("move");
    }

    public override void _PhysicsProcess(double delta)
    {
        int direction;
        Vector2 velocity = Velocity;

        if (player.Position.X - Position.X < 0)
        {
            Scale = new Vector2(Scale.X, 1);
            RotationDegrees = 0;
            direction = -1;
        }
        else
        {
            Scale = new Vector2(Scale.X, -1);
            RotationDegrees = 180;
            direction = 1;
        }

        velocity.X = direction * speed;

        float playerDistance = Mathf.Abs(player.Position.X - Position.X);

        Velocity = velocity;
        MoveAndSlide();
    }

    private void OnAttackTimerTimeout() { }
}
