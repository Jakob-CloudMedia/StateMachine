// 🏆 GAME OVER CONTROLLER – GOBLIN BATTLE RESULTS!
// This script handles the end of the battle: Did the goblins win? Did the player win? Or are all goblins gnashing their teeth in disappointment? 🐗
// Manages the game-over screen, decides the result, and stops time when the chaos ends!
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CloudMedia.StateMachine.Example.Scripts
{
    // 🎉 WHO WINS THE FIGHT?
    // This enum tracks the battle outcome. Only two possibilities: goblins or bots claim victory!
    public enum GameOverResult
    {
        PlayerWins, // 👑 Goblins (or players) defeat the enemy—time to celebrate!
        BotsWins    // 🤖 The bots crush the goblins—shame upon their caves!
    }
    
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Color _winColor;
        [SerializeField] private Color _loseColor;
        [SerializeField] private string _winText;
        [SerializeField] private string _loseText;
        [SerializeField] private Image _border;
        [SerializeField] private Text _message;

        // 🗺️ Keeps track of all active goblins and bots on the battlefield, sorted by their *side*.
        private readonly Dictionary<Side, List<AbsEntityController>> _entities = new(); 
        
        // 🏰 Singleton of this controller—only one game-over boss is allowed per battle!
        public static GameOverController Instance; 

        private void Awake()
        {
            // Ensure only ONE instance of this controller exists.
            if (Instance != null) Destroy(gameObject); // If there's already one… off to the trash bin!
            else Instance = this; //  Otherwise, set this as the ruler.
        }

        private void Start()
        {
            _gameOverCanvas.gameObject.SetActive(false); // Keep the game-over canvas hidden until the fighting ends.
        }

        public void RegisterEntity(AbsEntityController entity)
        {
            // Adds all entities to their respective side (team).
            var side = entity.Eb.Settings.MySide; // Check which side the entity belongs to.

            if (!_entities.ContainsKey(side)) 
            {
                _entities[side] = new List<AbsEntityController>(); // Create a new list for this side if none exists.
            }

            if (!_entities[side].Contains(entity)) 
            {
                _entities[side].Add(entity); // Add the entity to its team list!
            }
        }

        public void UpdateEntityStatus()
        {
            // Keeps an eye on the battlefield and decides who wins!
            if (_entities.Count == 0) return; // 🕵️ No entities? No game to end!

            // ⚔️ Check if all the enemies AND ancient horrors have been defeated.
            var allEnemiesDead = AreAllEntitiesDead(Side.Side2) && AreAllEntitiesDead(Side.AncientHorror);
            if (allEnemiesDead) GameOver(GameOverResult.PlayerWins); // 🎉 Players (goblins) win!

            // 💀 Check if all the player has been defeated.
            var playersDead = AreAllEntitiesDead(Side.Side1);
            if (playersDead) GameOver(GameOverResult.BotsWins); // 🤖 Bots win—goblin magic fails!
        }

        private void GameOver(GameOverResult result)
        {
            // 🛑 END THE GAME – Announce the winner and freeze time.
            if (result == GameOverResult.PlayerWins)
            {
                _message.text = _winText;      // Display victory message!
                _border.color = _winColor;    // Victory color!
            }
            else
            {
                _message.text = _loseText;    // Display defeat message…
                _border.color = _loseColor;  // Defeat's gloomy color.
            }
            
            _gameOverCanvas.gameObject.SetActive(true); // Show the game-over canvas!
            Time.timeScale = 0f;                        // Freeze the battlefield!
        }
        
        private bool AreAllEntitiesDead(Side side)
        {
            // Checks if all entities on a given side are DEAD or nonexistent.
            return !_entities.TryGetValue(side, out var entities) || entities.All(entity => entity.IsDead);
        }
    }
}