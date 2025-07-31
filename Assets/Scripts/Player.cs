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

    private IPlayerController controller;

    public float MaxHealth { get; private set; } = 30f;
    public float CurrentHealth { get; private set; } = 30f;
    public int Score { get; private set; } = 0;

    public UnityEvent OnCollected = new UnityEvent();
    public UnityEvent OnDamaged = new UnityEvent();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        controller = new KeyboardPlayerController(moveSpeed, jumpForce, rotationSpeed);
    }

    private void Update()
    {
        controller?.ProcessInput(rb, transform, ref isGrounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                isGrounded = true;
                break;

            case "Collectible":
                AddScore(10);
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

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
    }
}
