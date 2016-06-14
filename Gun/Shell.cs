using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

    public float m_timeBeginFade;
    public float m_fadeTime;
    public float m_destroyTime;
    public float m_minMoveDist;
    public float m_maxMoveDist;
    private Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        float moveDist = Random.Range(m_minMoveDist, m_maxMoveDist);
        m_rigidbody.AddForce(transform.right * moveDist);
        m_rigidbody.AddTorque(Random.insideUnitSphere * moveDist);
        Destroy(gameObject, m_destroyTime);
    }

    //IEnumerator Fade()
    //{
    //    yield return new WaitForSeconds(m_timeBeginFade);

    //    Material shellMaterial = GetComponent<Renderer>().material;
    //    Color initialColor = shellMaterial.color;

    //    float percent = 0;
    //    while(percent < 1)
    //    {
    //        Debug.Log(percent);
    //        shellMaterial.color = Color.Lerp(initialColor, Color.clear, percent);
    //        percent += Time.deltaTime / m_fadeTime;
            
    //        yield return null;
    //    }

    //    Destroy(gameObject);
    //}
}
