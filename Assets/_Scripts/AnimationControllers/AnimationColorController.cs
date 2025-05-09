using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationColorController : MonoBehaviour
{

    [SerializeField] private List<MeshRenderer> _materials;
    [SerializeField] private Material _insideMaterial;
    public void ApplyColor(Material material) {
        List<Material> newMaterial = new List<Material>();
        newMaterial.Add(_insideMaterial);
        newMaterial.Add(material);

        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].SetMaterials(newMaterial);
        }
    }

    IEnumerator Start() {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
