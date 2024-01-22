using Godot;

public partial class FinalAltar : Node
{
    [Export]
    private GameManager gameManager;

    public override void _Ready()
    {
        base._Ready();

        HealthBar healthBar = GetNode<HealthBar>("HealthBar");
        healthBar.Death += OnDeath;
    }

    private void OnDeath()
    {
        gameManager.YouWon();
    }
}
