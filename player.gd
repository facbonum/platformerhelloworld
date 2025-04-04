extends RigidBody3D

const FORCE = 1200.0  # Force applied to movement
const MAX_SPEED = 10.0  # Max movement speed
const GRAVITY = -9.8   # Optional, if you want gravity to affect movement

var input_direction = Vector3.ZERO

func _ready() -> void:
	pass

func _process(delta: float) -> void:
	# Reset direction vector each frame
	input_direction = Vector3.ZERO
	
	# Movement input detection
	if Input.is_action_pressed("move_forward"):
		input_direction += Vector3.FORWARD
	if Input.is_action_pressed("move_back"):
		input_direction += Vector3.BACK
	if Input.is_action_pressed("move_right"):
		input_direction += Vector3.RIGHT
	if Input.is_action_pressed("move_left"):
		input_direction += Vector3.LEFT

	# Normalize direction to prevent faster diagonal movement
	if input_direction != Vector3.ZERO:
		input_direction = input_direction.normalized()

	# Set linear_velocity directly to simulate force
	linear_velocity.x = input_direction.x * FORCE
	linear_velocity.z = input_direction.z * FORCE
	linear_velocity.y += GRAVITY * delta  # Apply gravity

	# Limit speed to prevent excessive acceleration
	if linear_velocity.length() > MAX_SPEED:
		linear_velocity = linear_velocity.normalized() * MAX_SPEED
