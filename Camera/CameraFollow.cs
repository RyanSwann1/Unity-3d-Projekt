using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float m_smoothing;
    public float m_xPosOffset;
    public float m_yPosOffset;
    public float m_zPosOffset;
    private Transform m_target;
    private Vector3 m_offSet;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //m_offSet = m_target.position - transform.position;
        FindTarget();
    }

    private void LateUpdate()
    {
        if (m_target != null)
        {
            Vector3 camToTargetPos = new Vector3(m_target.position.x + m_xPosOffset, m_target.position.y + m_yPosOffset, m_target.position.z + m_zPosOffset);
            transform.position = Vector3.Lerp(transform.position, camToTargetPos, m_smoothing * Time.deltaTime);
        }
    }

    private void FindTarget()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
        m_offSet = transform.position - m_target.position;
    }

}
