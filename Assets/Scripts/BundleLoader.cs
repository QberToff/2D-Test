using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleLoader : MonoBehaviour
{
    [SerializeField] string assetBundleURL = " ";
    int version = 0;
    Circle loadedAsset;

    //bool assetLoaded = false;

    public bool IsChecked { get; set; } = false;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType<BundleLoader>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }


    }


    private void Start()
    {
        StartCoroutine(DownloadAndCache());
    }
    IEnumerator DownloadAndCache()
    {
        while (!Caching.ready)//проверка готовности кэша к загрузке
            yield return null;

        var www = WWW.LoadFromCacheOrDownload(assetBundleURL, version);//загрузка бандла
        yield return www;

        if (!string.IsNullOrEmpty(www.error))//проверка на ошибки при загрузке бандла
        {
            Debug.Log(www.error);
            yield break;
        }
        Debug.Log("Bundle Loaded");

        var assetbudle = www.assetBundle;
        string prefabname = "Circle";

        var prefabRequest = assetbudle.LoadAssetAsync(prefabname, typeof(GameObject));//распаковка бандла
        yield return prefabRequest;
        Debug.Log("Asset unzipped");

        loadedAsset = prefabRequest.asset as Circle;
        this.IsChecked = true;
        Debug.Log("assetLoaded = " + this.IsChecked);
        


    }

    public Circle GetLoadedAsset()
    {
        return loadedAsset;
    }

   

}