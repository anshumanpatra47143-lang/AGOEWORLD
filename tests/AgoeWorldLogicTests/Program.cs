using System;
using System.Reflection;
using UnityEngine.SceneManagement;

public static class Program
{
    private static int passed;
    private static int failed;

    private static void Check(string name, string expected)
    {
        string actual = SceneManager.LastLoadedScene;

        if (actual == expected)
        {
            Console.WriteLine("PASS: " + name);
            passed++;
        }
        else
        {
            Console.WriteLine(
                $"FAIL: {name} | Expected={expected} | Actual={actual}"
            );
            failed++;
        }
    }

    public static void Main()
    {
        var flow = new AgoeWorldGameFlow();

        typeof(AgoeWorldGameFlow)
            .GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic)
            .Invoke(flow, null);

        flow.OpenMainMenu();
        Check("Open Main Menu", "AgoeWorld_MainMenu");

        flow.OpenLobby();
        Check("Open Lobby", "AgoeWorld_Lobby");

        flow.OpenCharacterSelection();
        Check("Open Character Selection", "AgoeWorld_CharacterSelection");

        flow.StartGameplay();
        Check("Start Gameplay", "AgoeWorld_Gameplay");

        var button = new AgoeWorldNavigationButton();

        button.buttonAction = AgoeWorldButtonAction.OpenLobby;
        button.Navigate();
        Check("PLAY button navigation", "AgoeWorld_Lobby");

        button.buttonAction = AgoeWorldButtonAction.OpenCharacterSelection;
        button.Navigate();
        Check("CHARACTER button navigation", "AgoeWorld_CharacterSelection");

        button.buttonAction = AgoeWorldButtonAction.StartGameplay;
        button.Navigate();
        Check("START MATCH button navigation", "AgoeWorld_Gameplay");

        button.buttonAction = AgoeWorldButtonAction.BackToLobby;
        button.Navigate();
        Check("BACK TO LOBBY button", "AgoeWorld_Lobby");

        button.buttonAction = AgoeWorldButtonAction.BackToMainMenu;
        button.Navigate();
        Check("BACK TO MAIN MENU button", "AgoeWorld_MainMenu");

        Console.WriteLine();
        Console.WriteLine($"RESULT: {passed} passed, {failed} failed");

        if (failed > 0)
            Environment.ExitCode = 1;
    }
}
