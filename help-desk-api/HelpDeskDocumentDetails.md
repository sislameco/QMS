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

- **Company Setup**  
  - Configure ticket prefixes, complaint prefixes, CAPA prefixes.  
  - Manage ticket types, default users, default departments, and SLAs per priority.  
  - Configure complaint setup (default user, SLA rules, notifications).  
  - Configure CAPA setup (default user, SLA rules, notifications).  
  - Manage root cause and resolution libraries (CRUD).  
  - Define company-specific fields (e.g., Qsmart project, customer mapping).  

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
  - Assigned user (Qsmart list).  
  - Department (multi-select).  
  - Root cause & Resolution.  
  - Status (Open → Closed).  
  - Priority (P1–P4).  
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

## QMS Company Setup  
- **Company Setup**  
  - Define prefixes for tickets, customer complaints, and CAPA.  
  - Manage company-specific settings for integration with Qsmart.  

- **Ticket Type Setup**  
  - List all ticket types per company.  
  - Enable/disable ticket types (disabled types cannot be selected for new tickets).  
  - Configure default values: assigned user, departments, priority.  
  - Define SLA per priority level (time in hours).  
  - Configure notifications for each ticket type (trigger, channel, subject/body, recipients).  

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

## Qsmart QMS Integration  
- **Frontend**  
  - Create tickets via QMS popup (company fixed to Churchfield).  
  - “My Tickets” lists all tickets created, assigned, or tagged, displayed in iframe with Kanban/Table views.  

- **Backoffice (Qsmart Admin)**  
  - **Quality Menu:** embeds Ticket Centre, Customer Complaints, CAPA Register.  
  - **Department Menus:** Ticket Centre scoped to department; Complaints visible only in Quality & Customer Care menus.  

---

## SLA’s  
- SLA rules set per ticket type, complaint, and CAPA.  
- Entered in W (weeks), D (days), H (hours).  
- Due date = Created date + SLA value.  
- Priority changes recalculate due dates.  

---

## Notifications  
- Triggers: Create, Update, Resolve, Close, SLA due, SLA overdue.  
- Channels: Push (in-app) and Email.  
- Configurable per item type (ticket, complaint, CAPA).  
- Recipients: Creator, Assigned user, Watch list, Tagged users.  
- Template variables available (subject, description, department, assigned user, root cause, resolution, etc.).  

---
