using Models.Entities.Notification;
using Models.Entities.Org;
using Models.Entities.Setup;
using Models.Entities.UserManagement;
using Models.Enum; // Add this at the top of the file

namespace Repository.Seeds
{
    public static class SeedData
    {
        public static readonly UserModel[] Users =
        {
            //new UserModel
            //{
            //    Id = 1,
            //    FirstName = "System",
            //    LastName = "User",
            //    FullName = "System User",
            //    UserName = "abc+essadmin",
            //    Email = "abc+anupam.roy88@gmail.com",
            //    PasswordHash = "2t4fql|8Vh9YWwSVHUipYQ==",
            //    RStatus = EnumRStatus.Active,
            //    CreatedDate = DateTime.UtcNow,
            //    CreatedBy = 1
            //},
            new UserModel
            {
                Id = -1,
                FirstName = "Brian",
                LastName = "McLoughlin",
                FullName = "Brian McLoughlin",
                UserName = "bmcloughlin@churchfieldhomeservices.ie",
                Email = "abcd+bmcloughlin@churchfieldhomeservices.ie",
                PasswordHash = "92SHgNOWdyeSC5gLQpmYCw==",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            //new UserModel
            //{
            //    Id = 78,
            //    FirstName = "Emmanuel",
            //    LastName = "Brobbey-Kyei",
            //    FullName = "Emmanuel Brobbey-Kyei",
            //    UserName = "manny@churchfieldhomeservices.ie",
            //    Email = "abc+manny@churchfieldhomeservices.ie",
            //    PasswordHash = "rAfMx53aQ|x7ynT0cvngHw==",
            //    RStatus = EnumRStatus.Active,
            //    CreatedDate = DateTime.UtcNow,
            //    CreatedBy = 1
            //},
            //new UserModel
            //{
            //    Id = 79,
            //    FirstName = "Cian",
            //    LastName = "OSullivan",
            //    FullName = "Cian OSullivan",
            //    UserName = "abc+cian.os@churchfieldhomeservices.ie",
            //    Email = "abc+cian.os@churchfieldhomeservices.ie",
            //    PasswordHash = "2t4fql|8Vh9YWwSVHUipYQ==",
            //    RStatus = EnumRStatus.Active,
            //    CreatedDate = DateTime.UtcNow,
            //    CreatedBy = 1
            //},
            //new UserModel
            //{
            //    Id = 80,
            //    FirstName = "Daniel",
            //    LastName = "Ross",
            //    FullName = "Daniel Ross",
            //    UserName = "abc+dross@churchfieldhomeservices.ie",
            //    Email = "abc+dross@churchfieldhomeservices.ie",
            //    PasswordHash = "2t4fql|8Vh9YWwSVHUipYQ==",
            //    RStatus = EnumRStatus.Active,
            //    CreatedDate = DateTime.UtcNow,
            //    CreatedBy = 1
            //},
            //new UserModel
            //{
            //    Id = 81,
            //    FirstName = "Darragh",
            //    LastName = "Walsh",
            //    FullName = "Darragh Walsh",
            //    UserName = "abc+dwalsh@churchfieldhomeservices.ie",
            //    Email = "abc+dwalsh@churchfieldhomeservices.ie",
            //    PasswordHash = "2t4fql|8Vh9YWwSVHUipYQ==",
            //    RStatus = EnumRStatus.Active,
            //    CreatedDate = DateTime.UtcNow,
            //    CreatedBy = 1
            //},
            //new UserModel
            //{
            //    Id = 82,
            //    FirstName = "Noel",
            //    LastName = "Rowland",
            //    FullName = "Noel Rowland",
            //    UserName = "abc+noel@churchfieldhomeservices.ie",
            //    Email = "abc+noel@churchfieldhomeservices.ie",
            //    PasswordHash = "2t4fql|8Vh9YWwSVHUipYQ==",
            //    RStatus = EnumRStatus.Active,
            //    CreatedDate = DateTime.UtcNow,
            //    CreatedBy = 1
            //},
            //new UserModel
            //{
            //    Id = 83,
            //    FirstName = "Khalid",
            //    LastName = "Bin Awlad",
            //    FullName = "Khalid Bin Awlad",
            //    UserName = "khalid.awlad@efficientsoftwaresolutions.com",
            //    Email = "abc+khalid.awlad@efficientsoftwaresolutions.com",
            //    PasswordHash = "n|Kb6PwNwTfug2W9ZNGq7w==",
            //    RStatus = EnumRStatus.Active,
            //    CreatedDate = DateTime.UtcNow,
            //    CreatedBy = 1
            //}
        };
    }

    public static class CompanySeedData
    {
        public static readonly CompanyModel[] companies =
        {
            new CompanyModel
            {
                Id=1,
                Name = "Churchfield Home Services",
                ShortName = "CHS",
                Description = "Updated Description",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                AccessKey = "CHS",
                SecretKey = "CHS2024!"
            },
            new CompanyModel
            {
                Id=2,
                Name = "OMS",
                ShortName = "OMS",
                Description = "Updated OMS Description",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                AccessKey = "OMS",
                SecretKey = "OMS2024!"
            },
            new CompanyModel
            {
                Id=3,
                Name = "Smart Lotto",
                Description = "Updated Smart Lotto Description",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            }
        };
    }

    public static class CompanyDefineDataSourceSeedData
    {
        public static readonly CompanyDefineDataSourceModel[] companyDefineDataSources =
        {
            new CompanyDefineDataSourceModel
            {
                Id = 1,
                FkCompanyId = 1,
                Source = "api/company-data/users",
                IsSync = false,
                JsonData = null,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                RStatus = EnumRStatus.Active,
                DataSourceType = EnumDataSource.User
            },
            new CompanyDefineDataSourceModel
            {
                Id = 2,
                FkCompanyId = 1,
                Source = "api/company-data/department",
                IsSync = false,
                JsonData = null,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                RStatus = EnumRStatus.Active,
                DataSourceType = EnumDataSource.Department
            },
            new CompanyDefineDataSourceModel
            {
                Id = 3,
                FkCompanyId = 1,
                Source = "api/company-data/projects",
                IsSync = false,
                JsonData = null,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                RStatus = EnumRStatus.Active,
                DataSourceType = EnumDataSource.Project
            },
            new CompanyDefineDataSourceModel
            {
                Id = 4,
                FkCompanyId = 1,
                Source = "api/company-data/customers",
                IsSync = false,
                JsonData = null,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                RStatus = EnumRStatus.Active,
                DataSourceType = EnumDataSource.Customer
            },
            new CompanyDefineDataSourceModel
            {
                Id = 5,
                FkCompanyId = 1,
                Source = "api/company-data/schemes",
                IsSync = false,
                JsonData = null,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                RStatus = EnumRStatus.Active,
                DataSourceType = EnumDataSource.Scheme
            }
        };
    }

    public static class MenuSeedData
    {
        public static readonly MenuModel[] menus =
        {
            new MenuModel
            {
                Name = "DashBoard",
                ParentId = null,
                Url = "#",
                DisplayOrder = 1,
                Route = "/",
                IconClass = "svg-menu-home",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "User Management",
                ParentId = null,
                Url = "#",
                DisplayOrder = 2,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "Company Management",
                ParentId = null,
                Url = "#",
                DisplayOrder = 3,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "RBAC",
                ParentId = null,
                Url = "#",
                DisplayOrder = 4,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
             new MenuModel
            {
                Name = "Ticket Center",
                ParentId = 4,
                Url = "#",
                DisplayOrder = 1,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "Users",
                ParentId = 2,
                Url = "#",
                Route = "/pages/user-management/users/list",
                DisplayOrder = 1,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "Roles",
                ParentId = 2,
                Url = "#",
                Route = "/pages/user-management/roles",
                DisplayOrder = 2,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "Company Config",
                ParentId = 3,
                Url = "#",
                Route = "/pages/company-configuration/list",
                DisplayOrder = 1,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "Issue Prefix",
                ParentId = 3,
                //Url = "pages/tickets",
                Route = "pages/tickets",
                DisplayOrder = 2,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new MenuModel
            {
                Name = "Notification Templates",
                ParentId = 3,
                Url = "#",
                DisplayOrder = 3,
                IconClass = "#",
                IconViewBox = "#",
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            }
        };
    }

    public static class RoleSeedData
    {
        public static readonly RoleModel[] menus =
        {
            new RoleModel
            {
                Name = "Super Admin",
                Description = null,
                HomeUrl = "#",
                IsSuperAdmin = true,
                IsSystemGenerated = true,
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new RoleModel
            {
                Name = "Supervisor",
                Description = "",
                HomeUrl = "#",
                IsSuperAdmin = false,
                IsSystemGenerated = true,
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new RoleModel
            {
                Name = "Company Admin",
                Description = "",
                HomeUrl = "#",
                IsSuperAdmin = false,
                IsSystemGenerated = true,
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            },
            new RoleModel
            {
                Name = "User",
                Description = "",
                HomeUrl = "#",
                IsSuperAdmin = false,
                IsSystemGenerated = true,
                RStatus = EnumRStatus.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            }
        };
    }

    public static class MenuActionSeedData
    {
        public static readonly MenuActionModel[] menuActions =
        {
            new MenuActionModel
            {
                Id = 1,
                Name = "View",
                HttpVerb = "GET",
                Description = "View resource",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionModel
            {
                Id = 2,
                Name = "Add",
                HttpVerb = "POST",
                Description = "Create new resource",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionModel
            {
                Id = 3,
                Name = "Edit",
                HttpVerb = "PUT",
                Description = "Update existing resource",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionModel
            {
                Id = 4,
                Name = "Delete",
                HttpVerb = "DELETE",
                Description = "Remove resource",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionModel
            {
                Id = 5,
                Name = "Patch",
                HttpVerb = "PATCH",
                Description = "Partially update resource",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                 RStatus = EnumRStatus.Active,
            }
        };
    }

    public static class MenuActionMapModelSeedData
    {
        public static readonly MenuActionMapModel[] menuActionMaps =
        {
            new MenuActionMapModel
            {
                FKMenuId = 4,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 4,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 2 ,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 4,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 3 ,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 4,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 4 ,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 4,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 5 ,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 5,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 5,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 2,
                 RStatus = EnumRStatus.Active
            },
            new MenuActionMapModel
            {
                FKMenuId = 5,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 3,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 5,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 4,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 5,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 5,
                 RStatus = EnumRStatus.Active,
            },
             new MenuActionMapModel
            {
                FKMenuId = 6,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 6,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 2,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 6,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 3,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 6,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 4,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 6,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 5,
                 RStatus = EnumRStatus.Active
            }

            ,

                new MenuActionMapModel
            {
                FKMenuId = 7,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 1,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 7,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 2,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 7,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 3,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 7,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 4,
                 RStatus = EnumRStatus.Active,
            },
            new MenuActionMapModel
            {
                FKMenuId = 7,
                ApiUrl = "#",
                RoutePath = "#",
                FKMenuActionId = 5,
                 RStatus = EnumRStatus.Active
            }
        };


    }


    public static class EmailConfigurationSeedData
    {
        public static readonly EmailConfigurationModel[] emailConfigurations =
        {
                new EmailConfigurationModel
                {
                    UserName = "noreply@company.com",
                    Password = "password123",
                    Host = "smtp.company.com",
                    SMTPPort = 587,
                    IMAPPort = 993,
                    AccessKey = "AKIA123456",
                    SecretKey = "SECRET123456",
                                     BCC = new[] { "audit@company.com", "audit@company.com" },
                    CcList = new[] { "audit@company.com", "audit@company.com" },
                    IsDefault = true,
                    Name = "Ticket Created Notification",
                    ReplyTo = "support@company.com",
                    Event = NotificationEvent.Created,
                                       RStatus = EnumRStatus.Active
                },
                new EmailConfigurationModel
                {
                    UserName = "updates@company.com",
                    Password = "password123",
                    Host = "smtp.company.com",
                    SMTPPort = 587,
                    IMAPPort = 993,
                    AccessKey = "AKIA123457",
                    SecretKey = "SECRET123457",
                                     BCC = new[] { "audit@company.com", "audit@company.com" },
                    CcList = new[] { "audit@company.com", "audit@company.com" },

                    IsDefault = false,
                    Name = "Ticket Updated Notification",
                    ReplyTo = "support@company.com",
                    Event = NotificationEvent.Updated,
                                       RStatus = EnumRStatus.Active
                },
                new EmailConfigurationModel
                {
                    UserName = "resolved@company.com",
                    Password = "password123",
                    Host = "smtp.company.com",
                    SMTPPort = 587,
                    IMAPPort = 993,
                    AccessKey = "AKIA123458",
                    SecretKey = "SECRET123458",
                                     BCC = new[] { "audit@company.com", "audit@company.com" },
                    CcList = new[] { "audit@company.com", "audit@company.com" },
                    IsDefault = false,
                    Name = "Ticket Resolved Notification",
                    ReplyTo = "support@company.com",
                    Event = NotificationEvent.Resolved,
                                       RStatus = EnumRStatus.Active
                },
                new EmailConfigurationModel
                {
                    UserName = "closed@company.com",
                    Password = "password123",
                    Host = "smtp.company.com",
                    SMTPPort = 587,
                    IMAPPort = 993,
                    AccessKey = "AKIA123459",
                    SecretKey = "SECRET123459",
                                     BCC = new[] { "audit@company.com", "audit@company.com" },
                    CcList = new[] { "audit@company.com", "audit@company.com" },
                    IsDefault = false,
                    Name = "Ticket Closed Notification",
                    ReplyTo = "support@company.com",
                    Event = NotificationEvent.Closed,
                                       RStatus = EnumRStatus.Active
                },
                new EmailConfigurationModel
                {
                    UserName = "sla@company.com",
                    Password = "password123",
                    Host = "smtp.company.com",
                    SMTPPort = 587,
                    IMAPPort = 993,
                    AccessKey = "AKIA123460",
                    SecretKey = "SECRET123460",
                    BCC = new[] { "audit@company.com", "audit@company.com" },
                    CcList = new[] { "audit@company.com", "audit@company.com" },
                    IsDefault = false,
                    Name = "SLA Due Notification",
                    ReplyTo = "support@company.com",
                    Event = NotificationEvent.SLADue,
                                       RStatus = EnumRStatus.Active
                },
                new EmailConfigurationModel
                {
                    UserName = "slaoverdue@company.com",
                    Password = "password123",
                    Host = "smtp.company.com",
                    SMTPPort = 587,
                    IMAPPort = 993,
                    AccessKey = "AKIA123461",
                    SecretKey = "SECRET123461",
                                     BCC = new[] { "audit@company.com", "audit@company.com" },
                    CcList = new[] { "audit@company.com", "audit@company.com" },
                    IsDefault = false,
                    Name = "SLA Overdue Notification",
                    ReplyTo = "support@company.com",
                    Event = NotificationEvent.SLAOverdue,
                    RStatus = EnumRStatus.Active
                }
            };
    }
    public static class NotificationTemplateModelSeedData
    {
        public static readonly NotificationTemplateModel[] notifications =
            {
        new NotificationTemplateModel
        {
            FkCompanyId = 1,
            Event = NotificationEvent.Created,
            NotificationType = NotificationType.Email,
            EmailConfigurationId = 1,
            SubjectTemplate = "New Ticket Created - {{TicketId}}",
            BodyTemplate = "Hello {{AssigneeName}},<br/>A new ticket ({{TicketId}}) has been created by {{CreatedBy}}.<br/>Subject: {{Subject}}",
            IsEnabled = true,
            Variables = new[] { "TicketId", "AssigneeName", "CreatedBy", "Subject" },
                               RStatus = EnumRStatus.Active
        },
        new NotificationTemplateModel
        {
            FkCompanyId = 1,
            Event = NotificationEvent.Updated,
            NotificationType = NotificationType.Email,
            EmailConfigurationId = 2,
            SubjectTemplate = "Ticket Updated - {{TicketId}}",
            BodyTemplate = "Hello {{AssigneeName}},<br/>Ticket {{TicketId}} has been updated.<br/>Updated By: {{UpdatedBy}}<br/>Remarks: {{Remarks}}",
            IsEnabled = true,
            Variables = new[] { "TicketId", "AssigneeName", "UpdatedBy", "Remarks" },
                               RStatus = EnumRStatus.Active
        },
        new NotificationTemplateModel
        {
            FkCompanyId = 1,
            Event = NotificationEvent.Resolved,
            NotificationType = NotificationType.Email,
            EmailConfigurationId = 3,
            SubjectTemplate = "Ticket Resolved - {{TicketId}}",
            BodyTemplate = "Hello {{CreatedBy}},<br/>Ticket {{TicketId}} has been resolved by {{ResolvedBy}}.<br/>Resolution: {{ResolutionSummary}}",
            IsEnabled = true,
            Variables = new[] { "TicketId", "CreatedBy", "ResolvedBy", "ResolutionSummary" },
                               RStatus = EnumRStatus.Active
        },
        new NotificationTemplateModel
        {
            FkCompanyId = 1,
            Event = NotificationEvent.Closed,
            NotificationType = NotificationType.Email,
            EmailConfigurationId = 4,
            SubjectTemplate = "Ticket Closed - {{TicketId}}",
            BodyTemplate = "Dear {{CreatedBy}},<br/>Your ticket {{TicketId}} has been closed.<br/>Closed By: {{ClosedBy}}",
            IsEnabled = true,
            Variables = new[] { "TicketId", "CreatedBy", "ClosedBy" },
                               RStatus = EnumRStatus.Active
        },
        new NotificationTemplateModel
        {
            FkCompanyId = 1,
            Event = NotificationEvent.SLADue,
            NotificationType = NotificationType.Email,
            EmailConfigurationId = 5,
            SubjectTemplate = "SLA Due - {{TicketId}}",
            BodyTemplate = "Reminder: Ticket {{TicketId}} assigned to {{AssigneeName}} is approaching SLA deadline ({{SLADate}}).",
            IsEnabled = true,
            Variables = new[] { "TicketId", "AssigneeName", "SLADate" },
                               RStatus = EnumRStatus.Active
        },
        new NotificationTemplateModel
        {
            FkCompanyId = 1,
            Event = NotificationEvent.SLAOverdue,
            NotificationType = NotificationType.Email,
            EmailConfigurationId = 6,
            SubjectTemplate = "SLA Overdue - {{TicketId}}",
            BodyTemplate = "Attention: Ticket {{TicketId}} assigned to {{AssigneeName}} has exceeded the SLA limit.<br/>Due Date: {{SLADate}}",
            IsEnabled = true,
            Variables = new[] { "TicketId", "AssigneeName", "SLADate" },
                               RStatus = EnumRStatus.Active
        }
    };
    }
}

