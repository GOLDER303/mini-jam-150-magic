using System;
using Godot;

public partial class GroundEnemy : CharacterBody2D
{
    [Export]
    private float speed = 30.0f;

    [Export]
    private float attackSpeed = 120.0f;

    [Export]
    private float attackRange = 50.0f;

    [Export]
    private Player player;

    private AnimationPlayer animationPlayer;
    private Sprite2D sprite2D;
    private Timer attackTimer;

    private bool isPreparingToAttack = false;
    private bool isAttacking = false;

    enum States
    {
        Moving,
        PreparingToAttack,
        Attacking
    }

    States currentState = States.Moving;

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
        int direction = -1;
        Vector2 velocity = Velocity;

        switch (currentState)
        {
            case States.Moving:
                if (player.Position.X < Position.X)
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

                if (playerDistance <= attackRange)
                {
                    currentState = States.PreparingToAttack;
                }
                break;

            case States.PreparingToAttack:
                velocity = Vector2.Zero;
                if (!animationPlayer.CurrentAnimation.Contains("attack"))
                {
                    animationPlayer.Play("attack1");
                }
                break;

            case States.Attacking:
                if (Scale.Y > 0)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }

                velocity.X = direction * attackSpeed;
                break;

            default:
                break;
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    // The function is triggered by the attack1 animation
    private void Attack()
    {
        currentState = States.Attacking;
        attackTimer.Start();
    }

    private void OnAttackTimerTimeout()
    {
        currentState = States.Moving;
        animationPlayer.Play("move");
    }
}
