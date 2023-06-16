using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonLoading : MonoBehaviour
{
    private float TimeLoad = 0.5f;
    private float currentTime = 0;
    public void LoadGalleryScene()
    {
        Coroutines.StartRoutine(LoadScene("02_Gallery", true, null));
    }
    public void LoadViewScene(Sprite sprite)
    {
        if (GetComponent<Button>().image.sprite.name == "UISprite") return;
        Coroutines.StartRoutine(LoadScene("03_View", true, sprite));
    }
    public void LoadMainScene()
    {
        Coroutines.StartRoutine(LoadScene("01_Menu", unloadLastScene: true, null));
    }
    public IEnumerator LoadScene(string sceneName, bool unloadLastScene, Sprite sprite)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync("04_Loading_Screen", LoadSceneMode.Additive);

        while (currentTime < TimeLoad)
        {
            LoadingBar.Instance.UpdateLoadingBar(currentTime / TimeLoad);
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }

        if (unloadLastScene)
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        if (sprite != null)
        {
            ViewScreen.Instance.SetImage(sprite);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        SceneManager.UnloadSceneAsync("04_Loading_Screen");
        asyncOperation.allowSceneActivation = true;
    }

}
