using UnityEngine;

public class EnableObjectsData : MonoBehaviour
{
    [Header("false時表示オブジェクト"), SerializeField] GameObject m_disableObj = null;
    [Header("true時表示オブジェクト"), SerializeField] GameObject m_enableObj = null;
    public void EnableDisableObject(bool isActive)
    {
        m_disableObj.SetActive(isActive ? false : true);
        m_enableObj.SetActive(isActive ? true : false);
    }
}
