using System.Collections.Generic;
using Example;
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts
{
    // 🏳️ SIDE ENUM – GOBLIN ALLIANCES!
    // Defines which team the goblin or entity belongs to. Watch out for AncientHorrors!
    public enum Side 
    {
        Side1,          // ⚔️ Player's team (usually the heroes, unless goblins stage a coup).
        Side2,          // 🛡️ Enemy bots trying to ruin all the fun.
        Side3,          // 🍄 Neutral or other factions (possibly mushroom people).
        AncientHorror   // 🧟 BIG BAD BOSSES! Scary monsters lurking in the shadows.
    }
    
    // 📝 GOBLIN ENTITY SETTINGS!
    // This ScriptableObject is the goblin’s blueprint—a shared magical scroll
    // that stores key settings for all entities of its kind (goblins, orcs, lovely princesses, etc.).
    // Load it up with stats, speed, detection details, and more!
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/EntitySettings")]
    public class EntitySettingsSo : SettingsBase
    {
        public Side MySide;
        public List<Side> MyFriends;
        public List<Side> MyEnemies;
        public float BaseHealth = 100f;
        
        public BulletController Bullet;
        public float ShotSpeed = 50f;
        public float FireRate = 1f;
        public float ShootingRange = 50f;
        public float FollowDistance = 60f;
        public float DetectionRange = 70f;
        public float RoamDistance = 50f;
        public LayerMask TargetObstructionLayers;
    }
}