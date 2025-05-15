using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager> {
    [SerializeField] private List<VFXEntry> vfxList; // Assign in inspector

    private Dictionary<VFXType, GameObject> vfxDictionary;

    protected override void Awake() {
        base.Awake();
        // Initialize the dictionary for quick lookup
        vfxDictionary = new Dictionary<VFXType, GameObject>();
        foreach (var vfx in vfxList) {
            if (!vfxDictionary.ContainsKey(vfx.type)) {
                vfxDictionary.Add(vfx.type, vfx.prefab);
            }
        }
    }

    public void SpawnVFX(VFXType type, Vector3 spawnPosition, Quaternion rotation = default, Vector3 scale = default, float duration = 0f) {
        if (!vfxDictionary.ContainsKey(type)) {
            Debug.LogWarning($"VFX type {type} not found!");
            return;
        }

        GameObject vfxPrefab = vfxDictionary[type];
        GameObject vfxInstance = Instantiate(vfxPrefab, spawnPosition, rotation == default ? Quaternion.identity : rotation);
        vfxInstance.transform.localScale = scale == default ? Vector3.one : scale;

        if (duration > 0f) {
            Destroy(vfxInstance, duration);
        }
        // Optionally add auto-destroy script if needed
    }
}