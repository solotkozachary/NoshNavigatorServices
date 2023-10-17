namespace zs.nn.NoshNavigatorServices.Events.Recipe
{
    /// <summary>
    /// The event published when a ingredient is created.
    /// </summary>
    public class IngredientCreatedEvents : AbstractEvent<IngredientCreatedEvents, Domain.Entity.Recipe.Ingredient>
    {
        public IngredientCreatedEvents(Domain.Entity.Recipe.Ingredient entity) : base(entity) { }
    }
}
