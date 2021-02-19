using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    private void Start()
    {
        Texture2D texture = new Texture2D(128, 128, TextureFormat.RGBA32, false);
        var rnd = GetComponent<SpriteRenderer>();
        if(rnd != null)
        {
            Debug.Log("got renderer");
            for(int w = 0; w <= 128; w++)
            {
                for (int h = 0; h <= 128; h++)
                {
                    Color col = Color.clear;
                    texture.SetPixel(w, h, col);

                }

            }
            texture.Apply();
        }
        else
        {
            Debug.Log("DONT HAVE RENDERER");
        }
        
        Circle(texture, 64, 64, 64, Color.blue);
        texture.Apply();
        rnd.sprite = Sprite.Create(texture, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f), 128);


    }


    public void Circle(Texture2D tex, int cx, int cy, int r, Color col)
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
