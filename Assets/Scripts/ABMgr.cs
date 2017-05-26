using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using XLua;

[LuaCallCSharp]
public class ABMgr : MonoBehaviour
{
    public Text LoadState;
    public GameObject UiCanvas;
    private AssetBundleCreateRequest _createRequest;
    private GameObject _mPrefab;

    // Use this for initialization
	void Start ()
	{
        LoadState.text = "未加载";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Load(string path)
	{
        //_createRequest = AssetBundle.Load(File.ReadAllBytes(path));
        //_createRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, path));
        //yield return _createRequest;
        //AssetBundle bundle = _createRequest.assetBundle;

        //string uri = "file:///" + Application.streamingAssetsPath + "/" + path;
        //UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.GetAssetBundle(uri, 0);
        //LoadState.text = uri;
        //yield return request.Send();
        //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        //var www = WWW.LoadFromCacheOrDownload(Application.streamingAssetsPath + "/" + path, 0);
        var www = WWW.LoadFromCacheOrDownload("http://192.168.1.180:8080/ab001.ab", 0);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield return null;
        }
        AssetBundle bundle = www.assetBundle;

        LoadState.text = "已加载";
        _mPrefab = bundle.LoadAsset<GameObject>("cat");

        var obj = Instantiate(_mPrefab, UiCanvas.transform);
        obj.GetComponent<Image>().rectTransform.localPosition = new Vector3(10, 10, 0);
    }

    public void LoadAssetBundle()
    {
        StartCoroutine(Load("ab001"));
    }

    public void Click(){

    }
}
