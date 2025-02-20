using System.Collections.Generic;
using UnityEngine;
// Ivar
public class RandomMaterial : MonoBehaviour {
    [SerializeField] private GameObject _player;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private List<Material> _materials = new List<Material>();

    public void Start() {
        string playerTag = _player.tag;
        int playerNumber = int.Parse(playerTag.Substring(playerTag.Length - 1));
        Material selectedMaterial = EnsureUniqueMaterial(_materials[playerNumber - 1]);

        if (_renderer != null && selectedMaterial != null) {
            // Remove the old material from the used _materials list
            Material oldMaterial = _renderer.material;
            if (oldMaterial != null) Helper.UsedPinballMaterials.Remove(oldMaterial);

            // Assign the new material to the renderer
            _renderer.material = selectedMaterial;
            // Add the new material to the used materials list
            Helper.UsedPinballMaterials.Add(selectedMaterial);
        } else {
            Debug.LogError("No Renderer found on the spawned object! Or missing material");
        }
    }

    /// <summary>
    /// Ensures the selected material is unique by checking against the used materials list.
    /// If the selected material is already used, it finds the first available material that is not used.
    /// </summary>
    private Material EnsureUniqueMaterial(Material selectedMaterial) {
        if (Helper.UsedPinballMaterials.Contains(selectedMaterial)) {
            selectedMaterial = null;
            for (int i = 0; i < _materials.Count; i++) {
                if (!Helper.UsedPinballMaterials.Contains(_materials[i])) {
                    selectedMaterial = _materials[i];
                    break;
                }
            }
            if (selectedMaterial == null) {
                Debug.LogError("No remaining available materials found!");
            }
        }
        return selectedMaterial;
    }
}
