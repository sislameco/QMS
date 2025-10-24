using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "menu");

            migrationBuilder.EnsureSchema(
                name: "log");

            migrationBuilder.EnsureSchema(
                name: "Org");

            migrationBuilder.EnsureSchema(
                name: "org");

            migrationBuilder.EnsureSchema(
                name: "setup");

            migrationBuilder.EnsureSchema(
                name: "notification");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "UserMgmt");

            migrationBuilder.EnsureSchema(
                name: "file");

            migrationBuilder.EnsureSchema(
                name: "issue");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AccessKey = table.Column<string>(type: "text", nullable: true),
                    SecretKey = table.Column<string>(type: "text", nullable: true),
                    PrefixTicket = table.Column<string>(type: "text", nullable: true),
                    LastTicketNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailConfiguration",
                schema: "setup",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Host = table.Column<string>(type: "text", nullable: true),
                    SMTPPort = table.Column<int>(type: "integer", nullable: false),
                    IMAPPort = table.Column<int>(type: "integer", nullable: false),
                    AccessKey = table.Column<string>(type: "text", nullable: true),
                    SecretKey = table.Column<string>(type: "text", nullable: true),
                    BCC = table.Column<string[]>(type: "text[]", nullable: true),
                    CcList = table.Column<string[]>(type: "text[]", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ReplyTo = table.Column<string>(type: "text", nullable: true),
                    Event = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelpDeskAuditLog",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IntegratedCompanyId = table.Column<int>(type: "integer", nullable: false),
                    FkUserId = table.Column<string>(type: "text", nullable: true),
                    IsHost = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Method = table.Column<string>(type: "text", nullable: true),
                    RequestBody = table.Column<string>(type: "text", nullable: true),
                    ResponseBody = table.Column<string>(type: "text", nullable: true),
                    StatusCode = table.Column<int>(type: "integer", nullable: false),
                    LogAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpDeskAuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "menu",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IconClass = table.Column<string>(type: "text", nullable: true),
                    IconViewBox = table.Column<string>(type: "text", nullable: true),
                    Route = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuAction",
                schema: "menu",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    HttpVerb = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplate",
                schema: "notification",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkCompanyId = table.Column<int>(type: "integer", nullable: false),
                    Event = table.Column<int>(type: "integer", nullable: false),
                    NotificationType = table.Column<int>(type: "integer", nullable: false),
                    EmailConfigurationId = table.Column<int>(type: "integer", nullable: true),
                    SubjectTemplate = table.Column<string>(type: "text", nullable: true),
                    BodyTemplate = table.Column<string>(type: "text", nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    Variables = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FkUserId = table.Column<int>(type: "integer", nullable: false),
                    UserIp = table.Column<string>(type: "text", nullable: true),
                    FkLoginId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "UserMgmt",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HomeUrl = table.Column<string>(type: "text", nullable: true),
                    IsSuperAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemGenerated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempFiles",
                schema: "file",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    Extension = table.Column<string>(type: "text", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkUserId = table.Column<int>(type: "integer", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Browser = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    MachineUser = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    LoginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCustomers",
                schema: "org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKCompanyId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    CustomerFirstName = table.Column<string>(type: "text", nullable: true),
                    CustomerLastName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCustomers_Companies_FKCompanyId",
                        column: x => x.FKCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDefineDataSources",
                schema: "Org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkCompanyId = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: true),
                    IsSync = table.Column<bool>(type: "boolean", nullable: false),
                    JsonData = table.Column<string>(type: "text", nullable: true),
                    DataSourceType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDefineDataSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDefineDataSources_Companies_FkCompanyId",
                        column: x => x.FkCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDefineRootResolutions",
                schema: "setup",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKCompanyId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDefineRootResolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDefineRootResolutions_Companies_FKCompanyId",
                        column: x => x.FKCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyProjects",
                schema: "org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKCompanyId = table.Column<int>(type: "integer", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "text", nullable: true),
                    ProjectAddress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyProjects_Companies_FKCompanyId",
                        column: x => x.FKCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuActionMap",
                schema: "menu",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKMenuId = table.Column<int>(type: "integer", nullable: false),
                    ApiUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    RoutePath = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FKMenuActionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuActionMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuActionMap_MenuAction_FKMenuActionId",
                        column: x => x.FKMenuActionId,
                        principalSchema: "menu",
                        principalTable: "MenuAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuActionMap_Menu_FKMenuId",
                        column: x => x.FKMenuId,
                        principalSchema: "menu",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociateActionRoutes",
                schema: "menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkMenuActionMapId = table.Column<int>(type: "integer", nullable: true),
                    ApiUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    HttpVerb = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociateActionRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociateActionRoutes_MenuActionMap_FkMenuActionMapId",
                        column: x => x.FkMenuActionMapId,
                        principalSchema: "menu",
                        principalTable: "MenuActionMap",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuActionRoleMapping",
                schema: "menu",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKRoleId = table.Column<int>(type: "integer", nullable: false),
                    FKMenuActionMapId = table.Column<int>(type: "integer", nullable: false),
                    IsAllowed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuActionRoleMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuActionRoleMapping_MenuActionMap_FKMenuActionMapId",
                        column: x => x.FKMenuActionMapId,
                        principalSchema: "menu",
                        principalTable: "MenuActionMap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuActionRoleMapping_Role_FKRoleId",
                        column: x => x.FKRoleId,
                        principalSchema: "UserMgmt",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "log",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    ActionType = table.Column<int>(type: "integer", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedBy = table.Column<int>(type: "integer", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomFields",
                schema: "Org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkTicketTypeId = table.Column<int>(type: "integer", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    DDLValue = table.Column<string[]>(type: "text[]", nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsMultiSelect = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "Org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IntegrationsPrimaryId = table.Column<int>(type: "integer", nullable: true),
                    FKManagerId = table.Column<int>(type: "integer", nullable: true),
                    FKCompanyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Companies_FKCompanyId",
                        column: x => x.FKCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuActionDepartmentMapping",
                schema: "menu",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkDepartmentId = table.Column<int>(type: "integer", nullable: false),
                    FKMenuActionMapId = table.Column<int>(type: "integer", nullable: false),
                    IsAllowed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuActionDepartmentMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuActionDepartmentMapping_Department_FkDepartmentId",
                        column: x => x.FkDepartmentId,
                        principalSchema: "Org",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuActionDepartmentMapping_MenuActionMap_FKMenuActionMapId",
                        column: x => x.FKMenuActionMapId,
                        principalSchema: "menu",
                        principalTable: "MenuActionMap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UserMgmt",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastPasswordChange = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSystemUser = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsReportingManager = table.Column<bool>(type: "boolean", nullable: false),
                    IntegrationsPrimaryId = table.Column<int>(type: "integer", nullable: true),
                    FkCompanyId = table.Column<int>(type: "integer", nullable: true),
                    FkDepartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Department_FkDepartmentId",
                        column: x => x.FkDepartmentId,
                        principalSchema: "Org",
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecoverPasswordTokens",
                schema: "auth",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkUserId = table.Column<int>(type: "integer", nullable: false),
                    OtpCode = table.Column<string>(type: "text", nullable: true),
                    UserToken = table.Column<string>(type: "text", nullable: true),
                    IsVarified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoverPasswordTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecoverPasswordTokens_Users_FkUserId",
                        column: x => x.FkUserId,
                        principalSchema: "UserMgmt",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    FKAssignedUserId = table.Column<int>(type: "integer", nullable: true),
                    FKDepartmentIds = table.Column<int?[]>(type: "integer[]", nullable: true),
                    FKCompanyId = table.Column<int>(type: "integer", nullable: false),
                    QmsType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketType_Users_FKAssignedUserId",
                        column: x => x.FKAssignedUserId,
                        principalSchema: "UserMgmt",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "UserMgmt",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKUserId = table.Column<int>(type: "integer", nullable: false),
                    FKRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_FKRoleId",
                        column: x => x.FKRoleId,
                        principalSchema: "UserMgmt",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_FKUserId",
                        column: x => x.FKUserId,
                        principalSchema: "UserMgmt",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SLAConfiguration",
                schema: "Org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKTicketTypeId = table.Column<int>(type: "integer", nullable: false),
                    FKCompanyId = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false),
                    ResponseTime = table.Column<int>(type: "integer", nullable: false),
                    ResolutionTime = table.Column<int>(type: "integer", nullable: false),
                    EscalationTime = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLAConfiguration_Companies_FKCompanyId",
                        column: x => x.FKCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SLAConfiguration_TicketType_FKTicketTypeId",
                        column: x => x.FKTicketTypeId,
                        principalSchema: "issue",
                        principalTable: "TicketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<string>(type: "text", nullable: true),
                    TicketNumber = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SubmittedByUserId = table.Column<int>(type: "integer", nullable: false),
                    FKCompanyId = table.Column<int>(type: "integer", nullable: false),
                    FKTicketTypeId = table.Column<int>(type: "integer", nullable: false),
                    TicketCategory = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    AssignedUserId = table.Column<int>(type: "integer", nullable: true),
                    RootCauseId = table.Column<int>(type: "integer", nullable: true),
                    ResolutionId = table.Column<int>(type: "integer", nullable: true),
                    EstimatedTime = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Companies_FKCompanyId",
                        column: x => x.FKCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketType_FKTicketTypeId",
                        column: x => x.FKTicketTypeId,
                        principalSchema: "issue",
                        principalTable: "TicketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSchedule",
                schema: "setup",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKNotificationConfigId = table.Column<int>(type: "integer", nullable: false),
                    FKTicketId = table.Column<int>(type: "integer", nullable: true),
                    Recipient = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    NotificationType = table.Column<int>(type: "integer", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    FKEmailConfigurationId = table.Column<int>(type: "integer", nullable: true),
                    ScheduledTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RetryCount = table.Column<int>(type: "integer", nullable: false),
                    MaxRetryCount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSchedule_EmailConfiguration_FKEmailConfiguratio~",
                        column: x => x.FKEmailConfigurationId,
                        principalSchema: "setup",
                        principalTable: "EmailConfiguration",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationSchedule_Ticket_FKTicketId",
                        column: x => x.FKTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketAttachment",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKTicketId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    FileExtension = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketAttachment_Ticket_FKTicketId",
                        column: x => x.FKTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketComment",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TicketId = table.Column<int>(type: "integer", nullable: false),
                    CommentText = table.Column<string>(type: "text", nullable: true),
                    MentionUserIds = table.Column<int[]>(type: "integer[]", nullable: true),
                    TicketModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketComment_Ticket_TicketModelId",
                        column: x => x.TicketModelId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketCustomerMaps",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKTicketId = table.Column<int>(type: "integer", nullable: false),
                    FkCustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCustomerMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketCustomerMaps_CompanyCustomers_FkCustomerId",
                        column: x => x.FkCustomerId,
                        principalSchema: "org",
                        principalTable: "CompanyCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketCustomerMaps_Ticket_FKTicketId",
                        column: x => x.FKTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketCustomFields",
                schema: "Org",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkTicketId = table.Column<int>(type: "integer", nullable: false),
                    TicketTypeCustomFieldId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCustomFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketCustomFields_CustomFields_TicketTypeCustomFieldId",
                        column: x => x.TicketTypeCustomFieldId,
                        principalSchema: "Org",
                        principalTable: "CustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketCustomFields_Ticket_FkTicketId",
                        column: x => x.FkTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketDepartmentMap",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKTicketId = table.Column<int>(type: "integer", nullable: false),
                    FKDepartmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDepartmentMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketDepartmentMap_Department_FKDepartmentId",
                        column: x => x.FKDepartmentId,
                        principalSchema: "Org",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketDepartmentMap_Ticket_FKTicketId",
                        column: x => x.FKTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketLink",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKTicketId = table.Column<int>(type: "integer", nullable: false),
                    ExternalKey = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketLink_Ticket_FKTicketId",
                        column: x => x.FKTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketProjectMaps",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKTicketId = table.Column<int>(type: "integer", nullable: false),
                    FkProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketProjectMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketProjectMaps_CompanyProjects_FkProjectId",
                        column: x => x.FkProjectId,
                        principalSchema: "org",
                        principalTable: "CompanyProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketProjectMaps_Ticket_FKTicketId",
                        column: x => x.FKTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketWatchList",
                schema: "issue",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FKTicketId = table.Column<int>(type: "integer", nullable: false),
                    FKUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketWatchList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketWatchList_Ticket_FKTicketId",
                        column: x => x.FKTicketId,
                        principalSchema: "issue",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketWatchList_Users_FKUserId",
                        column: x => x.FKUserId,
                        principalSchema: "UserMgmt",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociateActionRoutes_FkMenuActionMapId",
                schema: "menu",
                table: "AssociateActionRoutes",
                column: "FkMenuActionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UserId",
                schema: "log",
                table: "AuditLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCustomers_FKCompanyId",
                schema: "org",
                table: "CompanyCustomers",
                column: "FKCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDefineDataSources_FkCompanyId",
                schema: "Org",
                table: "CompanyDefineDataSources",
                column: "FkCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDefineRootResolutions_FKCompanyId",
                schema: "setup",
                table: "CompanyDefineRootResolutions",
                column: "FKCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProjects_FKCompanyId",
                schema: "org",
                table: "CompanyProjects",
                column: "FKCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFields_FkTicketTypeId",
                schema: "Org",
                table: "CustomFields",
                column: "FkTicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_FKCompanyId",
                schema: "Org",
                table: "Department",
                column: "FKCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_FKManagerId",
                schema: "Org",
                table: "Department",
                column: "FKManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionDepartmentMapping_FkDepartmentId",
                schema: "menu",
                table: "MenuActionDepartmentMapping",
                column: "FkDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionDepartmentMapping_FKMenuActionMapId",
                schema: "menu",
                table: "MenuActionDepartmentMapping",
                column: "FKMenuActionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionMap_FKMenuActionId",
                schema: "menu",
                table: "MenuActionMap",
                column: "FKMenuActionId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionMap_FKMenuId",
                schema: "menu",
                table: "MenuActionMap",
                column: "FKMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionRoleMapping_FKMenuActionMapId",
                schema: "menu",
                table: "MenuActionRoleMapping",
                column: "FKMenuActionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionRoleMapping_FKRoleId",
                schema: "menu",
                table: "MenuActionRoleMapping",
                column: "FKRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSchedule_FKEmailConfigurationId",
                schema: "setup",
                table: "NotificationSchedule",
                column: "FKEmailConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSchedule_FKTicketId",
                schema: "setup",
                table: "NotificationSchedule",
                column: "FKTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_RecoverPasswordTokens_FkUserId",
                schema: "auth",
                table: "RecoverPasswordTokens",
                column: "FkUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAConfiguration_FKCompanyId",
                schema: "Org",
                table: "SLAConfiguration",
                column: "FKCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SLAConfiguration_FKTicketTypeId",
                schema: "Org",
                table: "SLAConfiguration",
                column: "FKTicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_FKCompanyId",
                schema: "issue",
                table: "Ticket",
                column: "FKCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_FKTicketTypeId",
                schema: "issue",
                table: "Ticket",
                column: "FKTicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachment_FKTicketId",
                schema: "issue",
                table: "TicketAttachment",
                column: "FKTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_TicketModelId",
                schema: "issue",
                table: "TicketComment",
                column: "TicketModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCustomerMaps_FkCustomerId",
                schema: "issue",
                table: "TicketCustomerMaps",
                column: "FkCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCustomerMaps_FKTicketId",
                schema: "issue",
                table: "TicketCustomerMaps",
                column: "FKTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCustomFields_FkTicketId",
                schema: "Org",
                table: "TicketCustomFields",
                column: "FkTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCustomFields_TicketTypeCustomFieldId",
                schema: "Org",
                table: "TicketCustomFields",
                column: "TicketTypeCustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartmentMap_FKDepartmentId",
                schema: "issue",
                table: "TicketDepartmentMap",
                column: "FKDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartmentMap_FKTicketId",
                schema: "issue",
                table: "TicketDepartmentMap",
                column: "FKTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketLink_FKTicketId",
                schema: "issue",
                table: "TicketLink",
                column: "FKTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjectMaps_FkProjectId",
                schema: "issue",
                table: "TicketProjectMaps",
                column: "FkProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjectMaps_FKTicketId",
                schema: "issue",
                table: "TicketProjectMaps",
                column: "FKTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_FKAssignedUserId",
                schema: "issue",
                table: "TicketType",
                column: "FKAssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketWatchList_FKTicketId",
                schema: "issue",
                table: "TicketWatchList",
                column: "FKTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketWatchList_FKUserId",
                schema: "issue",
                table: "TicketWatchList",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_FKRoleId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "FKRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_FKUserId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "FKUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FkDepartmentId",
                schema: "UserMgmt",
                table: "Users",
                column: "FkDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_Users_UserId",
                schema: "log",
                table: "AuditLog",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomFields_TicketType_FkTicketTypeId",
                schema: "Org",
                table: "CustomFields",
                column: "FkTicketTypeId",
                principalSchema: "issue",
                principalTable: "TicketType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Users_FKManagerId",
                schema: "Org",
                table: "Department",
                column: "FKManagerId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Users_FKManagerId",
                schema: "Org",
                table: "Department");

            migrationBuilder.DropTable(
                name: "AssociateActionRoutes",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "log");

            migrationBuilder.DropTable(
                name: "CompanyDefineDataSources",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "CompanyDefineRootResolutions",
                schema: "setup");

            migrationBuilder.DropTable(
                name: "HelpDeskAuditLog",
                schema: "log");

            migrationBuilder.DropTable(
                name: "MenuActionDepartmentMapping",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "MenuActionRoleMapping",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "NotificationSchedule",
                schema: "setup");

            migrationBuilder.DropTable(
                name: "NotificationTemplate",
                schema: "notification");

            migrationBuilder.DropTable(
                name: "RecoverPasswordTokens",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "log");

            migrationBuilder.DropTable(
                name: "SLAConfiguration",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "TempFiles",
                schema: "file");

            migrationBuilder.DropTable(
                name: "TicketAttachment",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "TicketComment",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "TicketCustomerMaps",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "TicketCustomFields",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "TicketDepartmentMap",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "TicketLink",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "TicketProjectMaps",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "TicketWatchList",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "log");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "UserMgmt");

            migrationBuilder.DropTable(
                name: "MenuActionMap",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "EmailConfiguration",
                schema: "setup");

            migrationBuilder.DropTable(
                name: "CompanyCustomers",
                schema: "org");

            migrationBuilder.DropTable(
                name: "CustomFields",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "CompanyProjects",
                schema: "org");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "UserMgmt");

            migrationBuilder.DropTable(
                name: "MenuAction",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "TicketType",
                schema: "issue");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "UserMgmt");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Org");
        }
    }
}
