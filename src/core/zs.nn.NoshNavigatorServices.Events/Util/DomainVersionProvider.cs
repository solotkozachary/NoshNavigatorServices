namespace zs.nn.NoshNavigatorServices.Events.Util
{
    /// <summary>
    /// Service to provide version value for events.
    /// </summary>
    /// <remarks>Event versions will be pulled from the assembly version of the domain, which will represent the state of the domain.</remarks>
    internal static class DomainVersionProvider
    {
        /// <summary>
        /// Get the version of the application domain.
        /// </summary>
        /// <returns></returns>
        internal static string DomainVersion
        {
            get
            {
                var domainAssembly = typeof(Domain.Entity.NoshNavigatorEntity).Assembly;

                var domainAssemblyName = domainAssembly.GetName();

                return domainAssemblyName.Version.ToString();
            }
        }

    }
}
