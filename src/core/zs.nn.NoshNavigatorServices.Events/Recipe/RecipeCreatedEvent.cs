namespace zs.nn.NoshNavigatorServices.Events.Recipe
{
    /// <summary>
    /// The event published when a recipe is created.
    /// </summary>
    internal class RecipeCreatedEvent : AbstractEvent<RecipeCreatedEvent, Domain.Entity.Recipe.Recipe>
    {
        public RecipeCreatedEvent(Domain.Entity.Recipe.Recipe entity) : base(entity) { }
    }
}
