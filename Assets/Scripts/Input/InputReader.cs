using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/Input Reader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
{

    public Vector2 MoveInput { get; private set; }

    private PlayerInput playerInput;

    public bool JumpPressed { get; private set; }
    public bool GlidePressed { get; private set; }

    public bool BlockPressed { get; private set; }

    public bool AttackPressed { get; private set; }

    public bool IsJumpPressed => playerInput.Player.Jump.ReadValue<float>() > 0;

    public bool IsGlidePressed => playerInput.Player.Glide.ReadValue<float>() > 0;

    public bool IsBlockPressed => playerInput.Player.Block.ReadValue<float>() > 0;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.Player.SetCallbacks(this);
        }
        playerInput.Player.Enable();
    }

    private void OnDisable() => playerInput.Player.Disable();

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpPressed = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerCombat.instance.Attack();
        }
    }

    public void OnGlide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GlidePressed = true;
        }

    }

    public void OnBlock(InputAction.CallbackContext context)
    {

    }

    public bool ConsumeJumpInput()
    {
        if (JumpPressed == true)
        {
            JumpPressed = false;
            return true;
        }
        return false;
    }

    public bool ConsumeAttackInput()
    {
        if (AttackPressed)
        {
            AttackPressed = false;
            return true;
        }

        return false;
    }
}