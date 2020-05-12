using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace DamageShower
{
    public class DamageCMD : IRocketCommand
    {
        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "damage.show" };
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public string Name
        {
            get
            {
                return "damage";
            }
        }

        public string Syntax
        {
            get
            {
                return "";
            }
        }

        public string Help
        {
            get
            {
                return "Damage measurement Command";
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>();
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer p = (UnturnedPlayer)caller;
            if (DamageShower.Instance.damage.Contains(p.CSteamID.m_SteamID))
            {
                DamageShower.Instance.damage.Remove(p.CSteamID.m_SteamID);
                UnturnedChat.Say(p, "Damage measurement removed!");
            } else
            {
                DamageShower.Instance.damage.Add(p.CSteamID.m_SteamID);
                UnturnedChat.Say(p, "Damage measurement added!");
            }
            return;
        }
    }
}
