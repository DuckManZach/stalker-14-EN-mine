using Content.Shared.Trigger.Components.Triggers;

namespace Content.Server._Stalker.Anomaly.Triggers.Systems;

/// <summary>
/// This marks that normal trigger events should be turned into a <see cref="Content.Shared._Stalker.Anomaly.Triggers.Events.STAnomalyTriggerEvent"/>
/// Mainly used as a stopgap so we don't have to refactor all the effects as well
/// </summary>
[RegisterComponent]
public sealed partial class STTriggerConvertComponent : Component;

