using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IHealthSystem, IScoreSystem
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rotationSpeed = 0.5f;

    private Rigidbody rb;
    private bool isGrounded;

    public float MaxHealth { get; private set; } = 30f;
    public float CurrentHealth { get; private set; }
    public int Score { get; private set; }

    public UnityEvent OnCollected = new UnityEvent();
    public UnityEvent OnDamaged = new UnityEvent();

    private IPlayerController controller;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        CurrentHealth = MaxHealth;
        Score = 0;

        controller = new KeyboardPlayerController(moveSpeed, rotationSpeed);
    }

    private void Update()
    {
        controller?.ApplyRotation(transform);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        controller?.ApplyMovement(rb);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                isGrounded = true;
                break;

            case "Collectible":
                Score += 10;
                OnCollected.Invoke();
                Destroy(collision.gameObject);
                break;

            case "Enemy":
                TakeDamage(10f);
                OnDamaged.Invoke();
                Destroy(collision.gameObject);
                break;
        }
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
