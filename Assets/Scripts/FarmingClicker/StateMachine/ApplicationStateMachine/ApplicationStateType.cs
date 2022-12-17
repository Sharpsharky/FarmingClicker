namespace FarmingClicker.StateMachine.ApplicationStateMachine
{
    public enum ApplicationStateType
    {
        Login = 0,
        Navigation = 1,
        Interaction = 2,
    }
}

// UI: PlsOpenMenuMessage
// MenuManager: ok, wyświetlam, OpenedMenuAchievedMessage
// NavigationManager: ok, pauzuję interakcję i progress gry
// MenuManager: ClosedMenuAchievedMessage
// NavigationManager: ok, odpauzowywuję interakcje i progress gry

// Non-state Managers:   
// Dialogue,
// Onboarding,
// Menu,
// Daily,