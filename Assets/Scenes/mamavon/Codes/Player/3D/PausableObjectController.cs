using Mamavon.Data;
using Mamavon.Funcs;
using Mamavon.Useful;
using UniRx;
using UnityEngine;

namespace Mamavon.Code
{
    /// <summary>
    /// 3dゲームオブジェクトのポーズを行ったり再開したりします。
    /// </summary>
    public class PausableObjectController : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody rb;
        [SerializeField] private SaveVelocityCS velocityCS;

        private void Start()
        {
            animator = this.GetComponent<Animator>();
            rb = this.GetComponent<Rigidbody>();
            velocityCS = new SaveVelocityCS();

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