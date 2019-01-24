using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    //num = 0: Start Scene
    //num = 1: Loading Scene
    //num = 2: Main Menu Scene
    //num = 3: Play Pong Scene
    //num = 4: Credit Scene
    //num = 5: Options Menu
    public static void LoadState(int num) {

        LoadingScreenManager.LoadScene(num);

    }

    public static void QuickLoadState(int num)
    {

        SceneManager.LoadScene(num);

    }

}
