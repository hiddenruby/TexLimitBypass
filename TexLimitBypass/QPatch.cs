using Harmony;
using System;
using System.Reflection;

namespace TexLimitBypass
{
    class QPatch
    {
        public static void Patch()
        {
            Console.WriteLine("[TexLimitBypass] Started");
            try
            {
                HarmonyInstance harmony = HarmonyInstance.Create("texlimitbypass.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                FileLog.Log($"Unable to create patch! Exception was thrown!\n{PrintException(e)}");
            }
        }

        private static string PrintException(Exception e)
        {
            if (e == null)
                return string.Empty;
            return $"\"Exception: {e.GetType()}\"\n\tMessage: {e.Message}\n\tStacktrace: {e.StackTrace}\n" +
                   PrintException(e.InnerException);
        }
    }
}
