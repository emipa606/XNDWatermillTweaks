using System.Reflection;
using HarmonyLib;
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