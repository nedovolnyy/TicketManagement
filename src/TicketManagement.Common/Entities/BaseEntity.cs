using System.Collections;
using System.Collections.Generic;

namespace TicketManagement.Common.Entities
{
    public abstract class BaseEntity : IEqualityComparer<BaseEntity>
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public int Id { get; protected set; }
        protected abstract string ForEquals(BaseEntity entity);
        protected abstract void Validate();

        public string ForEq(BaseEntity entity)
        {
            return entity.ForEquals(entity);
        }

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

        public bool Equals(BaseEntity entity, BaseEntity tmpEntity)
        {
            if (entity == null && tmpEntity == null)
            {
                return true;
            }

            if (entity == null || tmpEntity == null)
            {
                return false;
            }

            if (entity.ForEquals(entity) == tmpEntity.ForEquals(tmpEntity))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(BaseEntity entity)
        {
            if (entity is null)
            {
                return 0;
            }

            return entity.ForEquals(entity).GetHashCode();
        }
    }
}
