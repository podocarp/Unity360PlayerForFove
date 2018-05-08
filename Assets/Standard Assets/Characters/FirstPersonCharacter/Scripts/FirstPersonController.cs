using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] public MouseLook m_MouseLook;
        public Camera m_Camera;
        private Vector3 m_MoveDir = Vector3.zero;
        private Vector3 m_OriginalCameraPosition;

        // Use this for initialization
        private void Start()
        {
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
			m_MouseLook.Init(transform , m_Camera.transform);
        }
        
        // Update is called once per frame
        private void Update()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }

        private void FixedUpdate()
        {
            m_MouseLook.UpdateCursorLock();
        }
        public void ToggleX()
        {
            m_MouseLook.XSensitivity = -m_MouseLook.XSensitivity;
        }
        public void ToggleY()
        {
            m_MouseLook.YSensitivity = -m_MouseLook.YSensitivity;
        }
        public void SetSensitivity(float amount)
        {
            m_MouseLook.XSensitivity = (m_MouseLook.XSensitivity < 0) ? -amount : amount;
            m_MouseLook.YSensitivity = (m_MouseLook.YSensitivity < 0) ? -amount : amount;
        }
    }
}
