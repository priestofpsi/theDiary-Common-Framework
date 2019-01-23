using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Syncronization
{
    /// <summary>
    /// Contains the functionality required to identify an entity and its instance.
    /// </summary>
    public interface IEntityInfo
    {
        /// <summary>
        /// Gets the underlying <see cref="Type"/> of the represented entity .
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// Returns a <see cref="String"/> <see cref="Array"/> containing the property names used to identify the entity.
        /// </summary>
        /// <returns>Array of <see cref="String"/> elements containing the names of the properties used to identify the entity.</returns>
        string[] GetEntityIdentifierNames();

        /// <summary>
        /// Returns a <see cref="Object"/> <see cref="Array"/> containing the identifiers of the entity.
        /// </summary>
        /// <param name="entity">The entity to get the identifiers from.</param>
        /// <returns>Array of <see cref="Object"/> values containing the entity identifiers.</returns>
        object[] GetEntityIdentifiers(object entity);        
    }
}
