namespace TicketManagement.Common.Entities
{
    public class BusinessRule
    {
        public BusinessRule(string property, string rule)
        {
            Property = property;
            Rule = rule;
        }

        public string Property { get; private set; }
        public string Rule { get; private set; }
    }
}
