﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Harmony;
using RimWorld;
using Verse.AI;
using HugsLib.Settings;
using HugsLib;

namespace RunAndGun.Harmony
{

    [HarmonyPatch(typeof(MentalState_PanicFlee), "PostStart")]
    static class MentalState_PanicFlee_PostStart
    {
        static void Postfix(MentalState __instance)
        {
            CompRunAndGun comp = __instance.pawn.TryGetComp<CompRunAndGun>();
            if (comp != null && Settings.enableForAI.Value)
            {
                comp.isEnabled = shouldRunAndGun();

            }
        }
        static bool shouldRunAndGun()
        {
            var rndInt = new Random(DateTime.Now.Millisecond).Next(1, 100);
            ModSettingsPack settings = HugsLibController.SettingsManager.GetModSettings("RunAndGun");
            int chance = settings.GetHandle<int>("enableRGForFleeChance").Value;
            if (rndInt <= chance)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
            
}
