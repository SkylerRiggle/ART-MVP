using UnityEngine;

public class DebugConsole
{
    private CommandManager commandList = new CommandManager();

    private string input;
    private string feedback = "Awaiting Command";

    private string help;

    private GUIStyle consoleStyle;
    private GUIStyle feedbackStyle;

    public static bool showHelp = false;
    private Vector2 scrollPosition = Vector2.zero;

    public DebugConsole() => help = commandList.GetHelpString();

    public void CreateStyles(Color textColor, Color feedbackColor, Color backgroundColor) 
    {
        //Initialize the new console styles.
        consoleStyle = new GUIStyle();
        feedbackStyle = new GUIStyle();

        //Set the style text properties.
        consoleStyle.normal.textColor = textColor;
        feedbackStyle.normal.textColor = feedbackColor;
        consoleStyle.alignment = TextAnchor.MiddleLeft;
        feedbackStyle.alignment = TextAnchor.MiddleRight;

        //Create a texture for rendering the console background.
        Texture2D backgroundTexture = new Texture2D(1, 1);
        backgroundTexture.SetPixel(0, 0, backgroundColor);
        backgroundTexture.Apply();

        //Set the style background property.
        consoleStyle.normal.background = backgroundTexture;
        feedbackStyle.normal.background = null;

        //Set the style inset padding.
        RectOffset padding = new RectOffset(15, 15, 0, 0);
        consoleStyle.padding = padding;
        feedbackStyle.padding = padding;
    }

    public bool DisplayPasswordPanel(string consolePassword) 
    {
        //Check if the password is correct on return input.
        if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyDown) {
            bool isPassword = input.Equals(consolePassword);
            input = "";
            if (isPassword) {return true;}
        }

        //Grab an x and y value for drawing password panel elements.
        float x = Screen.width / 4f;
        float y = Screen.height / 2f;

        //Draw a box to display the user's current password input.
        GUI.Label(new Rect(x, y - 30f, 2f * x, 30f), "Enter Password:", consoleStyle);
        input = GUI.TextField(new Rect(x, y, 2f * x, 30f), input, consoleStyle);

        //Return false since no valid password has been entered.
        return false;
    }

    public void DisplayConsole() 
    {
        //Check for input to enter or autocomplete a command.
        if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyDown && input.Length > 0) 
            {feedback = commandList.HandleInput(input.Split(' ')); input = "";} //Enter command.
        else if (Event.current.keyCode == KeyCode.Tab && Event.current.type == EventType.KeyDown)
            {input = commandList.TryAutocomplete(input);} //Autocomplete command.

        //Rectangle value used for drawing console elements.
        Rect rectangle = new Rect(0, 0, Screen.width, 30);

        //Display the previous feedback and the current input.
        GUI.Label(rectangle, feedback, feedbackStyle);
        input = GUI.TextField(rectangle, input, consoleStyle);
        
        //Display all possible commands and their descriptions.
        if (showHelp) {
            GUILayout.BeginArea(new Rect(rectangle.x, rectangle.height, rectangle.width, rectangle.height * 4));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(rectangle.width), GUILayout.Height(rectangle.height * 4));
            GUILayout.Label(help, consoleStyle);
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }

    public void DisplayFPS(string fps) => GUI.Label(new Rect(Screen.width - 200f, Screen.height - 30f, 200f, 30f), fps, consoleStyle);

    public void DisplayExposure(string exposure) => GUI.Label(new Rect(0, Screen.height - 60f, 200f, 60f), exposure, consoleStyle);
}
