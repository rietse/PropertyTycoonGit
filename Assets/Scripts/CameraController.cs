using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera, player1Camera, player2Camera, player3Camera, player4Camera, player5Camera;
    public int currentPlayer;
    private bool playerCameraActive = false;

    public void InitialiseCameras()
    {
        //ensures we start on the main camera, disables the rest then goes to the current player view
        mainCamera.enabled = true; 
        player1Camera.enabled = false;
        player2Camera.enabled = false;
        player3Camera.enabled = false;
        player4Camera.enabled = false;
        player5Camera.enabled = false;

        SwitchCameraView();
    }

    //Selects the player that the camera will follow
    public void SetCurrentPlayer(int player)
    {
        currentPlayer = player;
    }

    //Enables and Disables the cameras attached to each player in order to change the camera view to focus on the intended player
    public void SwitchCameraPlayer()
    {
        switch (currentPlayer)
        {
            case 1:
                player1Camera.enabled = true;
                player5Camera.enabled = false;
                break;
            case 2:
                player2Camera.enabled = true;
                player1Camera.enabled = false;
                break;
            case 3:
                player3Camera.enabled = true;
                player2Camera.enabled = false;
                break;
            case 4:
                player4Camera.enabled = true;
                player3Camera.enabled = false;
                break;
            case 5:
                player5Camera.enabled = true;
                player4Camera.enabled = false;
                break;
            default:
                print("uuh the camera done goofed!");
                break;
        }
    }

    //Changes the camera view between the main, zoomed out view to a camera view following an individual player
    public void SwitchCameraView()
    {
        if (playerCameraActive == true)
        {
            mainCamera.enabled = true;
            switch (currentPlayer)
            {
                case 1:
                    player1Camera.enabled = false;
                    break;
                case 2:
                    player2Camera.enabled = false;
                    break;
                case 3:
                    player3Camera.enabled = false;
                    break;
                case 4:
                    player4Camera.enabled = false;
                    break;
                case 5:
                    player5Camera.enabled = false;
                    break;
                default:
                    print("uuh the camera done goofed!");
                    break;
            }
        }
        else
        {
            switch(currentPlayer)
            {
                case 1:
                    player1Camera.enabled = true;
                    break;
                case 2:
                    player2Camera.enabled = true;
                    break;
                case 3:
                    player3Camera.enabled = true;
                    break;
                case 4:
                    player4Camera.enabled = true;
                    break;
                case 5:
                    player5Camera.enabled = true;
                    break;
                default:
                    print("uuh the camera done goofed!");
                    break;
            }
            mainCamera.enabled = false;
        }
        playerCameraActive = !playerCameraActive;
    }
}
