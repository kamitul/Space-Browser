namespace SBDataLibrary.Models
{
    /// <summary>
    /// Abstract class for each entity
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Sets entity data
        /// </summary>
        /// <param name="data">Passed data</param>
        public abstract void Set(params object[] data);
        /// <summary>
        /// Gets fields as string
        /// </summary>
        /// <returns>Fields names as array</returns>
        public abstract string[] GetFields();
    }
}
