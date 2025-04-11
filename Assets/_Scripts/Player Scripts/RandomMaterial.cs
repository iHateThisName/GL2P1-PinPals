using System.Collections.Generic;
using UnityEngine;

// Ivar
public class RandomMaterial : MonoBehaviour {
    [SerializeField] private PlayerReferences _modelController;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private List<Material> _materials = new List<Material>();
    [field: SerializeField] public Color AssignedMaterialColor { get; private set; } = Color.white;
    private int _currentMaterialIndex = 0;
    private int? _oldMaterialIndex = null;

    private void Start() {
        EnumPlayerTag tag = _modelController.GetPlayerTag();
        this._currentMaterialIndex = (int)tag - 1;
        EnsureUniqueMaterial();

        AssaigneMaterial();
    }

    public PlayerReferences GetModelController() => this._modelController;

    /// <summary>
    /// Assaigne the selected material to the renderer and make the old material avaiable in the material pool.
    /// </summary>
    private void AssaigneMaterial() {
        // Remove the old material from the used _materials list
        if (this._oldMaterialIndex != null) {
            Helper.UsedPinballMaterials.Remove(_materials[_oldMaterialIndex.Value]);
        }
        _renderer.material = _materials[this._currentMaterialIndex];
        // Add the new material to the used materials list
        Helper.UsedPinballMaterials.Add(_materials[this._currentMaterialIndex]);
        // Update the current color varibale
        this.AssignedMaterialColor = _materials[this._currentMaterialIndex].color;
        this._modelController.PlayerFollowCanvasManager.UpdateTextColor();

    }

    /// <summary>
    /// Ensures the selected material is unique by checking against the used materials list.
    /// If the selected material is already used, it finds the first available material that is not used.
    /// </summary>
    private void EnsureUniqueMaterial(bool isForward = true) {
        Material selectedMaterial = _materials[this._currentMaterialIndex];
        int startIndex = this._currentMaterialIndex;
        while (true) {

            if (!Helper.UsedPinballMaterials.Contains(_materials[startIndex])) {
                selectedMaterial = _materials[startIndex]; // Found available material
                this._oldMaterialIndex = this._currentMaterialIndex;
                this._currentMaterialIndex = startIndex;
                break;
            }
            if (isForward) {
                startIndex++;
                if (startIndex == _materials.Count) startIndex = 0; // Loop back to the start
            } else {
                startIndex--;
                if (startIndex == -1) startIndex = _materials.Count - 1; // Loop back to the end
            }
            if (startIndex == this._currentMaterialIndex) { // Looped through all materials
                Debug.LogError("No remaining available materials found!");
                break;
            }
        }
    }

    /// <summary>
    /// Changes the material to the material that is next in the material pool.
    /// </summary>
    public void SelectNext() {
        EnsureUniqueMaterial();
        AssaigneMaterial();
    }

    public void SelectPrevious() {
        EnsureUniqueMaterial(false);
        AssaigneMaterial();
    }

    public Material CurrentMaterial() => this._materials[this._currentMaterialIndex];
}
