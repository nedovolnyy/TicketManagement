using System.Collections.Generic;

namespace TicketManagement.Common.Entities
{
    public abstract class BaseEntity : IEqualityComparer<BaseEntity>
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public int Id { get; protected set; }

        protected abstract void Validate();

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public bool Equals(BaseEntity entity1, BaseEntity entity2)
        {
            if (entity1 == null && entity2 == null)
            {
                return true;
            }

            if (entity1 == null || entity2 == null)
            {
                return false;
            }

            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(BaseEntity obj)
        {
            if (obj is null)
            {
                return 0;
            }

            return obj.Id.GetHashCode();
        }
    }
}
