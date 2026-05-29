using LabApi.Events.Arguments.Scp0492Events;
using LabApi.Events.CustomHandlers;
using Scp066.ApiFeatures;
using UncomplicatedCustomRoles.Extensions;

namespace Scp066;

public class EventHandler : CustomEventsHandler
{
    public override void OnScp0492StartingConsumingCorpse(Scp0492StartingConsumingCorpseEventArgs ev)
    {
        if (ev.Player.TryGetSummonedInstance(out var role) && role.Role.Id == 066)
            ev.IsAllowed = false;

        base.OnScp0492StartingConsumingCorpse(ev);
    }

    public override void OnServerWaitingForPlayers()
    {
        ApiManager.CheckForUpdates();
        base.OnServerWaitingForPlayers();
    }
}