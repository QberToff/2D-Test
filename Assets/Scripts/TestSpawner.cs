using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] Circle circlePrefab;
    [SerializeField] TextureGenerator texGen;

    float[] sizes = new float[4] /*{32, 64, 128, 256 }*/;

    // Start is called before the first frame update
    void Start()
    {
        sizes[0] = 32;
        sizes[1] = 64;
        sizes[2] = 128;
        sizes[3] = 256;



    }

    private void Update()
    {
        float size;
        if (TextureGenerator.TextureReady)
        {
            for (int i = 0; i < 4; i++)
            {
                size = sizes[i];
                switch (size)
                {
                    case 32:

                        Circle nextCircle = Instantiate(circlePrefab as Circle, new Vector3(i + 2, 10, 0), Quaternion.identity);
                        texGen.SetTexture(nextCircle.GetComponent<SpriteRenderer>(), size);
                        break;
                    case 64:

                        Circle nextCircle1 = Instantiate(circlePrefab as Circle, new Vector3(i + 2, 10, 0), Quaternion.identity);
                        texGen.SetTexture(nextCircle1.GetComponent<SpriteRenderer>(), size);
                        break;
                    case 128:

                        Circle nextCircle2 = Instantiate(circlePrefab as Circle, new Vector3(i + 2, 10, 0), Quaternion.identity);
                        texGen.SetTexture(nextCircle2.GetComponent<SpriteRenderer>(), size);
                        break;
                    case 256:

                        Circle nextCircle3 = Instantiate(circlePrefab as Circle, new Vector3(i + 2, 10, 0), Quaternion.identity);
                        texGen.SetTexture(nextCircle3.GetComponent<SpriteRenderer>(), size);
                        break;

                }

            }

            TextureGenerator.TextureReady = false;
        }

    }
}