using System.Collections.Generic;
using static DebugManager;
using static DebugConsole;

public class DebugCommandList
{
    public static List<DebugCommandBase> commandList{get{return _commandList;}}

    private static List<DebugCommandBase> _commandList = new List<DebugCommandBase>() 
    {
        new DebugCommand("help", "Toggling Command Help List", "Displays all possible commands", 
        () => showHelp = !showHelp),

        new DebugCommand<bool>("display_fps", "Set FPS Display to: ", "Toggles the FPS Display to a Passed Boolean Value",
        (bool toggle) => instance.ToggleFPS(toggle)),

        new DebugCommand("display_fps", "Toggling the FPS Display", "Toggles the FPS Display",
        () => instance.ToggleFPS(!instance.displayFPS)),

        new DebugCommand<bool>("fps_cap", "Set FPS Cap to: ", "Toggles the FPS Cap to a Passed Boolean Value",
        (bool toggle) => instance.ToggleFPSCap(toggle)),

        new DebugCommand("fps_cap", "Toggling the FPS Cap", "Toggles the FPS Cap",
        () => instance.ToggleFPSCap(!instance.capFPS)),

        new DebugCommand<int>("set_fps_cap", "Set the Maximum FPS to: ", "Sets the maximum FPS to a Passed Integer Value", 
        (int cap) => instance.ToggleFPSCap(true, cap)),

        new DebugCommand<bool>("display_exposure", "Set the Exposure Display to: ", "Toggles the Visual & Audible Display to a Passed Boolean Value",
            (bool toggle) => instance.ToggleExposure(toggle)),

        new DebugCommand("display_exposure", "Toggling the Exposure Display", "Toggles the Visual & Audible Display",
            () => instance.ToggleExposure(!instance.displayExposure)),
    };
}