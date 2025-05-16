using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helper {

    public static List<Material> UsedPinballMaterials = new List<Material>();
    public static Dictionary<EnumPlayerTag, GameObject> Players = new Dictionary<EnumPlayerTag, GameObject>();

    public const string level1 = "Level 01, LasVegas", level2 = "Level 02, PipelineMayhem", level3 = "Level 03, 6FeetUnder", level4 = "Level 04, Wide-West",
        level5 = "Level 05, HauntedPinMachine", level6 = "Level 06, The Ring", levelSelector = "LevelSelector", endGame = "EndGame", mainMenu = "MainMenu", lobby = "Lobby", howToPlay = "HowToPlay";

    public static class LobbyButtonCoords {
        public static readonly Vector2Int Previous = new(0, 0);
        public static readonly Vector2Int Next = new(1, 0);
        public static readonly Vector2Int Start = new(0, -1);
        public static readonly Vector2Int Exit = new(1, -1);
    }
    public static void GameReset() {
        UsedPinballMaterials.Clear();
        Players.Clear();
    }
    /// <summary>
    /// Finds an unused player tag starting from a given index.
    /// </summary>
    /// <param name="startIndex">The index to start searching from. Defaults to 1.</param>
    /// <returns>The first unused player tag found. If all player tags are used, returns EnumPlayerTag.Player16 and logs an error.</returns>
    /// <remarks>
    /// This method first searches for an unused player tag starting from the given index up to 16.
    /// If no unused tag is found, it searches again from 1 to 16. If still no unused tag is found,
    /// it logs an error indicating that more than 16 players have joined the game, which is not supported.
    /// </remarks>
    public static EnumPlayerTag GetUnusedPlayerTag(Dictionary<EnumPlayerTag, GameObject> players, int startIndex = 1) {
        for (int i = startIndex; i <= 16; i++) {
            if (!players.ContainsKey((EnumPlayerTag)i)) {
                return (EnumPlayerTag)i;
            }
        }

        for (int i = 1; i <= 16; i++) {
            if (!players.ContainsKey((EnumPlayerTag)i)) {
                return (EnumPlayerTag)i;
            }
        }
        Debug.LogError("More that 16 players joined the game. This is not supported.");
        return EnumPlayerTag.Player16;
    }

    public static EnumPlayerTag GetPlayerTagKey(Dictionary<EnumPlayerTag, GameObject> players, GameObject playerObject) {
        // Find the key corresponding to the given value
        EnumPlayerTag keyToRemove = players.FirstOrDefault(x => x.Value == playerObject).Key;
        return keyToRemove;
    }

}
