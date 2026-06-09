using CustomRoleLib.API.DefaultComponents;
using LabApi.Events.Arguments.Scp0492Events;
using LabApi.Events.Handlers;

namespace Scp066.Features;

public class Scp066BehaviorComponent : ComponentBase<Scp066RoleInstance>
{
    public override void SubscribeEvents(Scp066RoleInstance instance)
    {
        Scp0492Events.StartingConsumingCorpse += GetLabEvent<Scp0492StartingConsumingCorpseEventArgs>(instance, OnStartingConsumingCorpse);
    }

    public override void UnsubscribeEvents(Scp066RoleInstance instance)
    {
        Scp0492Events.StartingConsumingCorpse -= GetLabEvent<Scp0492StartingConsumingCorpseEventArgs>(instance, OnStartingConsumingCorpse);
    }

    private void OnStartingConsumingCorpse(Scp0492StartingConsumingCorpseEventArgs ev, Scp066RoleInstance instance)
    {
        if (ev.Player == instance.Owner) ev.IsAllowed = false;
    }
}
