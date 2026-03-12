using Content.Shared._Stalker.Anomaly.Triggers.Events;

namespace Content.Server._Stalker.Anomaly.Triggers.Systems;

public sealed class STAnomalyTriggerSystem : EntitySystem
{
    public void Trigger(EntityUid uid)
    {
        if (!TryComp<STAnomalyComponent>(uid, out var comp))
            return;

        Trigger((uid, comp));
    }

    public void Trigger(Entity<STAnomalyComponent> anomaly)
    {
        var ev = new STAnomalyTriggerEvent();
        RaiseLocalEvent(anomaly, ev);
    }
}
