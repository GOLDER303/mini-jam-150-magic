using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class HealthBar : HBoxContainer
{
    [Signal]
    public delegate void DeathEventHandler();

    private List<ColorRect> healthRects = new();
    private int currentHealth;
    private int maxHealth;

    public override void _Ready()
    {
        base._Ready();

        Node[] healthRectsNodes = GetChildren().Where(ch => ch is ColorRect).ToArray();

        foreach (var healthRectNode in healthRectsNodes)
        {
            healthRects.Add((ColorRect)healthRectNode);
        }

        maxHealth = healthRects.Count;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        for (int i = currentHealth; i < maxHealth; i++)
        {
            healthRects[i].Hide();
        }

        if (currentHealth <= 0)
        {
            EmitSignal("Death");

            GD.Print($"Death {Owner.Name}");
        }
    }
}
