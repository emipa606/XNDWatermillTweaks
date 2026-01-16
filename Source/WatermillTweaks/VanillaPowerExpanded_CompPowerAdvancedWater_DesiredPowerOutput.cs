using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace WatermillTweaks;

[HarmonyPatch]
public static class VanillaPowerExpanded_CompPowerAdvancedWater_DesiredPowerOutput
{
    public static bool Prepare()
    {
        return ModLister.GetActiveModWithIdentifier("VanillaExpanded.VFEPower", true) != null;
    }

    public static MethodInfo TargetMethod()
    {
        return AccessTools.Method("VanillaPowerExpanded.CompPowerAdvancedWater:get_DesiredPowerOutput");
    }

    public static void Postfix(CompPowerPlant __instance, ref float __result)
    {
        // Season
        __result *= WatermillUtility.SeasonalPowerOutputFactorFor(__instance.parent.GetMapSeason());

        // Outdoor Temperature
        __result *= WatermillUtility.GetTemperatureToPowerOutputFactorCurveFor(__instance.parent)
            .Evaluate(__instance.parent.MapHeld.mapTemperature.OutdoorTemp);

        // Turbulent Waters
        if (__instance.parent.MapHeld.GameConditionManager.ConditionIsActive(BWG_GameConditionDefOf
                .TurbulentWaters))
        {
            __result *= GameCondition_TurbulentWaters.WatermillPowerGenFactor;
        }
    }
}