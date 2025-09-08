# Quality Management System (QMS) Full Requirements Document

# Overview
We need to development a quality management system (QMS) that is a standalone platform that is a SAAS and plugged in to our different companies. 
Each of our companies has its own ERP and will need bespoke integration.
The quality management system will consist of the following: 
- Helpdesk. User support via tickets submitted on ERP frontend that are sent to the ticket centre and department ticket list in ERP back office.
- Customer Complaints. Able to create customer complaint items and these are listed within quality and customer care dashboard in ERP back office.
- CAPA. Located in quality dashboard in ERP back office, Quality manager can create CAPA items or frontend users can create support tickets for CAPA and also existing tickets can be escalated to CAPA.
- Risk Register. Located in quality menu in ERP back office. Quality manager can create risk items and link to helpdesk tickets, customer complaints, CAPA items or Goals
- Goals: Located in quality menu in ERP back office. Quality manager can create Goal items and link to helpdesk tickets, customer complaints, CAPA items or Risk items. Goals are also mapped to departments.


# Scope

Standalone QMS + Qsmart (Churchfield) integration: build a SaaS QMS and connect it to the Churchfield ERP, Qsmart.

QMS Admin Panel:
Helpdesk (Ticket Centre).
Customer Complaints.
CAPA.
Company setup for ticket types, SLAs, notifications (tickets/complaints/CAPA).
Qsmart connection – Ticket Centre: create tickets from Qsmart and view the Ticket Centre.
Qsmart connection – Customer Complaints: create complaints from Qsmart and view the complaints register.

Qsmart connection – CAPA: create/view CAPA items in Qsmart Quality menu CAPA Register.

Qsmart QMS integration surface areas:

Qsmart Frontend: launch QMS new-ticket flow; list in “My Tickets” (iframe) with Kanban/Table, view/edit/comment/attach.

Qsmart Admin: Quality menu embeds Ticket Centre, Customer Complaints, CAPA Register; department menus show department-scoped Ticket Centre.

SLA engine: per ticket type and per priority for Tickets, Customer Complaints, and CAPA.

Notifications: configurable triggers (created/updated/resolved/closed, SLA due/overdue) with push/email.