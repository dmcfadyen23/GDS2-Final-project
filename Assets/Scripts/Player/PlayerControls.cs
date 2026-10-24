
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public float speed;

    [Header("Walking Sound")]
    [SerializeField] private AudioSource walkingAudioSource;
    [SerializeField] private AudioClip walkingSound;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction attackAction;
    private InputAction pauseAction;
    private Rigidbody2D rb;
    private Vector2 moveValue;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        moveAction = playerInput.actions.FindAction("Move");
        interactAction = playerInput.actions.FindAction("Interact");
        attackAction = playerInput.actions.FindAction("Attack");
        pauseAction = playerInput.actions.FindAction("Pause");

        // Set up the walking sound.
        if (walkingAudioSource != null)
        {
            walkingAudioSource.clip = walkingSound;
            walkingAudioSource.loop = true;
            walkingAudioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (moveAction != null)
        {
            moveValue = moveAction.ReadValue<Vector2>();
            rb.position += moveValue*(speed*Time.deltaTime);
        }

        // Keep the existing player-facing behaviour.
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

        // Play footsteps while moving, and stop when idle.
        bool isMoving = moveValue.sqrMagnitude > 0.01f;

        if (walkingAudioSource != null && walkingSound != null)
        {
            if (isMoving && !walkingAudioSource.isPlaying)
                walkingAudioSource.Play();
            else if (!isMoving && walkingAudioSource.isPlaying)
                walkingAudioSource.Stop();
        }
    }

    private void OnDisable()
    {
        if (walkingAudioSource != null)
            walkingAudioSource.Stop();
    }
}
