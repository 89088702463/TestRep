using UnityEditor;

public class BuildAssetBundle
{
    [MenuItem("Assets/Build Asset Bundles")]
    static void BulidBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
