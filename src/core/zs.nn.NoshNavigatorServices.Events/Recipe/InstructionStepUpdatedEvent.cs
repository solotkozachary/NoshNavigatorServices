namespace zs.nn.NoshNavigatorServices.Events.Recipe
{
    /// <summary>
    /// The event published when a instruction step is updated.
    /// </summary>
    public class InstructionStepUpdatedEvent : AbstractEvent<InstructionStepUpdatedEvent, Domain.Entity.Recipe.InstructionStep>
    {
        public InstructionStepUpdatedEvent(Domain.Entity.Recipe.InstructionStep entity) : base(entity) { }
    }
}
