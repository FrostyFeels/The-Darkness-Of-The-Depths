using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticManager
{
    public static bool doubleJump, wallJump, powerJump, grappleHook, slide;
    public static bool shotgun, autoRifle, semiAutoRifle, sniper;
    public static GameObject spawnArea, doubleJumpArea, boostJumpArea, grappleHookArea, wallJumpArea;
    public static GameObject activeArea, secondArea;
    public static bool newArea = false;
    public static int unlocksLeft = 2;

    
    public static bool SelectDoorType(string doorName)
    {
        switch (doorName)
        {
            case"DoubleJump":
                return doubleJump;
            case "WallJump":
                return wallJump;
            case "BoostJump":
                return powerJump;
            case "GrappleHook":
                return grappleHook;
        }
        return false;
    }
    public static void SetArea(string areaName)
    {
        secondArea = activeArea;
        switch (areaName)
        {
            case "spawnArea":
                activeArea = spawnArea;
                break;
            case "doubleJumpArea":
                activeArea = doubleJumpArea;
                break;
            case "boostJumpArea":
                activeArea = boostJumpArea;
                break;
            case "grappleHookArea":
                activeArea = grappleHookArea;
                break;
            case "wallJumpArea":
                activeArea = wallJumpArea;
                break;
        }          
    }
    public static void SetObject(string areaName, GameObject area)
    {
        secondArea = activeArea;
        switch (areaName)
        {
            case "spawnArea":
                spawnArea = area;
                break;
            case "doubleJumpArea":
                doubleJumpArea = area;
                break;
            case "boostJumpArea":
                boostJumpArea = area;
                break;
            case "grappleHookArea":
                grappleHookArea = area;
                break;
            case "wallJumpArea":
                wallJumpArea = area;
                break;
        }
        Debug.Log("AreaName obj: " + area.name + "AreaName: " + areaName);
    }
    public static void UnlockWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "Shotgun":
                shotgun = true;
                break;
            case "Sniper":
                sniper = true;
                break;
            case "AutoRifle":
                autoRifle = true;
                break;
            case "SemiAutoRifle":
                semiAutoRifle = true;
                break;
            case "Slide":
                slide = true;
                break;
            case "DoubleJump":
                doubleJump = true;
                break;
            case "WallJump":
                wallJump = true;
                break;
            case "PowerJump":
                powerJump = true;
                break;
            case "GrappleHook":
                grappleHook = true;
                break;
        }
    }



}
