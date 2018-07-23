using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace TexLimitBypass.Patches
{
    [HarmonyPatch(typeof(MainMenuController))]
    [HarmonyPatch("Start")]
    class MainMenuController_Start_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var list = instructions.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var instruction = list[i];
                if (instruction.opcode == OpCodes.Ldc_I4 && instruction.operand.Equals(6144))
                {
                    instruction.operand = 0;
                    Console.WriteLine("[TexLimitBypass] Patched");
                }
                yield return instruction;
            };
        }
    }
}