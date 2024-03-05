using UnityEngine;

public class InputManager
{

    public struct Inputs
    {
        public bool ConfirmButtonPressed;

        public bool SouthButtonPressed;
        public bool EastButtonPressed;
        public bool NorthButtonPressed;
        public bool WestButtonPressed;

        public Vector2 LeftStickAxis;
        public bool LeftDpadButtonPressed;
        public bool RightDpadButtonPressed;
        public bool UpDpadButtonPressed;
        public bool DownDpadButtonPressed;
    }

    Inputs inputs;

    public Inputs GetInputs()
    {
        return inputs;
    }

    internal void UpdateLogic(int playerIndex)
    {
        Debug.Log($"playerIndex: {playerIndex}");
        if (playerIndex == 0)
            UpdateKeyboardPlayer0();
        else
            UpdateKeyboardPlayer1();
    }

    void UpdateKeyboardPlayer1()
    {
        inputs.SouthButtonPressed = Input.GetKeyDown(KeyCode.UpArrow);
    }

    void UpdateKeyboardPlayer0()
    {
        inputs.SouthButtonPressed = Input.GetKeyDown(KeyCode.Space);
        inputs.NorthButtonPressed = Input.GetKeyDown(KeyCode.E);
        inputs.WestButtonPressed = Input.GetKeyDown(KeyCode.R);

    }
}
