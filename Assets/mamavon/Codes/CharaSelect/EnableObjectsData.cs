using UnityEngine;

public class EnableObjectsData : MonoBehaviour
{
    [Header("false���\���I�u�W�F�N�g"), SerializeField] GameObject m_disableObj = null;
    [Header("true���\���I�u�W�F�N�g"), SerializeField] GameObject m_enableObj = null;
    public void EnableDisableObject(bool isActive)
    {
        m_disableObj.SetActive(isActive ? false : true);
        m_enableObj.SetActive(isActive ? true : false);
    }
}
