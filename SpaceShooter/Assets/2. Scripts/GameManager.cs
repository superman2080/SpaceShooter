using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution(1920, 1080, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += LoadedSceneEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadedSceneEvent(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Ingame":
                MonsterManager monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
                monsterManager.FindAllSpawnPos();
                monsterManager.SpawnMonsters(30, 2f);
                break;
            case "Start":
                break;
            default:
                break;
        }
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadGameSceneCor());
    }

    private IEnumerator LoadGameSceneCor()
    {
        GameObject.Find("Monster").GetComponent<Animator>().SetBool("IsStart", true);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Ingame");
    }

    private IEnumerator FadeOut()
    {
        Image fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        float dT = 0;
        while (true)
        {
            dT += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, dT);
            yield return null;
            if (dT > 1)
                break;
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }

}
