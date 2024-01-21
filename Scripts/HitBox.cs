using Godot;

public partial class HitBox : Area2D
{
    public HitBox()
    {
        CollisionLayer = 2;
        CollisionMask = 0;
    }
}
