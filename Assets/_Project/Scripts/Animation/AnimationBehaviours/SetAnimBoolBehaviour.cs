using UnityEngine;
using UnityEngine.Animations;

using AnimationStateEvent = DerbyRoyale.Animation.AnimationData.AnimationStateEvent;

namespace DerbyRoyale.Animation.AnimationBehaviours
{
    [AddComponentMenu("Derby Royale/Animation/Animation Behaviours/Set Boolean Behaviour")]
    public class SetAnimBoolBehaviour : StateMachineBehaviour
    {
        #region PROPERTIES
        private AnimationStateEvent onState { get => m_OnState; set => m_OnState = value; }
        private string booleanName { get => m_BooleanName; set => m_BooleanName = value; }
        private bool booleanValue { get => m_BooleanValue; set => m_BooleanValue = value; }
        #endregion


        #region EDITOR FIELDS
        [Header("SET BOOLEAN BEHAVIOUR"), Space(3)]
        [SerializeField]
        private AnimationStateEvent m_OnState;
        [SerializeField]
        private string m_BooleanName;
        [SerializeField]
        private bool m_BooleanValue;
        #endregion


        #region IMPLEMENTATION - State Machine Behaviour
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            if (onState == AnimationStateEvent.OnStateBegin)
            {
                animator.SetBool(booleanName, booleanValue);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex, controller);

            if (onState == AnimationStateEvent.OnStateUpdate)
            {
                animator.SetBool(booleanName, booleanValue);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (onState == AnimationStateEvent.OnStateEnd)
            {
                animator.SetBool(booleanName, booleanValue);
            }
        }
        #endregion
    }
}