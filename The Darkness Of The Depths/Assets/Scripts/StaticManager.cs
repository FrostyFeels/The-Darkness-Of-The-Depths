using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticManager
{
    public static bool doubleJump = true, wallJump, powerJump, grappleHook = true, slide;
    public static bool shotgun, autoRifle, semiAutoRifle, sniper;
    public static GameObject spawnArea, doubleJumpArea, boostJumpArea, grappleHookArea, wallJumpArea;
    public static GameObject activeArea, secondArea;
    public static bool newArea = false;
    public static int lastLevel;
    public static bool firstspawn = false;
    public static int goldAmount = 1500;
    public static int unlocksLeft = 2;
    public static bool pistol = true;
    public static bool HasBoughAWeapon = false;
    public static float playerHealth = 150f;

    
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

    }
    public static void UnlockWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "Shotgun":
                shotgun = true;
                HasBoughAWeapon = true;
                break;
            case "Sniper":
                sniper = true;
                HasBoughAWeapon = true;
                break;
            case "AutoRifle":
                autoRifle = true;
                HasBoughAWeapon = true;
                break;
            case "SemiAutoRifle":
                semiAutoRifle = true;
                HasBoughAWeapon = true;
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
        Debug.Log(weaponName);
    }


    public static bool CheckWeaponUnlock(string doorName)
    {
        switch (doorName)
        {
            case "Shotgun":
                return shotgun;
            case "Sniper":
                return sniper;
            case "Semi":
                return semiAutoRifle;
            case "Auto":
                return autoRifle;
            case "Pistol":
                return pistol;
            case "Slide":
                return slide;
       
            case "DoubleJump":
                return doubleJump;
         
            case "WallJump":
               return wallJump;
            
            case "PowerJump":
               return powerJump;
              
            case "GrappleHook":
               return grappleHook;
               
        }
        return false;
    }



}
