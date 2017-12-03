﻿using MaterialStore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaterialSetter : MonoBehaviour
{
    public int materialChosen;
    public MaterialCollection materialStore;

    public void SetMaterials()
    {
        var materialPattern = Resources.Load<Texture2DArray>("patternTextures");
        Shader.SetGlobalTexture("_MatTexArray", materialPattern);
        foreach (var part in FindObjectsOfType<Building.MaterialPart>())
        {
            var renderer = part.GetComponent<MeshRenderer>();
            MaterialPropertyBlock prop = new MaterialPropertyBlock();
            prop.SetColor("_MatColor", materialStore.textures[materialChosen].color);
            prop.SetFloat("_MatIndex", materialStore.textures[materialChosen].patternIndex);
            renderer.SetPropertyBlock(prop);
        }
        foreach (var part in FindObjectsOfType<ItemModel>())
        {
            var renderers = part.GetComponentsInChildren<MeshRenderer>();
            MaterialPropertyBlock prop = new MaterialPropertyBlock();
            prop.SetColor("_MatColor", materialStore.textures[materialChosen].color);
            prop.SetFloat("_MatIndex", materialStore.textures[materialChosen].patternIndex);
            foreach (var renderer in renderers)
            {
                renderer.SetPropertyBlock(prop);
            }
        }
    }
}
