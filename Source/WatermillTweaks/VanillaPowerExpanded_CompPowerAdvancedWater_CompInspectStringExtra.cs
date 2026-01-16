using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace WatermillTweaks;

[HarmonyPatch]
public static class VanillaPowerExpanded_CompPowerAdvancedWater_CompInspectStringExtra
{
    public static bool Prepare()
    {
        return ModLister.GetActiveModWithIdentifier("VanillaExpanded.VFEPower", true) != null;
    }

    public static MethodInfo TargetMethod()
    {
        return AccessTools.Method("VanillaPowerExpanded.CompPowerAdvancedWater:CompInspectStringExtra");
    }

    public static void Postfix(CompPowerPlant __instance, ref string __result)
    {
        // Season
        var season = __instance.parent.GetMapSeason();
        var seasonalPowerProductionFactor = WatermillUtility.SeasonalPowerOutputFactorFor(season);

        __result += "\n" + season.LabelCap() + ": x" + seasonalPowerProductionFactor.ToStringPercent();

        // Outdoor Temperature
        var tempPowerProductionFactor = WatermillUtility
            .GetTemperatureToPowerOutputFactorCurveFor(__instance.parent)
            .Evaluate(__instance.parent.MapHeld.mapTemperature.OutdoorTemp);
        if (tempPowerProductionFactor != 1f)
        {
            __result += "\n" + "BadTemperature".Translate().CapitalizeFirst() + ": x" +
                        tempPowerProductionFactor.ToStringPercent();
        }

        var turbulentPowerProductionFactor = GameCondition_TurbulentWaters.WatermillPowerGenFactor;
        if (__instance.parent.MapHeld.GameConditionManager.ConditionIsActive(BWG_GameConditionDefOf
                .TurbulentWaters))
        {
            __result += "\n" + BWG_GameConditionDefOf.TurbulentWaters.label.CapitalizeFirst() + ": x" +
                        turbulentPowerProductionFactor.ToStringPercent();
        }
    }
}