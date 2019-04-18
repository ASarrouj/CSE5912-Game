using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class AIStateManager : MonoBehaviour
    {
        public enum BehaviorState
        {
            Patrol,
            Hide,
            Follow,
        }

        public BehaviorState selectState;
        private BehaviorState currentState;

        private HideAgent hideAgent;
        private FollowAgent followAgent;
        private PatrolAgent patrolAgent;

        private PlayerTargeting pTargeting;

        private MechTakeDamage coreDamage;
        private float maxHP, defenseHP;
    
        void Start()
        {
            hideAgent = GetComponent<HideAgent>();    
            followAgent = GetComponent<FollowAgent>();
            patrolAgent = GetComponent<PatrolAgent>();
            pTargeting = GetComponent<PlayerTargeting>();
            GetState();
            coreDamage = transform.Find("NewMechWithSlots").Find("CoreHitbox").GetComponent<MechTakeDamage>();
            maxHP = coreDamage.health;
            defenseHP = maxHP / 2;
        }

        void Update()
        {
            if (!pTargeting.Target) {
                selectState = BehaviorState.Patrol;
            }
            else {
                if (coreDamage.health > defenseHP) {
                    selectState = BehaviorState.Follow;
                } else {
                    selectState = BehaviorState.Hide;
                }
            }

            if (selectState != currentState) {         
                GetState();
                currentState = selectState;
            }  
        }

        void DisableAgents() {
            patrolAgent.enabled = false;
            hideAgent.enabled = false;
            followAgent.enabled = false;
        }

        void GetState() {
            DisableAgents();
            switch (selectState) {
                case BehaviorState.Patrol:
                    patrolAgent.enabled = true;
                    break;
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
