//  Rigidbody2DExtension.cs
//  http://kan-kikuchi.hatenablog.com/entry/Pause_Resume
//  改変元
//  Created by kan kikuchi on 2015.11.26.

using Mamavon.Data;
using Mamavon.Funcs;
using Mamavon.Useful;
using UniRx;
using UnityEngine;

namespace Mamavon.Code
{
    /// <summary>
    /// 3d�Q�[���I�u�W�F�N�g�̃|�[�Y��s������ĊJ�����肵�܂��B
    /// </summary>
    public class PausableObjectController2D : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D rb;
        [SerializeField] private SaveVelocity2DCS velocityCS;

        private void Start()
        {
            animator = this.GetComponent<Animator>();
            rb = this.GetComponent<Rigidbody2D>();
            velocityCS = new SaveVelocity2DCS();

            PauseGameManager.Instance.OnPaused.Subscribe(_ => PauseProcess()).AddTo(this);
            PauseGameManager.Instance.OnResumed.Subscribe(_ => ResumeProcess()).AddTo(this);
        }
        private void PauseProcess()
        {
            if (animator != null)
                animator.speed = 0;

            rb?.Pause(velocityCS);
        }
        private void ResumeProcess()
        {
            if (animator != null)
                animator.speed = 1f;

            rb?.Resume(velocityCS);
        }
    }
}