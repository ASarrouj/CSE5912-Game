using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputState
{
    void Update(PlayerInput.InputType input);
}
