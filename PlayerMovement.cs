using Godot;

public partial class Player : CharacterBody3D
{
	// Movement speed in meters per second
	[Export] public float Speed { get; set; } = 14f;
	// Gravity applied while in the air
	[Export] public float FallAcceleration { get; set; } = 75f;
	// Jump force
	[Export] public float JumpVelocity { get; set; } = 20f;

	private Vector3 _velocity = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 direction = Vector3.Zero;

		// Get movement input
		if (Input.IsActionPressed("move_right"))
			direction.X += 1.0f;
		if (Input.IsActionPressed("move_left"))
			direction.X -= 1.0f;
		if (Input.IsActionPressed("move_back"))
			direction.Z += 1.0f;
		if (Input.IsActionPressed("move_forward"))
			direction.Z -= 1.0f;

		// Normalize the direction to avoid diagonal speed increase
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
		}

		// Update horizontal velocity
		_velocity.X = direction.X * Speed;
		_velocity.Z = direction.Z * Speed;

		// Apply gravity
		if (!IsOnFloor())
		{
			_velocity.Y -= FallAcceleration * (float)delta;
		}

		// Jumping logic
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			_velocity.Y = JumpVelocity;
		}

		// Apply movement
		Velocity = _velocity;
		MoveAndSlide();
	}
	public void ResetToGround()
{
	var ground = GetNodeOrNull<Node3D>("GroundMesh"); // Adjust path
	if (ground != null)
	{
		GlobalTransform = new Transform3D(Basis.Identity, ground.GlobalTransform.Origin + new Vector3(0, 2, 0));
	}
}

}
