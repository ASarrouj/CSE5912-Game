using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class AIStateManager : MonoBehaviour
    {
        public enum BehaviorState
        {
            Hide,
            Follow
        }

        public BehaviorState selectState;
        private BehaviorState currentState;

        private HideAgent hideAgent;
        private FollowAgent followAgent;

        private MechTakeDamage coreDamage;
        private float maxHP, defenseHP;
    
        void Start()
        {
            hideAgent = GetComponent<HideAgent>();    
            followAgent = GetComponent<FollowAgent>();
            GetState();
            coreDamage = transform.Find("NewMechWithGuns").Find("CoreHitbox").GetComponent<MechTakeDamage>();
            maxHP = coreDamage.health;
            defenseHP = maxHP / 2;
        }

        void Update()
        {
            if (coreDamage.health > defenseHP) {
                selectState = BehaviorState.Follow;
            } else {
                selectState = BehaviorState.Hide;
            }

            if (selectState != currentState) {         
                GetState();
                currentState = selectState;
            }    
        }

        void DisableAgents() {
            hideAgent.enabled = false;
            followAgent.enabled = false;
        }

        void GetState() {
            DisableAgents();
            switch (selectState) {
                case BehaviorState.Hide:
                    hideAgent.enabled = true;
                    break;
                case BehaviorState.Follow:
                    followAgent.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
