using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Ivar
public class PlayerController : MonoBehaviour {
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private Rigidbody _ballRigidbody;
    [SerializeField] private RandomMaterial randomMaterial;
    [SerializeField] private ModelController _modelController;
    private List<FlipperController> _leftFlipperController;
    private List<FlipperController> _rightFlipperController;

    private Vector3 _respawnPosition;

    public bool IsHoldingInteraction = false;

    // Input Actions Varibals
    private Vector2 _movementInput = Vector2.zero;

    private EnumPlayerTag _tag = EnumPlayerTag.None;
    private NavigationManager _navigationManager;



    private void Awake() {
        SceneManager.sceneLoaded -= OnSceneLoaded; // to prevent duplicate subscriptions when Awake() is called multiple times.
        SceneManager.sceneLoaded += OnSceneLoaded; // gets called every time a scene is loaded

        //this._respawnPosition = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }
    private void Start() {
        if (_ballRigidbody == null) Debug.LogWarning("Ball Rigidbody is not assigned! Please assign it in the inspector.");
        AssigneFlippers();
        AssigneNavigationManager();
    }

    private void AssigneNavigationManager() {
        this._tag = _modelController.GetPlayerTag();
        this._navigationManager = GameManager.Instance.PlayerNavigations[this._tag];

        if (this._navigationManager != null) {
            this._navigationManager.SetSkinController(_modelController.SkinController);

            PlayerInput playerInput = this.gameObject.GetComponent<PlayerInput>();
            this._navigationManager.RegisterPlayerInput(playerInput.actions.FindAction("Move"), playerInput.actions.FindAction("Interact"));
        }
    }

    //public void NavigationOnMovePerformed(InputAction.CallbackContext context) {
    //    if (this._navigationManager != null) {
    //        this._navigationManager.OnMovePerformed(context);
    //    }
    //}

    //public void NavigationOnSelectPerformed(InputAction.CallbackContext context) {
    //    if (this._navigationManager != null) {
    //        this._navigationManager.OnSelectPerformed(context);
    //    }
    //}

    void FixedUpdate() {
        _ballRigidbody.AddTorque(new Vector3(this._movementInput.y, 0, -this._movementInput.x) * _playerSpeed, ForceMode.Force);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name.StartsWith("Pro") || scene.name.StartsWith("Level")) {
            AssigneFlippers();
        } else {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


    private void AssigneFlippers() {
        GameObject[] leftFlippers = GameObject.FindGameObjectsWithTag(tag: "LeftFlipper");
        GameObject[] rightFlippers = GameObject.FindGameObjectsWithTag(tag: "RightFlipper");
        _leftFlipperController = new List<FlipperController>();
        _rightFlipperController = new List<FlipperController>();

        string playerLayerString = LayerMask.LayerToName(this.gameObject.layer);
        string playerSuffix = playerLayerString.Substring(playerLayerString.Length - 2);

        if (leftFlippers.Length == 0 || rightFlippers.Length == 0) {
            Debug.LogWarning("No flippers found! Please add flippers to the scene and tag them");
            return;
        }

        this._leftFlipperController = ProcessFlippers(leftFlippers, playerSuffix);
        this._rightFlipperController = ProcessFlippers(rightFlippers, playerSuffix);
    }

    public List<FlipperController> ProcessFlippers(GameObject[] flippers, string playerSuffix) {
        List<FlipperController> processedFlippers = new List<FlipperController>();
        foreach (GameObject flipper in flippers) {
            string flipperLayerString = LayerMask.LayerToName(flipper.layer);
            string flipperSuffix = flipperLayerString.Substring(flipperLayerString.Length - 2);

            if (flipperSuffix == playerSuffix) {
                FlipperController flipperController = flipper.GetComponent<FlipperController>();
                flipperController.SetFlipperColor(randomMaterial.AssignedMaterialColor);
                processedFlippers.Add(flipperController);
            }
        }
        return processedFlippers;
    }

    public void Respawn() {
        DisableGravity();
        _ballRigidbody.gameObject.transform.position = this._respawnPosition;
        StartCoroutine(ReEnableGravityCoroutine(0.5f));

    }

    // Input Actions Methods
    public void OnMove(InputAction.CallbackContext context) => this._movementInput = context.ReadValue<Vector2>();
    public void OnPauseAction(InputAction.CallbackContext context) {
        if (context.performed) {
            GameManager.Instance.OnPauseAction();
        }
    }
    public void OnFlipperLeft(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            AllFlipLeft(true);
        } else if (context.phase == InputActionPhase.Canceled) {
            AllFlipLeft(false);
        }
    }
    public void OnFlipperRight(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            AllFlipRight(true);
        } else if (context.phase == InputActionPhase.Canceled) {
            AllFlipRight(false);
        }
    }
    public void OnHoldInteraction(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            this.IsHoldingInteraction = true;
        } else if (context.phase == InputActionPhase.Canceled) {
            this.IsHoldingInteraction = false;
        }
    }

    private void AllFlipLeft(bool isFlipping) {
        foreach (FlipperController flipper in _leftFlipperController) {
            flipper.IsFlipping = isFlipping;
        }
    }

    private void AllFlipRight(bool isFlipping) {
        foreach (FlipperController flipper in _rightFlipperController) {
            flipper.IsFlipping = isFlipping;
        }
    }

    public void DisableGravity() {
        _ballRigidbody.isKinematic = true;
    }
    public void EnableGravity() {
        _ballRigidbody.isKinematic = false;
    }

    private IEnumerator ReEnableGravityCoroutine(float delayInSeconds) {
        yield return new WaitForSeconds(delayInSeconds);
        EnableGravity();
    }

    public void MovePlayer(Vector3 newPosition, bool reEnableGravity = true) {
        DisableGravity();
        //this.gameObject.transform.parent.transform.position = newPosition;
        //this.transform.GetChild(0).transform.position = this.transform.GetChild(0).transform.InverseTransformPoint(newPosition);
        //this.transform.GetChild(0).transform.position = this.transform.GetChild(0).transform.position;

        Transform child = this.transform.GetChild(0);
        child.SetParent(null); // Detach from parent
        child.position = newPosition; // Move freely
        child.SetParent(this.transform); // Reattach
        if (reEnableGravity) StartCoroutine(ReEnableGravityCoroutine(0.1f));
    }
}
