using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public Transform m_gameOver;
    public float m_GameOverFadeTime;
    public Color m_fadeColor;
    public Image m_fadePlane;
    

    private void Awake()
    {
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(FadeIn());
        m_gameOver.gameObject.SetActive(true);
    }

    IEnumerator FadeIn()
    {
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime / m_GameOverFadeTime;
            m_fadePlane.color = Color.Lerp(Color.clear, m_fadeColor, percent);
            yield return null;
        }
    }

    private void OnClickAs()
    {
        //Application.LoadLevel("Level1");
    }

}
