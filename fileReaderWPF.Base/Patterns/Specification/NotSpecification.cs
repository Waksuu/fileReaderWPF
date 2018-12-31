namespace fileReaderWPF.Base.Patterns.Specification
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> specification;

        public NotSpecification(ISpecification<T> specification)
        {
            this.specification = specification;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return !this.specification.IsSatisfiedBy(o);
        }
    }
}