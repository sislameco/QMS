# Help Desk Full Requirements Document  

## Contents  
- Overview  
- Scope  
- Out of Scope  
- QMS Admin Panel  
  - User Management  
- Helpdesk (Ticket Centre)  
  - Kanban View  
  - Table View  
  - Create Ticket  
  - View & Edit Ticket  
- Customer Complaints  
  - Kanban View  
  - Table View  
  - Create Customer Complaint  
  - Customer Complaint Fields  
- CAPA  
  - Kanban View  
  - Table View  
  - CAPA Fields  
- QMS Company Setup  
  - Company Setup  
  - Ticket Type Setup  
  - Customer Complaint Setup  
  - CAPA Setup  
  - Root Cause Setup  
  - Resolution Setup  
  - Company Defined Fields  
- Qsmart QMS Integration  
  - Qsmart Frontend  
    - Create Ticket  
    - My Tickets (User Ticket Centre)  
  - Qsmart Admin (Backoffice)  
    - Quality Menu  
    - Department Menu  
- SLA’s  
- Notifications  

---

## Overview  
The Help Desk provides structured support management within the Quality Management System (QMS), ensuring that issues are logged, tracked, and resolved efficiently. Users submit tickets through the ERP frontend, which are routed to the Ticket Centre and departmental queues in the ERP back office.  

Customer Complaints are logged and managed separately, enabling formal complaint resolution workflows. Corrective and Preventive Actions (CAPA) extend this by allowing tickets or complaints to be escalated into structured root cause investigations and preventative actions.  

The QMS operates as a standalone SaaS platform, integrated initially with Churchfield’s Qsmart ERP. Future milestones will extend functionality with Risk Register and Goals modules, enabling managers to link risks, goals, tickets, complaints, and CAPA items into a single quality framework.  

---

## Scope  
Scope for this development is to deliver a standalone QMS platform and connect it to the Churchfield ERP (Qsmart). The scope includes:  

- **QMS Admin Panel**  
  - Helpdesk (Ticket Centre)  
  - Customer Complaints  
  - CAPA  
  - Company setup (ticket types, SLAs, notifications, root causes, resolutions, defined fields)  

- **Qsmart Connections**  
  - Create and view tickets in the Ticket Centre  
  - Create and view Customer Complaints in the register  
  - Create and view CAPA items in the CAPA register  

- **Qsmart QMS Integration**  
  - Qsmart Frontend (ticket creation, My Tickets iframe)  
  - Qsmart Admin (Ticket Centre in all department menus, Complaints in Quality & Customer Care menus, CAPA in Quality menu)  

- **SLA Management** per ticket type, complaint, and CAPA priority  
- **Notifications** for all item types, configurable with push/email options  

---

## Out of Scope  
- Risk Register (future milestone)  
- Goals module (future milestone)  
- Integration with non-Qsmart ERPs (Materials Direct, Smart Lotto)  
- Qsmart customer dashboard QMS integration  

---

## QMS Admin Panel  
- **User Management**  
  - Create, edit, delete users.  
  - Assign permissions per company and per module (Ticket Centre, Complaints, CAPA, Company Setup).  
  - Enable or disable users.  
  - Super Admin role with unrestricted access across all companies, departments, and QMS features.  
  - **Menus available for permission control:**  
    - Ticket Centre  
    - Customer Complaints  
    - CAPA  
    - Company Setup  
    - SLA Setup  
    - Notifications Setup  
    - Root Cause Setup  
    - Resolution Setup  
    - Department Menus (Management, HR, Procurement, Marketing, Sales, Project Planning, Surveying, Project Management, Customer Care, QHSE, Technical, Accounts, ICT)  
  - **Role-wise permissions:**  
    - **Super Admin:** Full system access; all companies, menus, and modules.  
    - **Company Admin:** Full access to modules for their assigned company only.  
    - **Department Manager:** Access to Ticket Centre, Complaints, and CAPA limited to their department.  
    - **Supervisor:** Can see all tickets created by their subordinates in addition to their own.
    - **User:** Access to tickets or complaints they created, are assigned to, or are tagged in (My Tickets view).  

---

## QMS Company Setup  
- **Company Setup**  
  - Define prefixes for tickets, customer complaints, and CAPA.  
  - Manage company-specific settings for integration with Qsmart.  

- **Ticket Type Setup**  
- 
The Helpdesk has a setup section in the QMS Admin Panel for defining and managing ticket types. Each ticket type determines the workflow, defaults, SLA, and notifications for new tickets.  
- **List of all ticket types** (per company).  
- **Enable/Disable ticket types**  
  - Disabled types cannot be selected for new tickets.  
- **Edit ticket type**  
  - Change ticket type name.  
  - Set default departments (multi-select from company’s departments).  
  - Set default assigned user (single-select from company’s users).  
  - Set default priority (single-select from (Highest-Low).  
  - Configure SLA per priority level (time in hours, defined for each of the 4 priorities).  
  - Configure notifications for lifecycle events.  

### SLA per Priority  
- SLA is entered in **hours**.  
- SLA is configured separately for each priority (Highest-Low).  
- Due dates are automatically calculated = Created Date + SLA.  
- Updating ticket priority recalculates the due date.  

### Notifications per Ticket Type  
For each ticket type, administrators can configure notifications:  
- **Notification options:** Push (in-app), Email, or Disabled.  
- **Notification template:** Subject and body (with placeholders like `{TicketNumber}`, `{Subject}`, `{AssignedUser}`).  
- **Notification recipients:** Creator, Assigned User, Watch List, or specific CC list.  

**Events that trigger notifications:**  
- Ticket Created → Creator.  
- Ticket Updated → Creator + Watch List.  
- Ticket Resolved → Creator + Watch List.  
- Ticket Closed → Creator + Watch List.  
- SLA Due (< 2 hours before due) → Assigned User.  
- SLA Overdue → Assigned User.  

### Example Ticket Types & Custom Fields  
- **ICT Support**  
  - Device (single-select: Laptop/Desktop, Phone, Tablet, Scanner).  
- **Non-Conformance Issue**  
  - Platform (single-select: Qsmart Backoffice, Frontend, Native App, Form Builder, Pipedrive, Fast Field, Zapier, Other).  
- **Improvement Recommendation**  
  - Platform (same options as above).  
- **Change Request**  
  - Type (single-select: Qsmart Request, Form Builder Request, Fast Field Request, Pipedrive Request, Zapier Request, Other).  
  - Reason for Change (text area).  
  - Impact Assessment (text area).  
- **Marketing Feedback**  
  - Lead Origin (single-select: Direct Referral, Website, Previous Customer, Social Media, Site Signage, Fleet Branding, Traditional Media, Events).  
  - Feedback Rating (1–5 stars).  
  - Impact Assessment (text area).  
- **Customer Complaint Setup**  
  - Configure default assigned user and priority.  
  - Define SLA per priority level (time in hours).  
  - Configure notifications for complaint lifecycle (created, updated, resolved, closed, SLA due/overdue).  

- **CAPA Setup**  
  - Configure default assigned user and priority.  
  - Define SLA per priority level (time in hours).  
  - Configure notifications for CAPA lifecycle (created, updated, resolved, closed, SLA due/overdue).  

- **Root Cause Setup**  
  - CRUD list of root cause options.  
  - Values populate the root cause dropdown in tickets, complaints, and CAPA.  

- **Resolution Setup**  
  - CRUD list of resolution options.  
  - Values populate the resolution dropdown in tickets, complaints, and CAPA.  

- **Company Defined Fields**  
  - Each company can add fields specific to their ERP integration.  
  - For Churchfield/Qsmart:  
    - **Project**: searchable from Qsmart projects, populates project number + address.  
    - **Customer**: searchable from Qsmart, populates first/last name, email, phone.  
    - **Assigned Users / Watch List**: synced from Qsmart employees, contractors, operatives.  
    - **Departments**: mapped from top-level Qsmart menus (Management, HR, Procurement, Marketing, etc.).  
    - **Schemes**: defined list (ENQS, HEA, LSS, HEU, IEU, WHS).  

---

## Helpdesk (Ticket Centre)  
- **Views**  
  - **Kanban View:** Columns for Open, In Progress, Resolved, Closed. Cards show subject, type, priority, department, and assigned user. Drag-and-drop status changes. SLA due/overdue highlighting.  
  - **Table View:** Full ticket list with sortable/filterable columns (ticket number, company, type, subject, status, assigned user, priority, due date). SLA due/overdue highlighting.  

- **Ticket Creation**  
  - Four-step form:  
    1. Company & Subject  
    2. Ticket Type & Project/Customer mapping  
    3. Ticket-type specific sub-form  
    4. Departments & Attachments  
  - Once submitted, ticket appears in Ticket Centre.  

- **Ticket Fields**  
  - Ticket number (auto-generated, company prefix).  
  - Company (fixed).  
  - Subject & Description.  
  - Ticket type (company-defined).  
  - Project mapping (address, number).  
  - Ticket Cetagory (Ticket,Capa,Goals,complaints).
  - Assigned user (Qsmart list).  
  - Department (multi-select).  
  - Root cause & Resolution.  
  - Status (Open → Closed).  
  - Priority (Highest-Low).  
  - Attachments, comments (with tagging & notifications).  
  - Linked items (tickets, complaints, CAPA).  
  - Watch list & audit log.  
  - SLA-driven Due Date & Resolved Time.  

---

## Customer Complaints  
- **Views**  
  - **Kanban View:** Columns for Open, In Progress, Resolved, Closed. Cards show company, customer name, project address, priority, and assigned user. SLA highlighting.  
  - **Table View:** Complaint list with sortable/filterable columns (complaint number, type, company, project, customer, status, assigned user, priority, due date). SLA highlighting.  

- **Complaint Creation**  
  - Three-step form:  
    1. Company, Customer, Project (search for existing customer/project or enter details).  
    2. Complaint sub-form (subject, description).  
    3. Attachments (optional).  

- **Complaint Fields**  
  - Complaint number (auto-generated, prefix per company).  
  - Customer details (name, email, phone).  
  - Project mapping (address, number).  
  - Complaint type (Informal, Formal).  
  - Subject & Description.  
  - Departments & Assigned user.  
  - Root cause & Resolution.  
  - Status & Priority.  
  - Attachments, action plans, comments (with tagging), watch list, linked items (tickets, complaints, CAPA, Jira).  
  - SLA-driven Due Date & Resolved Time.  

---

## CAPA (Corrective and Preventive Action)  
- **Views**  
  - **Kanban View:** Columns for Open, In Progress, Resolved, Closed. Cards show subject, priority, departments, and assigned user. SLA highlighting.  
  - **Table View:** CAPA list with sortable/filterable columns (CAPA number, company, subject, status, assigned user, priority, due date). SLA highlighting.  

- **CAPA Fields**  
  - CAPA number (auto-generated, prefix per company).  
  - Subject & Description.  
  - Created date/by.  
  - Project mapping (address, number).  
  - Departments & Assigned user.  
  - Root cause & Resolution.  
  - Status & Priority.  
  - Risk Assessment (R1–R4).  
  - Attachments, comments (with tagging & notifications).  
  - Linked items (tickets, complaints, CAPA, Jira).  
  - Action Plan creation.  
  - Watch list & audit log.  
  - SLA-driven Due Date & Resolved Time.  

---



## Qsmart QMS Integration  
- **Frontend**  
  - Create tickets via QMS popup (company fixed to Churchfield).  
  - “My Tickets” lists all tickets created, assigned, or tagged, displayed in iframe with Kanban/Table views.  

- **Backoffice (Qsmart Admin)**  
  - **Quality Menu:** embeds Ticket Centre, Customer Complaints, CAPA Register.  
  - **Department Menus:** Ticket Centre scoped to department; Complaints visible only in Quality & Customer Care menus.  

---

## SLA Settings  
- SLA rules are defined **per company** within the QMS Admin panel.  
- SLA applies to:  
  - Tickets (per ticket type + per priority).  
  - Customer Complaints (per priority).  
  - CAPA (per priority).  
- SLA is expressed as a **time value** with suffix:  
  - `W` → Weeks  
  - `D` → Days  
  - `H` → Hours  
- Due Date calculation = Created Date + SLA value.  
- If priority is updated → Due Date recalculates.  
- SLA is also tied to notifications (due reminder, overdue alert).  

---

## Priority Settings  
- Priorities are defined as **Highest, High, Medium, Low** (highest to lowest).  
- Each priority level has its own SLA time configured.  
- Example:  
  - Highest = 1 Week  
  - High = 3 Days.  
  - Medium = 1 Day.  
  - Low = 4 Hours (critical IT issue).  
- Priority is selectable on Ticket, Complaint, and CAPA forms.  
- Changing priority mid-lifecycle updates SLA-driven Due Date.  

------

## Notification Settings  
- Configurable **per company and per item type** (Ticket, Complaint, CAPA).  
- **Triggers:**  
  - Created  
  - Updated  
  - Resolved  
  - Closed  
  - SLA Due (X hours before due)  
  - SLA Overdue  
- **Channels:**  
  - Push (in-app notification in Qsmart/QMS)  
  - Email (configured per company template)  
- **Recipients:**  
  - Creator  
  - Assigned User  
  - Watch List  
  - Tagged Users  
- **Templates:**  
  - Subject and Body support variables (e.g., `{TicketNumber}`, `{Subject}`, `{AssignedUser}`, `{DueDate}`).  

---



# Entities  

## BaseEntity<TId>  
Represents the base entity inherited by all models.  
- Id (TId, primary key)  
- CreatedDate (DateTime, required)  
- CreatedBy (int, FK → User.Id)  
- ModifiedDate (DateTime?)  
- ModifiedBy (int, FK → User.Id) 
- Status (enum: Active, Inactive, Archived)  
- DeletedDate (DateTime?)  
- DeletedBy (int, FK → User.Id) 

---

## User: #BaseEntity  
Represents a system user.  
- FirstName (string, required, max 100)  
- LastName (string, required, max 100)  
- Email (string, required, unique, max 200)  
- UserName (string, required, unique, max 200)  
- PasswordHash (string, required, max 500)  
- LastLoginDate (DateTime?)  

**Relations**  
- One `User` → Many `UserRole`  
- One `User` → Many `UserCompany`  
- One `User` → Many `UserDepartment`  

---

## Role:#BaseEntity  
Defines a role for RBAC.  
- Name (string, required, unique, max 100)  
- Description (string, max 250)  

**Relations**  
- One `Role` → Many `UserRole`  
- One `Role` → Many `RoleMenuPermission`  

---

## Company : #BaseEntity  
Represents a company/tenant within the QMS.  
- Name (string, required, max 200)  
- Description (string, max 500)  
- Status (enum: Active, Inactive)  

**Relations**  
- One `Company` → One `CompanyScopeConfig`  
- One `Company` → Many `Department`  
- One `Company` → Many `UserCompany`  
- One `Company` → Many `Ticket` / `CustomerComplaint` / `CAPA`  

---

## CompanyScopeConfig : #BaseEntity  
Represents company-level scoping and numbering rules.  
- CompanyId (long, FK → Company)  
- PrefixTicket (string, max 10)  
- PrefixComplaint (string, max 10)  
- PrefixCAPA (string, max 10)  
- SLASettings (ICollection<SLAConfigModel>)  
- NotificationSettings (ICollection<NotificationModel>)  
- RootCauseOptions (ICollection<RootCauseModel>)  
- ResolutionOptions (ICollection<ResolutionModel>)  

**Relations**  
- One `CompanyScopeConfig` → One `Company`  
- One `CompanyScopeConfig` → Many SLA/Notification/RootCause/Resolution definitions  

---

## Department:#BaseEntity    
Represents a department within a company.  
- Name (string, required, max 100)  
- Description (string, max 250)  
- FkCompanyId (long, FK → Company)  

**Relations**  
- One `Department` → One `Company`  
- One `Department` → Many `UserDepartment`  
- One `Department` → Many `Tickets` / `Complaints` / `CAPAs`  

---

## UserRole:#BaseEntity    
Mapping table that links users and roles.  
- FkUserId (long, FK → User)  
- FkRoleId (int, FK → Role)  

**Relations**  
- Many-to-Many between `User` and `Role`  

---

## UserCompany:#BaseEntity    
Mapping table that links users to companies.  
- FkUserId (long, FK → User)  
- FkCompanyId (long, FK → Company)  

**Relations**  
- Many-to-Many between `User` and `Company`  

---

## UserDepartment:#BaseEntity    
Mapping table that links users to departments.  
- FkUserId (long, FK → User)  
- FkDepartmentId (int, FK → Department)  

**Relations**  
- Many-to-Many between `User` and `Department`  











## AuditLog : #BaseEntity  
Represents an audit trail of actions performed within the QMS.  

- EntityName (string, required, max 200) → name of the entity being changed (e.g., Ticket, Complaint, CAPA, User).  
- EntityId (long, required) → the primary key of the entity being changed.  
- ActionType (enum: Created, Updated, Deleted, Viewed, Restored, StatusChanged)  
- OldValues (string, nullable, JSON) → serialized snapshot of values before change.  
- NewValues (string, nullable, JSON) → serialized snapshot of values after change.  
- ChangedDate (DateTime, required)  
- ChangedBy (int, FK → User.Id) → who performed the action.  
- IPAddress (string, max 50, nullable) → optional tracking of the request origin.  
- UserAgent (string, max 250, nullable) → browser/device info for context.  
- Notes (string, max 500, nullable)  

**Relations**  
- One `User` → Many `AuditLog` (user actions are logged).  
- One `AuditLog` → One target entity (linked by EntityName + EntityId).

## Menu : #BaseEntity  
Represents a menu/module in the system.  
- Name (string, required, max 100)  
- ParentId (int?, FK → Menu for hierarchy)  
- TemplateId (int)  
- DisplayOrder (int)  
- IconClass (string, max 100)  
- IconViewBox (string, max 100)  
- Route (string, max 250)  

**Relations**  
- One `Menu` → Many `MenuActionMap`  

---

## MenuAction : #BaseEntity  
Defines an **action** that can be performed on a menu.  
- Name (string, required, max 100)  
  - Examples: View, Create, Edit, Delete, Export, Approve  
- HttpVerb (enum: GET, POST, PUT, DELETE, PATCH, etc.)  
- Description (string, max 250)  

**Relations**  
- One `MenuAction` → Many `MenuActionMap`  

---

## MenuActionMap : #BaseEntity  
Mapping table linking menus to the actions available on them.  
- FkMenuId (int, FK → Menu)  
- FkMenuActionId (int, FK → MenuAction)  

**Relations**  
- One `Menu` → Many `MenuActionMap`  
- One `MenuAction` → Many `MenuActionMap`  
- One `MenuActionMap` → Many `MenuActionRoleMapping`  

---

## MenuActionRoleMapping : #BaseEntity  
Defines what **roles** are allowed to perform which **actions** on which **menu**.  
- FkRoleId (int, FK → Role)  
- FkMenuActionMapId (int, FK → MenuActionMap)  
- IsAllowed (bool)  

**Relations**  
- One `Role` → Many `MenuActionRoleMapping`  
- One `MenuActionMap` → Many `MenuActionRoleMapping`  

---




## TicketType : #BaseEntity  
Represents a ticket type within the Helpdesk.  
- Name (string, required, max 100)  
- Description (string, max 250, optional)  
- IsEnabled (bool, default true)  
- DefaultPriority (enum: Highest, High, Medium, Low)  
- DefaultAssignedUserId (int?, FK → User)  
- DefaultDepartments (ICollection<TicketTypeDepartmentMap>)  

**Relations**  
- One `TicketType` → Many `Tickets`  
- One `TicketType` → Many `SLAConfig`  
- One `TicketType` → Many `NotificationConfig`  
- One `TicketType` → Many `TicketTypeDepartmentMap`  

---

## SLAConfig : #BaseEntity  
Defines SLA rules per priority level for a given ticket type.  
- TicketTypeId (int, FK → TicketType)  
- Priority (enum: Highest, High, Medium, Low)  
- DurationHours (int, required)  

**Relations**  
- One `TicketType` → Many `SLAConfig`  

---

## NotificationTemplate : #BaseEntity  
Defines the rules for sending notifications.  
- TicketTypeId (int, FK → TicketType)  
- Trigger (enum: Created, Updated, Resolved, Closed, SLADue, SLAOverdue)  
- NotificationType (enum: Email, SMS, App, System)  
- EmailConfigurationId (int?, FK → EmailConfiguration, only when type = Email)  
- SubjectTemplate (string, max 200)  
- BodyTemplate (string, max 1000)  
- CcList (string, max 500, optional)  
- IsEnabled (bool, default true)  

**Relations**  
- One `TicketType` → Many `NotificationTemplate`  
- One `NotificationConfig` → One `EmailConfiguration` (optional)  

---

## TicketTypeDepartmentMap : #BaseEntity  
Mapping table linking Ticket Types to default departments.  
- TicketTypeId (int, FK → TicketType)  
- DepartmentId (int, FK → Department)  

**Relations**  
- Many-to-Many between `TicketType` and `Department`  

---

## EmailConfiguration : #BaseEntity  
Represents credentials and settings for sending emails.  
- UserName (string, max 200)  
- Password (string, max 200, encrypted)  
- Host (string, max 200)  
- SMTPPort (int, required)  
- IMAPPort (int, required)  
- AccessKey (string, max 200, optional)  
- SecretKey (string, max 200, optional, encrypted)  
- Status (enum: Active, Inactive)  
- BCC (string, max 1000, optional, comma-separated list)  
- IsDefault (bool, default false)  
- Name (string, max 200, friendly name)  
- ReplyTo (string, max 200, optional)  

**Relations**  
- One `EmailConfiguration` → Many `NotificationConfig`  

---

## NotificationSchedule : #BaseEntity  
Represents a scheduled notification that must be processed by a background worker.  
- NotificationConfigId (int, FK → NotificationConfig)  
- TicketId (int?, FK → Ticket, nullable if not ticket-related)  
- Recipient (string, required, max 500)  
- Subject (string, max 200)  
- Body (string, max 2000)  
- NotificationType (enum: Email, SMS, App, System)  
- EmailConfigurationId (int?, FK → EmailConfiguration, only when type = Email)  
- ScheduledTime (DateTime, required)  
- SentTime (DateTime, nullable)  
- RetryCount (int, default 0)  
- MaxRetryCount (int, default 3)  
- Status (enum: Pending, Processing, Sent, Failed, Cancelled)  
- ErrorMessage (string, max 1000, optional)  

**Relations**  
- One `NotificationConfig` → Many `NotificationSchedule`  
- One `EmailConfiguration` → Many `NotificationSchedule` (optional, only for Email)  





## TicketType : #BaseEntity
- Name (string, required, max 100)
- Description (string, max 250, optional)
- IsEnabled (bool, default true)
- DefaultPriority (enum: Highest, High, Medium, Low)
- DefaultAssignedUserId (int?, FK → User)
- DefaultDepartments (ICollection<TicketTypeDepartmentMap>)

**Relations**
- One `TicketType` → Many `Tickets`
- One `TicketType` → Many `SLAConfig`
- One `TicketType` → Many `NotificationConfig`
- One `TicketType` → Many `TicketTypeDepartmentMap`

---

## TicketLink : #BaseEntity
- TicketId (long, FK → Ticket, required)
- LinkedEntityType (enum: Ticket, Complaint, CAPA, Jira)
- LinkedEntityId (long?, FK when internal)
- ExternalKey (string, max 100, optional)
- Notes (string, max 250, optional)

**Relations**
- One `Ticket` → Many `TicketLink`

---

## TicketDepartmentMap : #BaseEntity
- TicketId (long, FK → Ticket, required)
- DepartmentId (long, FK → Department, required)

**Relations**
- Many-to-Many between `Ticket` and `Department`

---

## TicketWatchList : #BaseEntity
- TicketId (long, FK → Ticket, required)
- UserId (long, FK → User, required)

**Relations**
- Many-to-Many between `Ticket` and `User`

---

## TicketComment : #BaseEntity
- TicketId (long, FK → Ticket, required)
- CommentText (string, required, max 4000)
- CreatedByUserId (long, FK → User, required)
- CreatedAt (DateTime, required)
- MentionUserIds (string, max 500, optional)

**Relations**
- One `Ticket` → Many `TicketComment`
- One `User` → Many `TicketComment`

---

## TicketAttachment : #BaseEntity
- TicketId (long, FK → Ticket, required)
- FileName (string, required, max 255)
- FilePath (string, required, max 500)
- ContentType (string, max 100)
- SizeBytes (long)
- UploadedByUserId (long, FK → User, required)
- UploadedAt (DateTime, required)

**Relations**
- One `Ticket` → Many `TicketAttachment`

---
## LeadCustomer : #BaseEntity
- CompanyId (long, FK → Company, required)  
- ProjectNumber (string, max 50)  
- ProjectAddress (string, max 300)  
- CustomerFirstName (string, max 100)  
- CustomerLastName (string, max 100)  

**Relations**
- One `Company` → Many `LeadCustomer`  
- One `LeadCustomer` → Many `TicketLeadCustomerMap`

---

## TicketLeadCustomerMap : #BaseEntity
- TicketId (long, FK → Ticket, required)  
- LeadCustomerId (long, FK → LeadCustomer, required)  

**Relations**
- One `Ticket` → Many `TicketLeadCustomerMap`  
- One `LeadCustomer` → Many `TicketLeadCustomerMap`  

---

## Ticket : #BaseEntity
- CompanyId (long, FK → Company, required)  
- TicketNumber (string, required, max 30)  
- Subject (string, required, max 200)  
- Description (string, max 4000)  
- SubmittedByUserId (long, FK → User, required)  
- TicketTypeId (long, FK → TicketType, required)  
- TicketCategory (enum: Ticket, CAPA, Goals, Complaints)  
- Status (enum: Open, InProgress, Resolved, Closed, Reopen)  
- Priority (enum: enum: Highest, High, Medium, Low)  
- AssignedUserId (long?, FK → User)  
- RootCauseId (long?, FK → RootCause)  
- ResolutionId (long?, FK → Resolution)  
- EstimatedTime (string?, `\d+[WDH]`)  
- DueDate (DateTime?, computed)  
- ResolvedAt (DateTime?)  

**Relations**
- One `Company` → Many `Ticket`  
- One `User` → Many `Ticket`  
- One `Ticket` → Many `TicketAttachment`  
- One `Ticket` → Many `TicketComment`  
- One `Ticket` → Many `TicketWatchList`  
- One `Ticket` → Many `TicketDepartmentMap`  
- One `Ticket` → Many `TicketLink`  
- One `Ticket` → Many `TicketLeadCustomerMap`
---

## RootCause : #BaseEntity
- CompanyId (long, FK → Company, required)
- Name (string, required, max 100)
- Code (string, max 30, optional)
- Description (string, max 250, optional)
- IsActive (bool, default true)
- DisplayOrder (int, default 0)

**Relations**
- One `RootCause` → Many `Ticket`

---

## Resolution : #BaseEntity
- CompanyId (long, FK → Company, required)
- Name (string, required, max 100)
- Code (string, max 30, optional)
- Description (string, max 250, optional)
- IsActive (bool, default true)
- DisplayOrder (int, default 0)

**Relations**
- One `Resolution` → Many `Ticket`

---

