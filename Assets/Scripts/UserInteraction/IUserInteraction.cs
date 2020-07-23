/// <summary>
/// Interface for any object that needs User Interaction
/// </summary>
public interface IUserInteraction {

    System.Collections.Generic.List<string> getAvailableActions();
    bool RunAction(string action);
}
