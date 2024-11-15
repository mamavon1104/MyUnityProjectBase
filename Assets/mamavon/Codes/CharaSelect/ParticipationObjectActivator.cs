using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace Mamavon.Useful
{

    public class ParticipationObjectActivator : MonoBehaviour
    {
        [SerializeField] private PlayerSelectManager m_playerSelectManager;
        [Header("�v���C���[�̗L�����������Ő؂�ւ���I�u�W�F�N�g"), SerializeField] private List<EnableObjectsData> m_activeObjDatas;
        [Header("�}���`�v���C���[��InputModule"), SerializeField] private List<InputSystemUIInputModule> m_playerInputModule;
        [Header("�}���`�v���C���[��EventSystem"), SerializeField] private List<MultiplayerEventSystem> m_playerEventSystem;

        private void Start()
        {
            for (int i = 0; i < m_playerSelectManager.PlayerActiveStatus.Length; i++)
            {
                int index = i; // �L���v�`���ϐ�
                m_playerSelectManager.PlayerActiveStatus[index].Subscribe(isActive =>
                {
                    m_activeObjDatas[index].EnableDisableObject(isActive);
                }).AddTo(this);
            }
        }
        public InputSystemUIInputModule GetEventSystem(int i)
        {
            return m_playerInputModule[i];
        }
        public void SetFirstSelectObject(int i, GameObject gameObject)
        {
            m_playerEventSystem[i].firstSelectedGameObject = gameObject;
            m_playerEventSystem[i].SetSelectedGameObject(gameObject);
        }
        [ContextMenu("���̃q�G�����L�[����ϐ����")]
        private void StartSetMyValues()
        {
            m_playerInputModule = new List<InputSystemUIInputModule>();
            m_playerEventSystem = new List<MultiplayerEventSystem>();
            m_activeObjDatas = new List<EnableObjectsData>();

            SetMyValuesProcess(transform);
        }
        private void SetMyValuesProcess(Transform parentObj)
        {
            //concat�ŗv�f��ǉ����Ȃ���z��ƂȂ���B  
            foreach (Transform child in parentObj)
            {
                SetMyValuesProcess(child);

                if (child.TryGetComponent<InputSystemUIInputModule>(out var iM))
                    m_playerInputModule.Add(iM);

                if (child.TryGetComponent<MultiplayerEventSystem>(out var eS))
                    m_playerEventSystem.Add(eS);

                if (child.TryGetComponent<EnableObjectsData>(out var eOJ))
                    m_activeObjDatas.Add(eOJ);
            }
        }
    }
}