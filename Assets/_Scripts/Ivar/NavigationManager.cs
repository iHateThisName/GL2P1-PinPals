using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Ivar 
public class NavigationManager : Singleton<NavigationManager> {

    // Input Actions
    private InputAction _moveInput;
    private InputAction _selectInput;

    [SerializeField] private Vector2Int _startPosition = Vector2Int.zero;
    private Vector2Int _currentPosition;

    private Dictionary<Vector2Int, NavigationController> navigationGrid = new Dictionary<Vector2Int, NavigationController>();
    public bool LockedNavigation = false;

    private void Start() {

        // Input Actions
        this._moveInput = InputSystem.actions.FindAction("Move");
        this._selectInput = InputSystem.actions.FindAction("Interact");

        StartListening();

        // Grid Setup
        this._currentPosition = this._startPosition;
        if (navigationGrid.ContainsKey(this._currentPosition)) {
            navigationGrid[this._currentPosition].NavigatedEvent.Invoke();
        }
    }

    public void RegisterObject(Vector2Int gridPosition, NavigationController controller) {
        navigationGrid[gridPosition] = controller;
    }

    #region "Input Actions"
    public void StartListening() {
        _moveInput.Enable();
        _moveInput.performed += OnMovePerformed;
        _moveInput.canceled += OnMovePerformed;

        _selectInput.Enable();
        _selectInput.performed += OnSelectPerformed;
        _selectInput.canceled += OnSelectPerformed;
    }


    public void StopListening() {
        _moveInput.performed -= OnMovePerformed;
        _moveInput.canceled -= OnMovePerformed;
        _moveInput.Disable();

        _selectInput.performed -= OnSelectPerformed;
        _selectInput.canceled -= OnSelectPerformed;
        _selectInput.Disable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context) {
        if (this.LockedNavigation) return; // Not allowed to move
        Vector2 move = context.ReadValue<Vector2>();
        Vector2Int direction = Vector2Int.zero;

        if (move.x > 0) direction = Vector2Int.right;
        else if (move.x < 0) direction = Vector2Int.left;
        else if (move.y > 0) direction = Vector2Int.up;
        else if (move.y < 0) direction = Vector2Int.down;

        if (direction != Vector2Int.zero) MoveSelection(direction);
    }

    private void MoveSelection(Vector2Int direction) {
        Vector2Int newPosition = this._currentPosition + direction; // New grid position

        if (navigationGrid.ContainsKey(newPosition)) { // Check if the new position is valid
            navigationGrid[this._currentPosition].DeselectedEvent.Invoke(); // Deselect the current object
            this._currentPosition = newPosition; // Register the new position
            navigationGrid[this._currentPosition].NavigatedEvent.Invoke(); // Select the new object
        }
    }
    private void OnSelectPerformed(InputAction.CallbackContext context) {
        if (this.LockedNavigation) return; // Not allowed to select

        if (navigationGrid.ContainsKey(this._currentPosition)) {
            navigationGrid[this._currentPosition].SelectedEvent.Invoke();
        }
    }
    #endregion
}
