using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [Header("Input Settings")]
    public bool enableMobile = true;
    public FloatingJoystick joystick;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    } // Awake

    private void Update()
    {
        Vector2 input = Vector2.zero;

        if (enableMobile && joystick != null)
        {
            input = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        else
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        Vector3 moveDirection = new Vector3(input.x, 0.0f, input.y).normalized;
        playerMovement.Move(moveDirection);
    } // Update

} // PlayerController
