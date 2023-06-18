using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [SerializeField] private SuspensionManager suspensionManager;
    [SerializeField] private EngineManager engineManager;

    public float speed;
    public float rotationSpeed = 10f;
    public float rotationDampening = 10f; // Adjust this value to control the dampening effect
    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    public Rigidbody2D rb;

    #region Movement Variables
    private float movement = 0f;
    private float currentRotationVelocity = 0f; // Tracks the current rotational velocity
    private float rotation = 0f;
    private float rotationInput = 0f;
    private float rotationInputThreshold;
    private float clampedRotation;
    private float targetRotation;
    #endregion


    private GameManager gameManager;

    private const float MaxRotation = 10f;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogError("Gyroscope not supported on this device.");
        }
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    private void Update()
    {
        HandleInput();
        gameManager.DistanceCovered();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            rotationInputThreshold = 0.8f;

            if (touch.position.x < Screen.width * 0.25f)
            {
                movement = speed; // Move backward
                rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
                AudioManager.Instance.SetCarMoving(true);
            }
            else if (touch.position.x > Screen.width * 0.75f)
            {
                movement = -speed; // Move forward
                rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
                AudioManager.Instance.SetCarMoving(true);
            }
            else
            {
                movement = 0f; // No movement
                rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
                AudioManager.Instance.SetCarMoving(false);
            }
        }
        else
        {
            rotationInputThreshold = 0.5f;
            rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
            movement = 0f; // No movement
            AudioManager.Instance.SetCarMoving(false);
        }
        clampedRotation = Mathf.Clamp(rotationInput, -MaxRotation, MaxRotation);
        targetRotation = clampedRotation * rotationSpeed;
        rotation = Mathf.SmoothDamp(rotation, targetRotation, ref currentRotationVelocity, 1f / rotationDampening);
    }

    private void FixedUpdate()
    {
        if (movement == 0 || GameManager.fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            gameManager.FuelManagement();

            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = backWheel.motor.maxMotorTorque };
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }

        if (GameManager.fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
            CurrencyManager.Instance.AddBaseCurrency(GameManager.distanceCovered);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        rb.AddTorque(-rotation * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        gameManager.OnTriggerEnter2D(collider);
    }
}
