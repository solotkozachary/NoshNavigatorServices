namespace zs.nn.NoshNavigatorServices.Events.Recipe
{
    /// <summary>
    /// The event published when a instruction step is created.
    /// </summary>
    public class InstructionStepCreatedEvent : AbstractEvent<InstructionStepCreatedEvent, Domain.Entity.Recipe.InstructionStep>
    {
        public InstructionStepCreatedEvent(Domain.Entity.Recipe.InstructionStep entity) : base(entity) { }
    }
}
