using ProtoBuf;
using Sandbox.ModAPI;
using System.IO;
using VRageMath;

namespace Digi.RealisticThrusters
{
    [ProtoContract]
    public class Settings
    {
        public static readonly Settings Default = new Settings()
        {
            defaultRealisticAmount = 1f,
        };
        public static Settings Instance = Default;

        [ProtoMember(1)]
        public float defaultRealisticAmount { get; set; }
    }
    public partial class RealisticThrustersMod
    {
        private void InitConfig()
        {
            Settings s = Settings.Default;
            var Filename = "RealisticThrustersMod.cfg";
            try
            {
                if (MyAPIGateway.Utilities.FileExistsInLocalStorage(Filename, typeof(Settings)))
                {
                    TextReader reader = MyAPIGateway.Utilities.ReadFileInLocalStorage(Filename, typeof(Settings));
                    string text = reader.ReadToEnd();
                    reader.Close();

                    s = MyAPIGateway.Utilities.SerializeFromXML<Settings>(text);
                    Validate(ref s);
                    Save(s);
                }
                else
                {
                    s = Settings.Default;
                    Save(s);
                }
            }
            catch
            {
                Settings.Instance = Settings.Default;
                s = Settings.Default;
                Save(s);
                MyAPIGateway.Utilities.ShowNotification("RealisticThrustersMod: Error with config file, overwriting with default.");
            }
        }

        public static void Validate(ref Settings s)
        {
            s.defaultRealisticAmount = MathHelper.Clamp(s.defaultRealisticAmount, 0f, 1f);
        }
        public static void Save(Settings settings)
        {
            var Filename = "RealisticThrustersMod.cfg";
            try
            {
                TextWriter writer;
                writer = MyAPIGateway.Utilities.WriteFileInLocalStorage(Filename, typeof(Settings));
                writer.Write(MyAPIGateway.Utilities.SerializeToXML(settings));
                writer.Close();
                Settings.Instance = settings;
            }
            catch
            { }
        }
    }
}

