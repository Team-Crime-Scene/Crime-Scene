using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public static class Extension
{
    public static bool Contain(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    public static Vector3 GetLocalPosition (this PointerEventData eventData, Transform transform)
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        return transform.InverseTransformPoint(worldPosition); 
    }

    public static Texture2D LoadTexture(string path)
    {
        Texture2D texture2D;
        byte [] FileData;

        if ( File.Exists(path) )
        {
            FileData = File.ReadAllBytes(path);
            texture2D = new Texture2D(2, 2);           // Create new "empty" texture
            if ( texture2D.LoadImage(FileData) )           // Load the imagedata into the texture (size is set automatically)
                return texture2D;                 // If data = readable -> return texture
        }
        return null;
    }

    public static Sprite LoadSprite( string FilePath, float PixelsPerUnit = 100.0f )
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Sprite NewSprite;
        Texture2D SpriteTexture = LoadTexture(FilePath);
        NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }
}