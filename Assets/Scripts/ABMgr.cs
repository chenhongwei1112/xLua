using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XLua;

[LuaCallCSharp]
public class ABMgr : MonoBehaviour
{
    public Text LoadState;
    public GameObject UiCanvas;
    private AssetBundleCreateRequest _createRequest;
    private GameObject _mPrefab;
    private static AssetBundle bundle;
    private TextAsset luaContent;

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
        //加载本地资源
        //_createRequest = AssetBundle.Load(File.ReadAllBytes(path));
        //_createRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, path));
        //yield return _createRequest;
        //AssetBundle bundle = _createRequest.assetBundle;

        //使用UnityWebRequest加载资源，系统有bug
        //string uri = "file:///" + Application.streamingAssetsPath + "/" + path;
        //UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.GetAssetBundle(uri, 0);
        //LoadState.text = uri;
        //yield return request.Send();
        //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        //从本地加载资源
        //var www = WWW.LoadFromCacheOrDownload(Application.streamingAssetsPath + "/" + path, 0);
        //从服务器加载资源
        var www = WWW.LoadFromCacheOrDownload("http://192.168.1.180:8080/"+ path, 0);

        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield return null;
        }
        bundle = www.assetBundle;

        //加载图片
        //_mPrefab = bundle.LoadAsset<GameObject>("cat");
        //var obj = Instantiate(_mPrefab, UiCanvas.transform);
        //obj.GetComponent<Image>().rectTransform.localPosition = new Vector3(10, 10, 0);

        //加载场景
        //string[] scenePath = bundle.GetAllScenePaths();
        //SceneManager.LoadSceneAsync(System.IO.Path.GetFileNameWithoutExtension(scenePath[0]));

    }

    public void LoadAssetBundle()
    {
        StartCoroutine(Load("ab006"));
    }

    public void Click(){

    }

    public static TextAsset GetLuaContent(string fileName)
    {
        return bundle.LoadAsset<TextAsset>(fileName);
    }
}
