using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider sliderLoading;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsync(PlayerPrefsManager.instance.GetLastScene()));
    }

    IEnumerator LoadAsync(string nextScene)
    {
        // agar setiap memulai game, dimulai dari main menu
        PlayerPrefsManager.instance.SetLastScene("Home");
        AsyncOperation sync = SceneManager.LoadSceneAsync(nextScene);

        if (sync == null)
        {
            SceneManager.LoadScene("Home");
        }
        else
        {
            while(!sync.isDone)
            {
                float progress = Mathf.Clamp01(sync.progress);
                sliderLoading.value = progress;

                yield return new WaitForEndOfFrame();
            }
        }
    }

}
