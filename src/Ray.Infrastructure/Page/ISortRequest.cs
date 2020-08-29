namespace Ray.Infrastructure.Page
{
    /// <summary>
    /// This interface is defined to standardize to request a sorted result.
    /// </summary>
    public interface ISortRequest
    {
        /// <summary>
        /// Sorting information.
        /// Should include sorting field and optionally a direction (ASC or DESC)
        /// Can contain more than one field separated by comma (,).
        /// </summary>
        /// <example>
        /// 比如:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// </example>
        string Sorting { get; set; }
    }
}
