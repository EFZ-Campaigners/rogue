using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : Singleton<DebugController>
{
    bool showConsole;
    bool showHelp;

    string input;

    public static DebugCommand SHUT_DOWN;
    public static DebugCommand TIME_STOP;
    public static DebugCommand HELP;
    public static DebugCommand<float> SET_TIME_SCALE;
    public List<object> commandList;

    private void Update()
    {

    }

    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }

    protected override void Awake()
    {
        SHUT_DOWN = new DebugCommand("shut_down", "Quits the game", "shut_down", () =>
        {
            Application.Quit();
        });

        TIME_STOP = new DebugCommand("time_stop", "Stops time", "time_stop", () =>
        {
            Time.timeScale = 0;
        });

        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = true;
        });

        SET_TIME_SCALE = new DebugCommand<float>("set_time_scale", "Sets the value of time scale", "set_time_scale <time scale value>", (x) =>
        {
            Time.timeScale = x;
        });

        commandList = new List<object>{
            SHUT_DOWN,
            TIME_STOP,
            SET_TIME_SCALE,
            HELP
        };
    }

    Vector2 scroll;

    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (!input.Contains(commandBase.commandID)) continue;

            if (commandList[i] as DebugCommand != null)
            {
                //Cast to this type and invoke the command
                (commandList[i] as DebugCommand).Invoke();
            }
            else if (commandList[i] as DebugCommand<float> != null)
            {
                (commandList[i] as DebugCommand<float>).Invoke(float.Parse(properties[1]));
            }



        }
    }
}
