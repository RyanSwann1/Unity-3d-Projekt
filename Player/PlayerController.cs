using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    

    private Vector3 m_moveVelocity;
    private Vector3 m_newTurnPoint;
    private CharacterController m_characterController;

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 newVelocity)
    {
        //m_moveVelocity = newVelocity;
        m_characterController.Move(newVelocity * Time.deltaTime);
    }
    
    public void LookAt(Vector3 turnPoint)
    {
        Vector3 correctedHeightPoint = new Vector3(turnPoint.x, transform.position.y, turnPoint.z);
        transform.LookAt(correctedHeightPoint);
    }
}
