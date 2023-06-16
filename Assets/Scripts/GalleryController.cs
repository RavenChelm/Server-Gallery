using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class GalleryController : MonoBehaviour
{
    public GameObject imagePrefab;
    public GridLayoutGroup gridLayout;
    private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int currentImageIndex = 0;
    private float threshold = 0.037f;
    [SerializeField] private Canvas canvas;
    private UnityWebRequestAsyncOperation request;
    private List<GameObject> listGameObj = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < 66; i++)
        {
            GameObject newImage = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            newImage.transform.SetParent(gridLayout.transform, false);
            listGameObj.Add(newImage);
            var buttonload = newImage.AddComponent<ButtonLoading>();
            Button buttonObj = newImage.GetComponent<Button>();
            Debug.Log(buttonObj.image.sprite);
            buttonObj.onClick.AddListener(delegate { buttonload.LoadViewScene(buttonObj.image.sprite); });
        }
        LoadImages(11);
    }

    IEnumerator LoadImage(int index)
    {
        string imageUrl = baseUrl + index.ToString() + ".jpg";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        request = www.SendWebRequest();
        yield return request;

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            CreateImage(texture, index);
        }
    }

    void CreateImage(Texture2D texture, int index)
    {
        RectTransform rectTransform = listGameObj[index - 1].GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0f, 0f));
        listGameObj[index - 1].GetComponent<Button>().image.sprite = sprite;
    }

    void LoadImages(int numberOfImagesToLoad)
    {
        for (int i = currentImageIndex; i < currentImageIndex + numberOfImagesToLoad; i++)
        {
            StartCoroutine(LoadImage(i));
        }
        currentImageIndex += numberOfImagesToLoad;
    }

    public void OnScrollRectValueChanged()
    {
        if (IsScrolledToBottom() && currentImageIndex < 66 && request.isDone)
        {
            LoadImages(4);
            threshold += ((listGameObj[0].GetComponent<RectTransform>().rect.size.y + 20) * 2) / (float)gridLayout.GetComponent<RectTransform>().rect.height;
        }
    }

    bool IsScrolledToBottom()
    {

        float scrollPosition = (float)gridLayout.GetComponent<RectTransform>().anchoredPosition.y / (float)gridLayout.GetComponent<RectTransform>().rect.height;
        Debug.Log(scrollPosition);
        return scrollPosition > threshold;
    }
}
