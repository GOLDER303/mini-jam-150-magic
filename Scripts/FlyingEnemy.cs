using System;
using Godot;

public partial class FlyingEnemy : CharacterBody2D
{
    [Export]
    private float speed = 30.0f;

    [Export]
    private float attackRange = 130.0f;

    [Export]
    private Player player;

    [Export]
    private PackedScene bullet;

    private AnimationPlayer animationPlayer;
    private Sprite2D sprite2D;
    private Timer attackTimer;
    private Marker2D shootingPoint;

    public override void _Ready()
    {
        base._Ready();

        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        attackTimer = GetNode<Timer>("AttackTimer");
        shootingPoint = GetNode<Marker2D>("ShootingPoint");

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

        Velocity = velocity;
        MoveAndSlide();
    }

    private void OnAttackTimerTimeout()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (GlobalPosition.DistanceTo(player.GlobalPosition) > attackRange)
        {
            return;
        }

        Bullet newBullet = bullet.Instantiate() as Bullet;

        newBullet.AddToGroup("enemy_bullet");
        newBullet.GlobalPosition = shootingPoint.GlobalPosition;
        newBullet.LookAt(player.GlobalPosition);

        GetTree().Root.AddChild(newBullet);
    }
}
