using UnityEditor;
using System.IO;


public class CreateAssetBundle
{

    [MenuItem("Assets/ Create AssetBundle")]

    static void CreateAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundle";

        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

    }









}