using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed = 150.0f;

    [Export]
    public float JumpVelocity = -300.0f;

    [Export]
    private GameManager gameManager;

    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        base._Ready();
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        HealthBar healthBar = GetNode<HealthBar>("HealthBar");
        healthBar.Death += OnDeath;
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

                if (velocity.X > 0)
                {
                    Scale = new Vector2(Scale.X, 1);
                    RotationDegrees = 0;
                }
                else
                {
                    Scale = new Vector2(Scale.X, -1);
                    RotationDegrees = 180;
                }

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

    private void OnDeath()
    {
        gameManager.GameOver();
    }
}
