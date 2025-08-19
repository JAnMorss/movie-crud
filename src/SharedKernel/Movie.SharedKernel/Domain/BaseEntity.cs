namespace Movie.SharedKernel.Domain
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected BaseEntity() { }
        protected BaseEntity(Guid id)
        {
            Id = id;
            CreateDate = DateTime.UtcNow;
        }
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents()
            => _domainEvents.Clear();


        protected void RaiseDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);
    }
}
