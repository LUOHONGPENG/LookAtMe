using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SpriteImport : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.spritePixelsPerUnit = 100;
    }
}
