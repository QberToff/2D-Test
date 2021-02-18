using UnityEditor;
using System.IO;


public class CreateAssetBundle
{

    [MenuItem("Assets/Create AssetBundles /Create AssetBundles For Mac")]

    static void CreateAssetBundlesForMac()
    {
        string assetBundleDirectory = "Assets/AssetBundlesForMac";

        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

    }

    [MenuItem("Assets/Create AssetBundles /Create AssetBundles For Windows")]

    static void CreateAssetBundlesForWindows()
    {
        string assetBundleDirectory = "Assets/AssetBundlesForWindows";

        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

    }







}