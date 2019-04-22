using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUPDPacks : MonoBehaviour
{
    string path = "http://g46869.hostnl1.fornex.org/test2.unity3d";
    public int version = 0;

    [SerializeField] GameObject prefab;

    void Awake()
    {
        StartCoroutine(DownloadBundle());
    }

   IEnumerator DownloadBundle()
    {
        while (!Caching.ready)
            yield return null;

        var www = WWW.LoadFromCacheOrDownload(path, version);
        yield return www;

        if(!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield break;
        }
    
        var assetBundle = www.assetBundle;

        var prefabRequest_line = assetBundle.LoadAssetAsync("BUNDLE_ASSET.prefab", typeof(GameObject));
        yield return prefabRequest_line;

        GameObject game_obj = prefabRequest_line.asset as GameObject;
        Vector3 pos = game_obj.transform.position;
        pos.y -= 10f;
        Debug.Log("-------------POSITION A LOAD LINE OBJECT--------------");
        Debug.Log("X = " + pos.x + " Y = " + pos.y + " Z = " + pos.z);
        Instantiate(game_obj, pos, Quaternion.identity);

        var prefabRequest_box = assetBundle.LoadAssetAsync("BOX_BUNDLE.prefab", typeof(GameObject));
        yield return prefabRequest_box;

        GameObject game_obj_2 = prefabRequest_box.asset as GameObject;

        Vector3 pos2 = game_obj_2.transform.position;
        Debug.Log("-------------POSITION A LOAD BOX OBJECT--------------");
        Debug.Log("X = " + pos2.x + " Y = " + pos2.y + " Z = " + pos2.z);
        Instantiate(game_obj_2, pos2, Quaternion.identity);
    }
}
