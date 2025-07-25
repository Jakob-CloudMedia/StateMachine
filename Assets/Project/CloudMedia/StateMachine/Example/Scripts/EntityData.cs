using System.Collections.Generic;
using CloudMedia.StateMachine.Example.Scripts.Bots;
using UnityEngine;
using UnityEngine.AI;

namespace CloudMedia.StateMachine.Example.Scripts
{
    
    // 🧠 ENTITY DATA – GOBLIN'S PERSONAL RUNTIME INFORMATION!
    // This class holds all the juicy runtime data specific to each goblin. 🦴
    // It's like each goblin's personal journal, tracking their health, friends, enemies, where they roam, and more.

    [System.Serializable]
    public class EntityData : DataBase
    {
		public float CurrentHealth;
        public AbsEntityController Me;
        public List<AbsEntityController> Enemies;
        public List<AbsEntityController> Friends;
        public AbsEntityController ActionTarget;
        public bool IsAbleToShoot;
        public NavMeshAgent NavMeshAgent;
        public Vector3 StartPosition;
        public Vector3 CurrentRoamPosition;
        public AncientEntity AncientEntity;
        public AbsEntityController Leader;
        
        public bool DebugLog;
    }
}
