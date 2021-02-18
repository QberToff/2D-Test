using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleLoader : MonoBehaviour
{
    [SerializeField] string assetBundleMacURL = " ";
    [SerializeField] string assetBundleWinURL = " ";
    int version = 0;
    GameObject loadedAsset;

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
        if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            StartCoroutine(DownloadMacBundle());
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            StartCoroutine(DownloadWinBundle());
        }
        
    }
    IEnumerator DownloadMacBundle()
    {
        while (!Caching.ready)//проверка готовности кэша к загрузке
            yield return null;

        var www = WWW.LoadFromCacheOrDownload(assetBundleMacURL, version);//загрузка бандла
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

        loadedAsset = prefabRequest.asset as GameObject;

        if (loadedAsset != null)
        {
            this.IsChecked = true;
            Debug.Log("assetLoaded = " + this.IsChecked);
        }

    }

    IEnumerator DownloadWinBundle()
    {
        while (!Caching.ready)//проверка готовности кэша к загрузке
            yield return null;

        var www = WWW.LoadFromCacheOrDownload(assetBundleWinURL, version);//загрузка бандла
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

        loadedAsset = prefabRequest.asset as GameObject;

        if (loadedAsset != null)
        {
            this.IsChecked = true;
            Debug.Log("assetLoaded = " + this.IsChecked);
        }

    }

    public GameObject GetLoadedAsset()
    {
        return loadedAsset;
    }

   

}