using Content.Shared._Stalker.Anomaly.Triggers.Events;
using Content.Shared.Trigger;

namespace Content.Server._Stalker.Anomaly.Triggers.Systems;

/// <summary>
/// This marks that normal trigger events should be turned into a <see cref="Content.Shared._Stalker.Anomaly.Triggers.Events.STAnomalyTriggerEvent"/>
/// Mainly used as a stopgap so we don't have to refactor all the effects as well
/// </summary>
public sealed class STTriggerConvertSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<STTriggerConvertComponent, TriggerEvent>(OnTriggered);
    }

    private void OnTriggered(Entity<STTriggerConvertComponent> ent, ref TriggerEvent args)
    {
        var ev = new STAnomalyTriggerEvent();
        RaiseLocalEvent(ent.Owner, ev);
    }
}
