using Godot;

public partial class HurtBox : Area2D
{
    [Export]
    private HealthBar healthBar;

    [Export]
    private string[] damagedByGroups;

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
        var hitterGroups = area2D.GetGroups();

        bool shouldBeHurt = false;

        foreach (var damagedByGroup in damagedByGroups)
        {
            if (
                hitterGroups.Contains(damagedByGroup)
                || (area2D.Owner != null && area2D.Owner.IsInGroup(damagedByGroup))
            )
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
