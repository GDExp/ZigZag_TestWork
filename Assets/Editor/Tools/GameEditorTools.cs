#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using ZigZag.GameCore;

public static class GameEditorTools
{
    private const string GameSetupObjectName = "GAME SETUP";

    [MenuItem("ZigZag Tools/Create Game Setup")]
    public static void CreateCurrentGameSetupObjectOnScene()
    {
        var gameSetup = GameObject.FindObjectOfType<GameSetup>();
        if(gameSetup != null)
        {
            Debug.LogWarning("<color=red><size=20>На сцене уже создан объект с настройками</size></color>");
            return;
        }
        else
        {
            GameObject go = new GameObject(GameSetupObjectName);
            go.AddComponent<GameSetup>();
            Debug.LogWarning($"<color=blue><size=20>Создан объект с настройками на сцене, имя обекта {GameSetupObjectName}</size></color>");
        }
    }
}

#endif
