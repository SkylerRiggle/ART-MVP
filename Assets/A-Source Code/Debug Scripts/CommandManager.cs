using System;
using System.Diagnostics;
using System.Collections.Generic;

public class CommandManager
{
    private static List<DebugCommandBase> commandList = DebugCommandList.commandList;
    private static string[] commandNames = new string[commandList.Count];

    private static string invalidInput = "Invalid Input!";
    private static int minInput = 1;
    private static int maxInput = 2;

    public CommandManager()
    {
        //Grab all command names.
        for (int i = 0; i < commandNames.Length; i++)
        {
            commandNames[i] = commandList[i].name;
        }
    }

    public string HandleInput(string[] input) 
    {
        #region DebugCommand Invoke
        if (input.Length == minInput) { //Debug Command with no parameters.
            //Linearly search for a matching command with no parameters.
            foreach (DebugCommandBase entry in commandList) {
                if (entry.name.Equals(input[0])) {
                    //Invoke the command and return its success message.
                    DebugCommand command = entry as DebugCommand;
                    if (command != null) {
                        command.Invoke();
                        return entry.message;
                    }
                }
            }
        }
        #endregion
        #region DebugCommand<T> Invoke
        else if (input.Length <= maxInput) { //Debug Command with one parameter.
            //Linearly search for a matching command with one parameter.
            foreach (DebugCommandBase entry in commandList) {
                if (entry.name.Equals(input[0])) {
                    //Try to invoke the command with the parsed input string and return its success message.
                    try {
                        if ((entry as DebugCommand<bool>) != null) {
                            (entry as DebugCommand<bool>).Invoke(bool.Parse(input[1]));
                            return entry.message + input[1];

                        } else if ((entry as DebugCommand<int>) != null) {
                            (entry as DebugCommand<int>).Invoke(int.Parse(input[1]));
                            return entry.message + input[1];

                        } else if ((entry as DebugCommand<float>) != null) {
                            (entry as DebugCommand<float>).Invoke(float.Parse(input[1]));
                            return entry.message + input[1];

                        } else if ((entry as DebugCommand<string>) != null) {
                            (entry as DebugCommand<string>).Invoke(input[1]);
                            return entry.message + input[1];

                        }
                    } catch (Exception e) {
                        //Otherwise, log an error and return an invalid input message.
                        Debug.WriteLine(e.ToString());
                        return invalidInput;
                    }
                }
            }
        }
        #endregion

        //Otherwise, return an invalid input message.
        return invalidInput;
    }

    public string TryAutocomplete(string input) 
    {
        //Trim each name using the input string and save the index of the smallest result.
        int shortest = int.MaxValue;
        int index = -1;
        for (int i = 0; i < commandNames.Length; i++)
        {
            string name = commandNames[i];
            if (name.Contains(input))
            {
                int length = name.Replace(input, "").Length;
                if (length < shortest)
                {
                    shortest = length;
                    index = i;
                }
            }
        }

        if (index != -1) { return commandNames[index]; } //Match case.
        else { return input; } //No match case.
    }

    public string GetHelpString() 
    {
        //Build a string of help values for displaying the possible commands and what they do.
        string result = "";
        foreach (DebugCommandBase command in commandList) 
            {result += command.name + " : " + command.description + '\n';}
        return result;
    }
}
