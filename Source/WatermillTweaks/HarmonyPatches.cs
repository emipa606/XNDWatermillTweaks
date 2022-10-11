using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace WatermillTweaks;

[StaticConstructorOnStartup]
internal static class HarmonyPatches
{
    private static readonly Type patchType = typeof(HarmonyPatches);

    static HarmonyPatches()
    {
        var h = new Harmony("XeoNovaDan.WatermillTweaks");

        h.Patch(AccessTools.Property(typeof(CompPowerPlantWater), "DesiredPowerOutput").GetGetMethod(true), null,
            new HarmonyMethod(patchType, nameof(PostfixDesiredPowerOutput)));

        h.Patch(AccessTools.Method(typeof(CompPowerPlantWater), nameof(CompPowerPlantWater.CompInspectStringExtra)),
            null,
            new HarmonyMethod(patchType, nameof(PostfixCompInspectStringExtra)));
    }

    public static void PostfixDesiredPowerOutput(CompPowerPlantWater __instance, ref float __result)
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

    public static void PostfixCompInspectStringExtra(CompPowerPlantWater __instance, ref string __result)
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