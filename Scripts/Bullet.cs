using Godot;

public partial class Bullet : Area2D
{
    [Export]
    private int bulletSpeed = 250;

    [Export]
    private int bulletRange = 250;

    private float traveledDistance = 0;

    public override void _Ready()
    {
        base._Ready();

        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 direction = Vector2.Right.Rotated(Rotation);
        Position += direction * bulletSpeed * (float)delta;

        traveledDistance += bulletSpeed * (float)delta;

        if (traveledDistance >= bulletRange)
        {
            QueueFree();
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        QueueFree();
    }
}
