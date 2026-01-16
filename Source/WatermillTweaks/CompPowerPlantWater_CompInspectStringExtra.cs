using HarmonyLib;
using RimWorld;
using Verse;

namespace WatermillTweaks;

[HarmonyPatch(typeof(CompPowerPlantWater), nameof(CompPowerPlantWater.CompInspectStringExtra))]
public static class CompPowerPlantWater_CompInspectStringExtra
{
    public static void Postfix(CompPowerPlantWater __instance, ref string __result)
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