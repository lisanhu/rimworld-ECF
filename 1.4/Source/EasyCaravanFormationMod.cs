using HarmonyLib;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace EasyCaravanFormation
{
    public class EasyCaravanFormationMod : Mod
    {
        public EasyCaravanFormationMod(ModContentPack pack) : base(pack)
        {
			new Harmony("EasyCaravanFormationMod").PatchAll();
        }
    }

    [HarmonyPatch(typeof(Dialog_FormCaravan), "TrySend")]
    public static class Dialog_FormCaravan_TrySend_Patch
    {
        public static bool Prefix(Dialog_FormCaravan __instance)
        {
            if (GenHostility.AnyHostileActiveThreatToPlayer(__instance.map) is false)
            {
                if (__instance.DebugTryFormCaravanInstantly())
                {
                    __instance.Close();
                    return false;
                }
            }
            return true;
        }
    }
}
