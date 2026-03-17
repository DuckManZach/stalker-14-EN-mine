namespace Content.Shared._Stalker.ZoneAnomaly.Triggers;

/// <summary>
/// Anomalies with this component will trigger when an entity starts colliding with them, and continue triggering until they are no longer colliding
/// </summary>
[RegisterComponent]
public sealed partial class ZoneAnomalyUpdateTriggerCollideComponent : ZoneAnomalyTriggerCollideComponent
{
    [DataField]
    public HashSet<EntityUid> InAnomaly = new();
}
