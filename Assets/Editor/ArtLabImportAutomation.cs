using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters; // <-- THIS IS THE MISSING PIECE

public class ArtLabImportAutomation : AssetPostprocessor
{
    // -----------------------------------------------------------
    // 👾 2D PIXEL ART PIPELINE (FF6 Era)
    // -----------------------------------------------------------
    void OnPreprocessTexture()
    {
        // Only run this rule if the file is dropped into the 2D folder
        if (assetPath.Contains("2D_Pixel_Art"))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;

            // Force Sprite Mode
            textureImporter.textureType = TextureImporterType.Sprite;
            
            // Lock PPU to 16 for SNES-era accuracy
            textureImporter.spritePixelsPerUnit = 16f;
            
            // Force Point (No Filter) to keep pixels razor sharp
            textureImporter.filterMode = FilterMode.Point;
            
            // Turn off compression to prevent smudging
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            
            Debug.Log($"[Art Lab] Auto-processed 2D Pixel Asset: {assetPath}");
        }
    }

    // -----------------------------------------------------------
    // 🧊 3D PBR MODEL PIPELINE (FF7 Remake Era)
    // -----------------------------------------------------------
    void OnPreprocessModel()
    {
        // Only run this rule if the file is dropped into the 3D folder
        if (assetPath.Contains("3D_PBR_Models"))
        {
            ModelImporter modelImporter = (ModelImporter)assetImporter;

            // Tell Unity to automatically extract materials into the folder
            // rather than burying them inside the Read-Only FBX file
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.ImportStandard;
            modelImporter.materialLocation = ModelImporterMaterialLocation.External;
            
            // Tell Unity to search for textures next to the model and auto-link them
            modelImporter.materialSearch = ModelImporterMaterialSearch.Local;

            Debug.Log($"[Art Lab] Auto-processed 3D PBR Model: {assetPath}");
        }
    }

    // -----------------------------------------------------------
    // 🎨 URP MATERIAL AUTO-ASSIGNMENT
    // -----------------------------------------------------------
    void OnPreprocessMaterialDescription(MaterialDescription description, Material material, AnimationClip[] materialAnimation)
    {
        if (assetPath.Contains("3D_PBR_Models"))
        {
            // Force the newly extracted materials to use the Universal Render Pipeline Lit shader
            var urpLitShader = Shader.Find("Universal Render Pipeline/Lit");
            if (urpLitShader != null)
            {
                material.shader = urpLitShader;
            }
        }
    }
}