using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Babam : MonoBehaviour
{
    public GameObject crime;
    public GameObject whyRG;

    Button startButton;
    AudioSource yee;
    Vector2 targetPos = new Vector2 (8.6f, -3);
    Vector2 targetPos2 = new Vector2 (-8.8f, -4);

    bool canStart = false;

    void Start()
    {
        var root = GameObject.Find("UI").GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("start_button");

        startButton.RegisterCallback<ClickEvent>(StartGame);
        yee = GameObject.Find("yee").GetComponent<AudioSource>();
        StartCoroutine(Bam());
    }

    void StartGame(ClickEvent ev)
    {
        SceneManager.LoadScene("GameScreen");
    }

    IEnumerator Bam()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            yee.Play();

            yield return new WaitForSeconds(0.5f);
            var obj = Instantiate(crime, new Vector2(-4.19f, 1.11f), Quaternion.Euler(0, 0, 26.049f));

            yield return new WaitForSeconds(0.1f);
            var obj2 = Instantiate(crime, new Vector2(2f, -0.6f), Quaternion.Euler(0, 0, -21.1f));
            obj2.transform.localScale = new Vector2(7, 7);

            yield return new WaitForSeconds(0.2f);
            var obj3 = Instantiate(crime, new Vector2(5f, 2f), Quaternion.Euler(0, 0, -9f));
            obj3.transform.localScale = new Vector2(5, -5);

            yield return new WaitForSeconds(0.2f);
            Destroy(obj);
            Destroy(obj2);
            Destroy(obj3);

            obj = Instantiate(crime, new Vector2(-8f, 3f), Quaternion.identity);
            obj.transform.localScale = new Vector2(5.8f, 5.8f);

            while (Vector2.Distance(obj.transform.position, targetPos) >= 0.3f)
            {
                obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPos, 0.2f);
                yield return new WaitForSeconds(0.03f);
            }

            obj.transform.position = new Vector2(9, 5);
            obj.GetComponent<SpriteRenderer>().flipX = true;
            while (Vector2.Distance(obj.transform.position, targetPos2) >= 0.35f)
            {
                obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPos2, 0.2f);
                yield return new WaitForSeconds(0.03f);
            }

            obj.GetComponent<SpriteRenderer>().flipX = false;
            obj.transform.position = new Vector2(0.23f, -1.5f);
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
            obj.transform.localScale = new Vector2(22f, 22f);
            whyRG.SetActive(true);


            yield return new WaitForSeconds(1f);
            whyRG.SetActive(false);
            Destroy(obj);
            if (!this.canStart)
            {
                this.canStart = true;
                startButton.AddToClassList("start_button_fade");
            }
        }
    }
}
