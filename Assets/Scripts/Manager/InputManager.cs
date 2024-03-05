using UnityEngine;

public class InputManager : MonoBehaviour
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

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        UpdateKeyboard();
    }

    void UpdateKeyboard()
    {
        inputs.ConfirmButtonPressed = Input.GetKeyDown(KeyCode.Space);

        inputs.SouthButtonPressed = Input.GetKeyDown(KeyCode.Space);
        inputs.EastButtonPressed = Input.GetKeyDown(KeyCode.Backspace) | Input.GetKeyDown(KeyCode.Escape);
        inputs.NorthButtonPressed = Input.GetKeyDown(KeyCode.E);
        inputs.WestButtonPressed = Input.GetKeyDown(KeyCode.R);

        inputs.LeftStickAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputs.LeftDpadButtonPressed = Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow);
        inputs.RightDpadButtonPressed = Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow);
        inputs.UpDpadButtonPressed = Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow);
        inputs.DownDpadButtonPressed = Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow);
    }
}
