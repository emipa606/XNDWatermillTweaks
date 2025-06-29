using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace WatermillTweaks;

[StaticConstructorOnStartup]
internal static class HarmonyPatches
{
    static HarmonyPatches()
    {
        new Harmony("XeoNovaDan.WatermillTweaks").PatchAll(Assembly.GetExecutingAssembly());
    }
}

[HarmonyPatch(typeof(CompPowerPlantWater), "DesiredPowerOutput", MethodType.Getter)]
public static class CompPowerPlantWater_DesiredPowerOutput
{
    public static void Postfix(CompPowerPlantWater __instance, ref float __result)
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