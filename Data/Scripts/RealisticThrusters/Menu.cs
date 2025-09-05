using Draygo.API;
using VRageMath;

namespace Digi.RealisticThrusters
{
    public partial class RealisticThrustersMod
    {
        HudAPIv2.MenuRootCategory SettingsMenu;
        HudAPIv2.MenuSliderInput DefaultRealisticAmount;

        private void InitMenu() // entrypoint from RealisticThrustersMod.BeforeStart
        {
            SettingsMenu = new HudAPIv2.MenuRootCategory("Realistic Thrusters", HudAPIv2.MenuRootCategory.MenuFlag.PlayerMenu, "Realistic Thrusters Settings");
            DefaultRealisticAmount = new HudAPIv2.MenuSliderInput("Default Offset Thrust Amount", SettingsMenu, Settings.Instance.defaultRealisticAmount, "Adjust Default Offset Thrust Amount", ChangeDefaultRealisticAmount);
        }

        private void ChangeDefaultRealisticAmount(float val)
        {
            Settings.Instance.defaultRealisticAmount = val;
            Save(Settings.Instance);
        }
    }
}

