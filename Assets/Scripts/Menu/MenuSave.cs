using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public int version = 2;
    public string scene;
    public Vector3 position;
    public bool tutorialStarted;
    public int paintsCollected;
    public bool brushCollected;
    public List<string> defeatedEnemies = new List<string>();
    public List<int> collectedPaintIDs = new List<int>();
}

public static class MenuSave
{
    public static string SavePath => Path.Combine(Application.persistentDataPath, "gds2-save.json");

    public static bool TryRead(out GameSaveData data, out string error)
    {
        data = null;
        error = "";
        try
        {
            if (!File.Exists(SavePath)) { error = "No saved game yet."; return false; }
            data = JsonUtility.FromJson<GameSaveData>(File.ReadAllText(SavePath));
            Validate(data);
            return true;
        }
        catch (Exception ex) { data = null; error = "Cannot load this save. " + ex.Message; return false; }
    }

    private static void Validate(GameSaveData data)
    {
        if (data == null || data.version != 2 || string.IsNullOrEmpty(data.scene)
            || !Application.CanStreamedLevelBeLoaded(data.scene)
            || !Finite(data.position.x) || !Finite(data.position.y) || !Finite(data.position.z)
            || data.paintsCollected < 0 || data.defeatedEnemies == null || data.collectedPaintIDs == null
            || data.paintsCollected != new HashSet<int>(data.collectedPaintIDs).Count)
            throw new InvalidDataException("The save is incompatible or incomplete.");
    }

    private static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);

    public static bool TryWrite(GameSaveData data, out string error)
    {
        error = "";
        try
        {
            Validate(data);
            Directory.CreateDirectory(Application.persistentDataPath);
            string temporary = SavePath + ".tmp";
            File.WriteAllText(temporary, JsonUtility.ToJson(data, true));
            if (File.Exists(SavePath)) File.Replace(temporary, SavePath, null);
            else File.Move(temporary, SavePath);
            return true;
        }
        catch (Exception ex) { error = "Save failed. Your game is still open. " + ex.Message; return false; }
    }
}
