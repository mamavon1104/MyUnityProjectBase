using Mamavon.Funcs;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Useful
{
    /// <summary>
    /// �v���C���[�̓��ގ��̊Ǘ��N���X�i�A�E�g�Q�[�� ���ɃL�����N�^�[�I���j
    /// </summary>
    public class PlayerJoinManager : MonoBehaviour
    {
        [Header("JoinAction��ݒ肵�Ă�����"), SerializeField]
        private InputActionReference playerJoinInputAction = default;
        private InputAction act;

        [Header("�Z�b�V�����V�X�e��"), SerializeField] PlayerInputManagerSessionSystem sessionSystem;

        [Header("�v���C���[�̃v���t�@�u"), SerializeField] private PlayerInput playerPrefab = default;

        [Header("�v���C���[�̃}�b�N�X�l��"), SerializeField] private int maxPlayerCount;

        // Join�ς݂̃f�o�C�X���
        private InputDevice[] joinedDevices = default;

        private void Awake()
        {
            act = new InputAction(name: playerJoinInputAction.action.name);

            // �Q�Ƃ��ꂽ�A�N�V��������o�C���f�B���O���R�s�[ (InputActionAssets��C#�����Ȃ��Ƌ������ς��̂Œ���)
            foreach (var binding in playerJoinInputAction.action.bindings)
            {
                act.AddBinding(binding.path);
            }

            joinedDevices = new InputDevice[maxPlayerCount];
        }
        private void OnEnable()
        {
            sessionSystem.joinAction += SetPlayerDevices;
            sessionSystem.leftAction += LeftPlayerProcess;

            act.Enable();
            act.performed += OnJoin;
        }

        private void OnDestroy()
        {
            sessionSystem.joinAction -= SetPlayerDevices;
            sessionSystem.leftAction -= LeftPlayerProcess;
            act.Dispose();
        }

        /// <summary>
        /// �f�o�C�X�ɂ����Join�v�������΂����Ƃ��ɌĂ΂�鏈��
        /// </summary>
        private void OnJoin(InputAction.CallbackContext context)
        {
            int playerNum = GetPlayerIndex();

            // �v���C���[�����ő吔�ɒB���Ă�����A�������I��
            if (playerNum >= maxPlayerCount)
                return;

            if (joinedDevices.Contains(context.control.device)) //�����f�o�C�X�������ĂĂ��I��
                return;

            // contextDevice��playerInput��R�Â��ăC���X�^���X�𐶐�����
            PlayerInput.Instantiate(
                    prefab: playerPrefab.gameObject,
                    playerIndex: playerNum,
                    pairWithDevice: context.control.device
                );

        }
        private void SetPlayerDevices(PlayerInput input)
        {
            joinedDevices[input.playerIndex] = input.devices[0];
        }
        private void LeftPlayerProcess(PlayerInput playerInp)
        {
            playerInp.playerIndex.Debuglog();
            int index = playerInp.playerIndex;
            if (index >= 0 && index < maxPlayerCount)
            {
                joinedDevices[index] = null;
                Debug.Log($"�v���C���[ {index + 1} ���ޏo���܂����B");
            }
        }

        private int GetPlayerIndex()
        {
            for (int i = 0; i < joinedDevices.Length; i++)
            {
                if (joinedDevices[i] == null)
                    return i;
            }
            return maxPlayerCount; //������Ԃ��Ă����� returnValue(maxPlayerCount) >= maxPlayerCount�ŏ�����B
        }
    }
}