using Godot;

public partial class HurtBox : Area2D
{
    [Export]
    private HealthBar healthBar;

    [Export]
    private string[] arrayList;

    public HurtBox()
    {
        CollisionLayer = 0;
        CollisionMask = 2;
    }

    public override void _Ready()
    {
        base._Ready();
        Monitoring = true;
        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area2D)
    {
        bool shouldBeHurt = false;

        foreach (var ele in arrayList)
        {
            if (area2D.Owner.IsInGroup(ele))
            {
                shouldBeHurt = true;
                break;
            }
        }

        if (!shouldBeHurt)
        {
            return;
        }

        healthBar.TakeDamage(1);
    }
}
