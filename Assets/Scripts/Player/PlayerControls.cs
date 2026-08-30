using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    
    public float speed;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction attackAction;
    private InputAction pauseAction;
    private Rigidbody2D rb;
    private Vector2 moveValue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        moveAction = playerInput.actions.FindAction("Move");
        interactAction = playerInput.actions.FindAction("Interact");
        attackAction = playerInput.actions.FindAction("Attack");
        pauseAction = playerInput.actions.FindAction("Pause");

    }

    // Update is called once per frame
    void Update()
    {
        if (moveAction != null)
        {
            moveValue = moveAction.ReadValue<Vector2>();
            rb.position += moveValue*(speed*Time.deltaTime);
        }

        if (moveValue != Vector2.zero)
        {
            Vector3 scale = transform.localScale;
            if (moveValue.x < 0f)
                scale.x = -Mathf.Abs(scale.x);
            else if (moveValue.x > 0.01f)
                scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;

            transform.rotation = Quaternion.identity;
        }
    }
}
