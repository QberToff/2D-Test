using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{

    Texture2D[] textures = new Texture2D[4];
    static public bool TextureReady { get; set; } = false;
    
    private void Start()
    {
        textures[0] = new Texture2D(32, 32);
        textures[1] = new Texture2D(64, 64);
        textures[2] = new Texture2D(128, 128);
        textures[3] = new Texture2D(256, 256);

        Debug.Log("Array of textures declared and textures added");
        
        CreateTextures();

    }

    
    public void SetTexture(SpriteRenderer spr, float size)
    {
        switch (size)
        {
            case 32:
                spr.sprite = Sprite.Create(textures[0], new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
                Debug.Log("32x32 texture setted to circle");
                break;
            case 64:
                spr.sprite = Sprite.Create(textures[1], new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
                Debug.Log("64x64 texture setted to circle");
                break;
            case 128:
                spr.sprite = Sprite.Create(textures[2], new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
                Debug.Log("128x128 texture setted to circle");
                break;
            case 256:
                spr.sprite = Sprite.Create(textures[3], new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
                Debug.Log("256x256 texture setted to circle");
                break;
        }

    }
    
    private void CreateTextures()
    {
        for (int i = 0; i < 4; i++)
        {
           
            for (int y = 0; y < textures[i].height; y++) 
            {
                for (int x = 0; x < textures[i].width; x++)
                {
                    textures[i].SetPixel(x, y, Color.clear);
                }
            }
            Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            DrawCircle(textures[i], textures[i].width / 2, textures[i].height / 2, textures[i].height / 2, col);
            textures[i].Apply();
            Debug.Log("texture  " + textures[i].width +" ready");
        }
        TextureReady = true;
    }

    
    public void RemakeTextureArray()
    {
        
        for(int n = 0; n < textures.Length; n++ )
        {
            textures[n] = null;
        }
        Resources.UnloadUnusedAssets();

        Texture2D[] newtextures = new Texture2D[4];

        newtextures[0] = new Texture2D(32, 32);
        newtextures[1] = new Texture2D(64, 64);
        newtextures[2] = new Texture2D(128, 128);
        newtextures[3] = new Texture2D(256, 256);

        for (int i = 0; i < textures.Length; i++)
        {
            textures[i] = newtextures[i];
        }

        CreateTextures();

    }

    
    private void DrawCircle(Texture2D tex, int cx, int cy, int r, Color col)
    {
        int x, y, px, nx, py, ny, d;

        for (x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
            for (y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tex.SetPixel(px, py, col);
                tex.SetPixel(nx, py, col);

                tex.SetPixel(px, ny, col);
                tex.SetPixel(nx, ny, col);

            }
        }
    }


}
