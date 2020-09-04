namespace RayPI.Domain.Aggregates.CommentAggregate
{
    public class Comment : BaseAggregateRoot
    {
        protected Comment()
        {

        }

        public Comment(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
