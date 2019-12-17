using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using Logger = Rocket.Core.Logging.Logger;

namespace DamageShower
{
    public class DamageShower : RocketPlugin
    {
        public static string version = "1.0.0";
        public static string maker = "[RAR] Metshival";
        public static DamageShower Instance;
        public List<string> damage = new List<string>();

        protected override void Load()
        {
            Instance = this;
            Logger.Log("This Plugin was made by: " + maker);
            Logger.Log("Version: " + version);
            DamageTool.damagePlayerRequested += UnturnedEvents_OnPlayerDamaged;
        }

        private void UnturnedEvents_OnPlayerDamaged(ref DamagePlayerParameters pars, ref bool canDamage)
        {
            try
            {
                UnturnedPlayer player = UnturnedPlayer.FromPlayer(pars.player);
                UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(pars.killer);
                if (damage.Contains(killer.Id))
                {

                    PlayerEquipment pe = killer.Player.equipment;
                    Item item = killer.Inventory.getItem(pe.equippedPage, player.Inventory.getIndex(pe.equippedPage, pe.equipped_x, pe.equipped_y)).item;
                    UnturnedChat.Say(killer, "Damage done: " + (pars.damage * pars.times) + ", Damaged Limb: " + LimbToName(pars.limb) + " and Weapon ID: " + item.id);
                    canDamage = false;

                }
            }
            catch (Exception) { }
        }

        public string LimbToName(ELimb limb)
        {
            string Part = "???";
            if (limb == ELimb.SKULL)
            {
                Part = ("head");
            }
            else if (limb == ELimb.SPINE)
            {
                Part = ("body");
            }
            else if (limb == ELimb.LEFT_ARM || limb == ELimb.RIGHT_ARM || limb == ELimb.LEFT_HAND || limb == ELimb.RIGHT_HAND)
            {
                Part = ("arm");
            }
            else if (limb == ELimb.LEFT_LEG || limb == ELimb.RIGHT_LEG || limb == ELimb.LEFT_FOOT || limb == ELimb.RIGHT_FOOT)
            {
                Part = ("leg");
            }
            return Part;
        }

        protected override void Unload()
        {
            Logger.Log("This Plugin was made by: " + maker);
            Logger.Log("Version: " + version);
            DamageTool.damagePlayerRequested -= UnturnedEvents_OnPlayerDamaged;
        }
    }
}