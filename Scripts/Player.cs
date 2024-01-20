using Godot;

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    private AnimationPlayer animationPlayer;
    private Sprite2D sprite2D;

    public override void _Ready()
    {
        base._Ready();
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        sprite2D = GetNode<Sprite2D>("Sprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

        Vector2 velocity = Velocity;

        if (!IsOnFloor())
        {
            animationPlayer.Stop();
            velocity.Y += gravity * (float)delta;
        }

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        float direction = Input.GetAxis("move_left", "move_right");

        if (direction != 0)
        {
            velocity.X = direction * Speed;

            sprite2D.FlipH = velocity.X < 0;

            animationPlayer.Play("run");
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

            animationPlayer.Play("idle");
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
