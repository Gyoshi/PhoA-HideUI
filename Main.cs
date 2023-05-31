using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;

namespace HideUI
{
    internal static class Main
    {
        public static Harmony harmony;
        public static UnityModManager.ModEntry.ModLogger logger;

        static void Load(UnityModManager.ModEntry modEntry)
        {
            logger = modEntry.Logger;

            modEntry.OnToggle = OnToggle;

            harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();

            SetHud(false);

        }
        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            SetHud(true);
            harmony.UnpatchAll(modEntry.Info.Id);

            return true;
        }
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool active)
        {
            if (active) { Load(modEntry); }
            else { return Unload(modEntry); }
            return true;
        }

        public static void SetHud(bool visible)
        {
            PT2.hud_heart.OnScreen(visible);
            PT2.hud_stamina.OnScreen(visible);
            PT2.thing_wheel.ToolHudOnScreen(visible);
            CamHudLogic.OnScreen(visible);
        }
    }
}