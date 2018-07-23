using Harmony;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TexLevelBypass.Patches
{
    [HarmonyPatch(typeof(MainMenuController))]
    [HarmonyPatch("Perform")]
    class MainMenuController_Perform_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                bool injected = false;
                
                if (instruction.opcode.Equals(OpCodes.Ldc_I4) && instruction.operand.Equals("3") && !injected)
                {
                    injected = true;
                    yield return new CodeInstruction(OpCodes.Ldc_I4, TexLevelBypassSettings.Instance.masterTextureLimit);
                }

                if (instruction.opcode.Equals(OpCodes.Ldstr) && instruction.operand.Equals("Forcing texture limit to 3 due to < 6GB of RAM") && !injected)
                {
                    injected = true;
                    yield return new CodeInstruction(OpCodes.Ldstr, "Bypassing forced texture limit imposed by system having < 6GB of RAM, setting masterTextureLimit to 4.");
                }

                yield return instruction;
            }
        }
    }
}