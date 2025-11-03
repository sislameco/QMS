using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.AuditLog
{
    public static class AuditHelper
    {

        // ✅ List of auditable actions by controller
        private static readonly Dictionary<string, string[]> _auditMap = new()
    {
        { "Ticket", new[] { "UpdateBasicDetails", "ChangeTicketStatus", "UpdateSpecification" } },
    };

        // ✅ Checks if current request is auditable
        public static bool IsAuditable(string? controllerName, string? actionName)
        {
            if (string.IsNullOrWhiteSpace(controllerName) || string.IsNullOrWhiteSpace(actionName))
                return false;

            if (_auditMap.TryGetValue(controllerName, out var actions))
                return actions.Any(a => string.Equals(a, actionName, StringComparison.OrdinalIgnoreCase));

            return false;
        }
    }
}
