using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed = 150.0f;

    [Export]
    public float JumpVelocity = -300.0f;

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
            if (!animationPlayer.CurrentAnimation.StartsWith("attack"))
            {
                animationPlayer.Stop();
            }
            velocity.Y += gravity * (float)delta;
        }

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        if (Input.IsActionJustPressed("attack1"))
        {
            animationPlayer.Play("attack1");
        }

        float direction = Input.GetAxis("move_left", "move_right");

        if (!(animationPlayer.CurrentAnimation.StartsWith("attack") && animationPlayer.IsPlaying()))
        {
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
        }
        else if (IsOnFloor() || direction == 0)
        {
            velocity.X = 0;
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}