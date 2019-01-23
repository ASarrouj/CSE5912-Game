using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //num = 0: StartState
    //num = 1: Loading State
    //num = 2: Main Menu State
    //num = 3: Play Pong State
    public static void LoadState(int num) {

        LoadingScreenManager.LoadScene(num);

    }

}
