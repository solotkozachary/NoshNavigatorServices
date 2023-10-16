namespace zs.nn.NoshNavigatorServices.Application.Enumerations
{
    /// <summary>
    /// Values that express types of application rules.
    /// </summary>
    public enum ApplicationRules
    {
        /// <summary>
        /// Invalid default value.
        /// </summary>
        Default,

        /// <summary>
        /// Rule that indicates that an entity must exist.
        /// </summary>
        MustExist,

        /// <summary>
        /// Rule that indicates that an entity must not exist.
        /// </summary>
        MustNotExist,

        /// <summary>
        /// A rule that indicates that an entity must be valid.
        /// </summary>
        MustBeValid,

        /// <summary>
        /// Rule that indicates an entity must initialize.
        /// </summary>
        MustInitialize
    }
}
