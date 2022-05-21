using System;
using System.Collections.Generic;

namespace TicketManagement.BusinessLogic.DTO
{
    public abstract class BaseDto : IEqualityComparer<BaseDto>
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public Guid Id { get; set; }

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

        public bool Equals(BaseDto entity1, BaseDto entity2)
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

        public int GetHashCode(BaseDto obj)
        {
            if (obj is null)
            {
                return 0;
            }

            return obj.Id.GetHashCode();
        }
    }
}
