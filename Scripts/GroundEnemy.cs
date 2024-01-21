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

        if (!isPreparingToAttack && !isAttacking)
        {
            if (player.Position.X < Position.X)
            {
                sprite2D.FlipH = false;
                direction = -1;
            }
            else
            {
                sprite2D.FlipH = true;
                direction = 1;
            }

            velocity.X = direction * speed;
        }
        else if (isAttacking)
        {
            velocity.X = direction * attackSpeed;
        }

        Velocity = velocity;
        MoveAndSlide();

        float playerDistance = Mathf.Abs(player.Position.X - Position.X);

        if (playerDistance <= attackRange)
        {
            PrepareToAttack();
        }
    }

    private void PrepareToAttack()
    {
        if (!isAttacking)
        {
            Velocity = Vector2.Zero;
            isPreparingToAttack = true;
            animationPlayer.Play("attack1");
            attackTimer.Start();
        }
    }

    private void Attack()
    {
        isAttacking = true;
    }

    private void OnAttackTimerTimeout()
    {
        isAttacking = false;
        isPreparingToAttack = false;
        animationPlayer.Play("move");
    }
}
