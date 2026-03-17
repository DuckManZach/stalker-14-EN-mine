using System.Linq;
using Content.Shared._Stalker.ZoneAnomaly.Components;
using Content.Shared.Whitelist;
using Robust.Shared.Physics.Events;

namespace Content.Shared._Stalker.ZoneAnomaly.Triggers;

public sealed class ZoneAnomalyTriggerCollideSystem : EntitySystem
{
    [Dependency] private readonly ZoneAnomalySystem _anomaly = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelistSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ZoneAnomalyTriggerStartCollideComponent, StartCollideEvent>(OnStartCollide);
        SubscribeLocalEvent<ZoneAnomalyTriggerEndCollideComponent, EndCollideEvent>(OnEndCollide);

        SubscribeLocalEvent<ZoneAnomalyUpdateTriggerCollideComponent, ZoneAnomalyEntityAddEvent>(OnEntityAdd);
        SubscribeLocalEvent<ZoneAnomalyUpdateTriggerCollideComponent, ZoneAnomalyEntityRemoveEvent>(OnEntityRemove);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<ZoneAnomalyComponent, ZoneAnomalyUpdateTriggerCollideComponent>();
        while (query.MoveNext(out var uid, out var anomaly, out var trigger))
        {
            if (trigger.InAnomaly.Count == 0)
                continue;

            _anomaly.TryActivate((uid, anomaly), trigger.InAnomaly);
        }
    }

    // stalker-en-changes - Add and remove update activation instead of update
    // WHEN DOING UPSTREAM MERGES: Make SURE to keep our version of OnEntityAdd and OnEntityRemove
    private void OnEntityAdd(Entity<ZoneAnomalyUpdateTriggerCollideComponent> trigger, ref ZoneAnomalyEntityAddEvent args)
    {
        if (!Validate(args.Entity, trigger, trigger.Comp))
            return;

        if (!trigger.Comp.InAnomaly.Add(args.Entity))
            return;

        TryActivate(args.Entity, (trigger, trigger.Comp));
    }

    private void OnEntityRemove(Entity<ZoneAnomalyUpdateTriggerCollideComponent> trigger, ref ZoneAnomalyEntityRemoveEvent args)
    {
        if (!Validate(args.Entity, trigger, trigger.Comp))
            return;

        if (!trigger.Comp.InAnomaly.Remove(args.Entity))
            return;

        TryActivate(args.Entity, (trigger, trigger.Comp));
    }
    //stalker-en-changes-end

    private void OnStartCollide(Entity<ZoneAnomalyTriggerStartCollideComponent> trigger, ref StartCollideEvent args)
    {
        TryActivate(args.OtherEntity, (trigger, trigger.Comp));
    }

    private void OnEndCollide(Entity<ZoneAnomalyTriggerEndCollideComponent> trigger, ref EndCollideEvent args)
    {
        TryActivate(args.OtherEntity, (trigger, trigger.Comp));
    }

    private void TryActivate(EntityUid target, Entity<ZoneAnomalyTriggerCollideComponent> trigger)
    {
        if (!Validate(target, trigger))
            return;

        if (!TryComp<ZoneAnomalyComponent>(trigger, out var anomaly))
            return;

        _anomaly.TryActivate((trigger, anomaly), target);
    }

    private bool Validate(EntityUid target, EntityUid triggerUid, ZoneAnomalyTriggerCollideComponent component)
    {
        return Validate(target, (triggerUid, component));
    }

    private bool Validate(EntityUid target, Entity<ZoneAnomalyTriggerCollideComponent> trigger)
    {
        return ValidateWhitelist(target, trigger.Comp.Whitelist) &&
               ValidateBlacklist(target, trigger.Comp.Blacklist) &&
               ValidateBlacklist(target, trigger.Comp.BaseBlacklist);
    }

    private bool ValidateWhitelist(EntityUid uid, EntityWhitelist? whitelist)
    {
        return whitelist is null || _whitelistSystem.IsWhitelistPass(whitelist, uid);
    }

    private bool ValidateBlacklist(EntityUid uid, EntityWhitelist? blacklist)
    {
        if (blacklist is null)
            return true;

        return _whitelistSystem.IsWhitelistFail(blacklist, uid);
    }
}
