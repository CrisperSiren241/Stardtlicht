using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IDataPersistenceManager
{
    public CharacterController controller;
    private bool isInitialPositionSet = false;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity = 0.1f;
    public Animator animator;
    public Transform cam;
    public float walkSpeed = 1f;
    public float runSpeed = 2f;
    public float currentSpeed;
    bool isRunning;

    public float speed = 0.5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float velocity = 0f;

    // Переменные для прыжка и гравитации
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    private Vector3 playerVelocity;

    // Переменные для проверки касания с землей
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    public float jumpHorizontalMultiplier = 1.5f;
    private bool isJumping = false;
    public bool isDialogueActive = false;
    public float rollSpeed = 2f; // Скорость переката
    public float rollDuration = 0.5f; // Длительность переката
    public bool isRolling = false; // Флаг переката
    CharacterStats stats;
    public float runJumpMultiplier = 2.0f; // Добавим новый множитель для прыжка при беге
    public Transform PlayerPosition;
    static PlayerMovement instance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloading;
    }

    private void OnSceneUnloading(Scene current)
    {
        // Сохраните данные перед сменой сцены
        SaveData(DataPersistenceManager.instance.GetGameData());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isInitialPositionSet = false;
        SetInitialPosition();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Character in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (stats.isDead)
        {
            controller.enabled = false;
            return;
        }

        if (isRolling)
        {
            return;
        }

        Vector3 playerPosition = transform.position;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
            isJumping = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Q) && direction.magnitude >= 0.1f && isGrounded && !isRolling)
        {
            StartCoroutine(Roll(direction));
            return;
        }

        if (direction.magnitude >= 0.1f)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            currentSpeed = isRunning ? runSpeed : walkSpeed;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float currentJumpMultiplier = isRunning ? runJumpMultiplier : jumpHorizontalMultiplier;
            float currentMoveSpeed = isJumping ? Mathf.Min(currentSpeed * currentJumpMultiplier, runSpeed * runJumpMultiplier) : currentSpeed;
            controller.Move(moveDir.normalized * currentMoveSpeed * Time.deltaTime);

            velocity = Mathf.Lerp(velocity, isRunning ? 1.0f : 0.5f, acceleration * Time.deltaTime);
        }
        else
        {
            velocity = Mathf.Lerp(velocity, 0f, deceleration * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("Velocity", velocity);
    }

    private IEnumerator Roll(Vector3 inputDirection)
    {
        isRolling = true;

        animator.SetTrigger("Roll");

        Vector3 rollDirection;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
            rollDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            rollDirection = cam.forward;
            rollDirection.y = 0;
            rollDirection.Normalize();
        }

        float elapsedTime = 0f;
        while (elapsedTime < rollDuration)
        {
            controller.Move(rollDirection * rollSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
    }

    public void LoadData(GameData data)
    {
        if (controller != null)
        {
            Vector3 loadedPosition = new Vector3(data.PlayerPosX, data.PlayerPosY, data.PlayerPosZ);
            controller.enabled = false;
            transform.position = loadedPosition;
            controller.enabled = true;
        }
        else
        {
            Debug.LogWarning("Controller is null while loading data.");
        }
    }

    public void SaveData(GameData data)
    {
        if (controller != null)
        {
            data.PlayerPosX = transform.position.x;
            data.PlayerPosY = transform.position.y;
            data.PlayerPosZ = transform.position.z;

            data.Level = SceneManager.GetActiveScene().name;
        }
        else
        {
            Debug.LogWarning("Controller is null while saving data.");
        }
    }

    private void SetInitialPosition()
    {
        if (isInitialPositionSet) return;

        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene);

        // Set initial position based on scene
        switch (currentScene)
        {
            case "Level1":
                transform.position = new Vector3(-27.1f, 0.2f, -34.2f);
                Debug.Log("Position set for Level1");
                break;
            case "Level2":
                transform.position = new Vector3(125.9f, 12.7f, 178.3f);
                Debug.Log("Position set for Level2");
                break;
        }

        Debug.Log("Player position: " + transform.position);

        isInitialPositionSet = true;
    }
}
