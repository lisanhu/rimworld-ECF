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
                if (ToggleIconPatcher.Enable && __instance.DebugTryFormCaravanInstantly())
                {
                    __instance.Close();
                    return false;
                }
            }
            return true;
        }
    }
    
    [HarmonyPatch(typeof(PlaySettings), "DoPlaySettingsGlobalControls", MethodType.Normal)]
	public class ToggleIconPatcher
	{
        public static bool Enable = false;
        [HarmonyPostfix]
        public static void AddIcon(WidgetRow row, bool worldView)
        {
            if (worldView) return;
            // bool flag = Find.WindowStack.IsOpen(typeof(TimerSetWindow));
            // row.ToggleableIcon(ref flag, ContentFinder<Texture2D>.Get("UI/timer_mail", true), "AlertUtility".Translate(), SoundDefOf.Mouseover_ButtonToggle, (string)null);
            // if (flag != Find.WindowStack.IsOpen(typeof(TimerSetWindow)))
            // {
            // 	if (!Find.WindowStack.IsOpen(typeof(TimerSetWindow)))
            // 	{
            // 		TimerSetWindow.DrawWindow(AlertUtility.GetEvents());
            // 	}
            // 	else
            // 	{
            // 		Find.WindowStack.TryRemove(typeof(TimerSetWindow), false);
            // 	}
            // }
            row.ToggleableIcon(ref Enable, ContentFinder<Texture2D>.Get("ECF/caravan", true), "ECF.IconTooltip".Translate());
		}
	}
}
