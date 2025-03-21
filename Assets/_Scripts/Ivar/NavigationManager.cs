using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Ivar 
[DefaultExecutionOrder(-1)] // Runs before scripts with default execution order (0)
public class NavigationManager : MonoBehaviour {

    // Input Actions
    private InputAction _moveInput;
    private InputAction _selectInput;

    [SerializeField] private Vector2Int _startPosition = Vector2Int.zero;
    [field: SerializeField] public Vector2Int _currentPosition { get; private set; }
    [SerializeField] private bool IsLobbyNavigation = false;

    private Dictionary<Vector2Int, NavigationController> navigationGrid = new Dictionary<Vector2Int, NavigationController>();
    public bool LockedNavigation = false;

    private float _moveCooldown = 0.15f;
    private float _lastMoveTime = 0f;

    private float _selectCooldown = 0.2f;
    private float _lastSelectTime = 0f;

    [SerializeField] private EnumPlayerTag _AssaigneSpecificPlayer = EnumPlayerTag.None;
    private SkinController _skinController;

    private void Start() {

        if (_AssaigneSpecificPlayer == EnumPlayerTag.None) {
            // Assaigning Global Input Actions
            this._moveInput = InputSystem.actions.FindAction("Move");
            this._selectInput = InputSystem.actions.FindAction("Interact");

            StartListening();

        } else {
            // Player Specific Navigation
            if (_AssaigneSpecificPlayer != EnumPlayerTag.None) {
                GameManager.Instance.PlayerNavigations[_AssaigneSpecificPlayer] = this;
            }
        }

        // Grid Setup
        this._currentPosition = this._startPosition;
        if (navigationGrid.ContainsKey(this._currentPosition)) {
            this.navigationGrid[this._currentPosition].NavigatedEvent.Invoke();
        }
    }

    public Color GetPlayerColor() {
        if (this._skinController != null) {
            return this._skinController.GetColor();
        }
        return Color.black;
    }

    public void RegisterObject(Vector2Int gridPosition, NavigationController controller) {
        this.navigationGrid[gridPosition] = controller;
    }

    public void RegisterPlayerInput(InputAction moveAction, InputAction selectAction) {
        this._moveInput = moveAction;
        this._selectInput = selectAction;
        StartListening();
    }

    public NavigationController GetNavigationController(Vector2Int position) {
        if (navigationGrid.ContainsKey(position)) {
            return this.navigationGrid[position];
        }
        return null;
    }

    public void SetSkinController(SkinController skinController) {
        this._skinController = skinController;

        //this.navigationGrid[Helper.LobbyButtonCoords.Next].SelectedEvent.AddListener(() => this._skinController.NextOption());
        //this.navigationGrid[Helper.LobbyButtonCoords.Previous].SelectedEvent.AddListener(() => this._skinController.PreviosOption());
        //this.navigationGrid[Helper.LobbyButtonCoords.Start].SelectedEvent.AddListener(() => this._skinController.StartButton());
        //this.navigationGrid[Helper.LobbyButtonCoords.Exit].SelectedEvent.AddListener(() => {
        //    if (GameManager.Instance.Players.Count > 1) {
        //        GameManager.Instance.DeletePlayer(this._AssaigneSpecificPlayer);
        //    } else {
        //        GameManager.Instance.MainMenu();
        //    }
        //});
    }

    #region "Input Actions"

    private void OnDisable() {
        StopListening();
    }
    public void StartListening() {
        _moveInput.Enable();
        _moveInput.performed += OnMovePerformed;
        //_moveInput.canceled += OnMovePerformed;

        _selectInput.Enable();
        _selectInput.performed += OnSelectPerformed;
        //_selectInput.canceled += OnSelectPerformed;
    }


    public void StopListening() {
        //_moveInput.performed -= OnMovePerformed;
        ////_moveInput.canceled -= OnMovePerformed;
        //_moveInput.Disable();

        //_selectInput.performed -= OnSelectPerformed;
        ////_selectInput.canceled -= OnSelectPerformed;
        //_selectInput.Disable();
        this.LockedNavigation = true;
    }
    private void OnMovePerformed(InputAction.CallbackContext context) {
        if (this.LockedNavigation) return; // Not allowed to move
        if (context.phase == InputActionPhase.Canceled) return; // Ignore up key
        if (Time.time - this._lastMoveTime < this._moveCooldown) return; // Cooldown check

        Vector2 move = context.ReadValue<Vector2>();
        Vector2Int direction = Vector2Int.zero;

        if (move.x > 0) direction = Vector2Int.right;
        else if (move.x < 0) direction = Vector2Int.left;
        else if (move.y > 0) direction = Vector2Int.up;
        else if (move.y < 0) direction = Vector2Int.down;

        if (direction != Vector2Int.zero) {
            MoveSelection(direction);
            this._lastMoveTime = Time.time;
        }
    }

    private void MoveSelection(Vector2Int direction) {
        Vector2Int newPosition = this._currentPosition + direction; // New grid position

        if (navigationGrid.ContainsKey(newPosition)) { // Check if the new position is valid
            this.navigationGrid[this._currentPosition].DeselectedEvent.Invoke(); // Deselect the current object
            this._currentPosition = newPosition; // Register the new position
            this.navigationGrid[this._currentPosition].NavigatedEvent.Invoke(); // Select the new object
        }
    }
    private void OnSelectPerformed(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Canceled) return; // Ignore up key
        if (this.LockedNavigation) return; // Not allowed to select
        if (Time.time - _lastSelectTime < _selectCooldown) return; // Cooldown check

        if (this.navigationGrid.ContainsKey(this._currentPosition)) {
            NavigationController navigationController = this.navigationGrid[this._currentPosition];
            navigationController.SelectedEvent.Invoke();

            if (this.IsLobbyNavigation) {
                if (this._currentPosition == Helper.LobbyButtonCoords.Next) {
                    this._skinController.NextOption();
                    navigationController.UpdateOutlineMaterialColor();
                } else if (this._currentPosition == Helper.LobbyButtonCoords.Previous) {
                    this._skinController.PreviosOption();
                    navigationController.UpdateOutlineMaterialColor();
                } else if (this._currentPosition == Helper.LobbyButtonCoords.Start) {
                    this._skinController.StartButton();
                } else if (this._currentPosition == Helper.LobbyButtonCoords.Exit) {
                    if (GameManager.Instance.Players.Count > 1) {
                        GameManager.Instance.DeletePlayer(this._AssaigneSpecificPlayer);
                    } else {
                        GameManager.Instance.MainMenu();
                    }
                }
            }
            this._lastSelectTime = Time.time;

        }

        #endregion
    }
}
