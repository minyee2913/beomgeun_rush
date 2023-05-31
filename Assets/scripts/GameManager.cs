using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject bum;
    public GameObject fast_load;
    public GameObject player;
    public GameObject[] falls;
    GameObject ui;
    AudioSource beatBox;
    AudioSource bgm;

    Button resumeButton;

    public bool alive = false;
    void Start()
    {
        ui = GameObject.Find("UI");
        var root = ui.GetComponent<UIDocument>().rootVisualElement;

        resumeButton = root.Q<Button>("resume");

        resumeButton.RegisterCallback<ClickEvent>(Resume);

        beatBox = GameObject.Find("beatBox").GetComponent<AudioSource>();
        bgm = GameObject.Find("bgm").GetComponent<AudioSource>();

        StartCoroutine(LoadAnim());
    }

    public IEnumerator LoadAnim()
    {
        resumeButton.style.display = DisplayStyle.None;
        alive = false;
        fast_load.SetActive(true);
        beatBox.Play();
        bgm.Stop();
        yield return new WaitForSeconds(0.3f);

        for (float i = 0; i < 2; i+=0.2f)
        {
            fast_load.transform.position = new Vector2(fast_load.transform.position.x - i, fast_load.transform.position.y);
            yield return new WaitForSeconds(0.03f);
        }

        for (float i = 0; i < 2; i += 0.2f)
        {
            fast_load.transform.position = new Vector2(fast_load.transform.position.x, fast_load.transform.position.y + i);
            yield return new WaitForSeconds(0.03f);
        }

        fast_load.transform.position = new Vector2(-2, 0);
        for (float i = 0.8f; i > 0.3; i -= 0.07f)
        {
            fast_load.transform.localScale = new Vector2(i, i);
            yield return new WaitForSeconds(0.03f);
        }

        for (int i = 0; i < 4; i++)
        {
            foreach (Transform child in fast_load.transform)
            {
                var renderer  = child.GetComponent<SpriteRenderer>();

                renderer.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
            }
            yield return new WaitForSeconds(0.1f);
        }

        fast_load.transform.position = new Vector2(-5.98f, 0.22f);
        player.transform.position = new Vector2(-6f, -2f);

        for (float i = 0.3f; i < 1; i+=0.1f)
        {
            fast_load.transform.localScale = new Vector2(i, i);
            yield return new WaitForSeconds(0.03f);
        }


        foreach (Transform child in fast_load.transform)
        {
            var renderer = child.GetComponent<SpriteRenderer>();

            renderer.color = Color.blue;
        }
        yield return new WaitForSeconds(0.1f);
        fast_load.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        bgm.Play();
        alive = true;
        StartCoroutine(Gaming());

        bum.SetActive(true);
        resumeButton.style.display = DisplayStyle.Flex;
        for (float i = 1; i >= 0; i -= 0.1f)
        {
            var renderer = bum.GetComponent<SpriteRenderer>();

            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, i);   

            yield return new WaitForSeconds(0.03f);
        }
        bum.SetActive(false);
    }

    void Resume(ClickEvent ev)
    {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator Gaming()
    {
       while (alive)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            var fall = Instantiate(falls[Random.Range(0, falls.Length)], new Vector2(Random.Range(-7, 7), 6), Quaternion.identity);
            Destroy(fall, 2f);
        }
    }
}
