using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using Logger = Rocket.Core.Logging.Logger;

namespace DamageShower
{
    public class DamageShower : RocketPlugin
    {
        public static DamageShower Instance;
        public List<ulong> damage = new List<ulong>();
        static string Fist(ItemJar jar) => jar == null ? "0 AKA Fist" : jar.item.id.ToString();

        protected override void Load()
        {
            Instance = this;
            Logger.Log("This Plugin was made by: [RAR] Metshival", ConsoleColor.Green);
            Logger.Log("Version: 1.0.0", ConsoleColor.Green);
            DamageTool.damagePlayerRequested += DamageTool_damagePlayerRequested;
        }

        private void DamageTool_damagePlayerRequested(ref DamagePlayerParameters pars, ref bool canDamage)
        {
            try
            {
                if (pars.killer != CSteamID.Nil)
                {
                    Player killer = PlayerTool.getPlayer(pars.killer);
                    if (damage.Contains(pars.killer.m_SteamID))
                    {
                        ItemJar item = null;
                        if (killer.equipment.isEquipped)
                            item = killer.inventory.getItem(killer.equipment.equippedPage, killer.inventory.getIndex(killer.equipment.equippedPage, killer.equipment.equipped_x, killer.equipment.equipped_y));
                        UnturnedChat.Say(pars.killer, string.Format("Damage: {0}, Limb: {1}, and Weapon ID: {2}", pars.damage * pars.times, LimbToName(pars.limb), Fist(item)));
                        canDamage = false;
                    }
                }
            }
            catch
            {
            }
        }

        public string LimbToName(ELimb limb)
        {
            string Part = "???";
            if (limb == ELimb.SKULL)
            {
                Part = ("Head");
            }
            else if (limb == ELimb.SPINE)
            {
                Part = ("Body");
            }
            else if (limb == ELimb.LEFT_ARM || limb == ELimb.RIGHT_ARM || limb == ELimb.LEFT_HAND || limb == ELimb.RIGHT_HAND)
            {
                Part = ("Arm");
            }
            else if (limb == ELimb.LEFT_LEG || limb == ELimb.RIGHT_LEG || limb == ELimb.LEFT_FOOT || limb == ELimb.RIGHT_FOOT)
            {
                Part = ("Leg");
            }
            return Part;
        }

        protected override void Unload()
        {
            Logger.Log("This Plugin was made by: [RAR] Metshival", ConsoleColor.Green);
            Logger.Log("Version: 1.0.0", ConsoleColor.Green);
            DamageTool.damagePlayerRequested -= DamageTool_damagePlayerRequested;
        }
    }
}
