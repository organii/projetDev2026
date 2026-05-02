# Agile AI Backend Implementation TODO

This file tracks the backend implementation progress phase by phase.

## Phase 1 - Security And Authorization Foundation

- [x] Add role-based authorization policies in `Program.cs`.
- [x] Add a current-user helper service to read `UserId`, email, and role from JWT claims.
- [x] Add project authorization service:
  - [x] Check if current user is admin.
  - [x] Check if current user owns a project.
  - [x] Check if current user is a member of a project.
  - [x] Check if current user can manage project members.
- [x] Register authorization/helper services in startup.
- [x] Apply project-level authorization checks to project endpoints.
- [x] Apply project-level authorization checks to project member endpoints.
- [x] Apply project-level authorization checks to project execution endpoints.
- [x] Apply project-level authorization checks to sprint endpoints.
- [x] Apply project-level authorization checks to backlog/user-story endpoints.
- [x] Apply project-level authorization checks to issue/kanban endpoints.
- [x] Apply project-level authorization checks to comments and subtasks.

## Phase 2 - DTOs And Safer API Contracts

- [x] Create auth DTOs:
  - [x] `LoginRequestDto`
  - [x] `RegisterUserDto`
  - [x] `UserResponseDto`
- [x] Create project DTOs:
  - [x] `CreateProjectDto`
  - [x] `ProjectResponseDto`
  - [x] `ProjectDetailsDto`
- [x] Create sprint DTOs:
  - [x] `CreateSprintDto`
  - [x] `UpdateSprintDto`
  - [x] `SprintResponseDto`
- [x] Create user story DTOs:
  - [x] `CreateUserStoryDto`
  - [x] `UpdateUserStoryDto`
  - [x] `UserStoryResponseDto`
- [x] Create issue DTOs:
  - [x] `CreateIssueDto`
  - [x] `UpdateIssueDto`
  - [x] `MoveIssueDto`
  - [x] `IssueResponseDto`
- [x] Create collaboration DTOs for project members, comments, and subtasks.
- [x] Update controllers so they do not expose entities directly.
- [x] Ensure password, access token, and refresh token fields are never returned in user responses.

## Phase 3 - Workflow-Specific CQRS Commands

- [x] Add `CreateProjectCommand` and handler.
- [x] Add `AddProjectMemberCommand` and handler.
- [x] Add `RemoveProjectMemberCommand` and handler.
- [x] Add `CreateSprintCommand` and handler.
- [x] Add `StartSprintCommand` and handler.
- [x] Add `CloseSprintCommand` and handler.
- [x] Add `CreateUserStoryCommand` and handler.
- [x] Add `AssignUserStoryToSprintCommand` and handler.
- [x] Add `CreateIssueCommand` and handler.
- [x] Add `AssignIssueCommand` and handler.
- [x] Add `MoveIssueStatusCommand` and handler.
- [x] Add `ToggleSubTaskCommand` and handler.
- [x] Register workflow command handlers in `DependencyContainer.RegisterServices()`.
- [x] Update controllers to use workflow commands instead of generic CRUD for business actions.

## Phase 4 - Validation And Error Handling

- [x] Add a standard API error response model.
- [x] Add global exception middleware.
- [x] Add validation for project creation.
- [x] Add validation for sprint dates and status transitions.
- [x] Add validation for story points and priority.
- [x] Add validation for issue status transitions.
- [x] Add duplicate project member protection.
- [x] Return consistent `400`, `401`, `403`, `404`, and `409` responses.

## Phase 5 - Agile Dashboard And Reporting Endpoints

- [x] Add "my projects" endpoint.
- [x] Add "active sprint summary" endpoint.
- [x] Add "sprint board" response optimized for frontend drag/drop.
- [x] Add "team workload" endpoint.
- [x] Add "burndown chart data" endpoint.
- [x] Add "velocity chart data" endpoint.
- [x] Add "blocked/overdue issues" endpoint.

## Phase 6 - AI Features

- [x] Implement demo-ready user-story description generation.
- [x] Implement acceptance-criteria generation.
- [x] Implement story-to-subtasks generation.
- [x] Implement priority prediction.
- [x] Implement issue auto-assignment based on members/workload.
- [x] Implement sprint risk prediction.
- [x] Implement daily standup summary.
- [x] Implement sprint review/release notes generation.
- [x] Save every AI result to `AIPredictionLog`.

## Phase 7 - Collaboration And Real-Time Features

- [x] Add activity history model.
- [x] Add activity logging for issue/comment/subtask changes.
- [x] Add notification creation on issue assignment.
- [x] Add notification read/unread endpoints.
- [x] Add SignalR board updates.
- [x] Add attachment upload endpoint.
- [x] Add comment mentions.

## Test Checkpoints

- [x] Checkpoint 1: Security services and policies compile. Ready for user test.
- [x] Checkpoint 2: Project authorization works. Ready for user test.
- [x] Checkpoint 3: DTO migration works for auth and project endpoints. Ready for user test.
- [x] Checkpoint 4: Workflow commands work for project/sprint/story/issue. Ready for user test.
- [x] Checkpoint 5: Validation/error handling works. Ready for user test.
- [x] Checkpoint 6: Dashboard endpoints return expected data. Ready for user test.
- [x] Checkpoint 7: AI endpoints return and log predictions. Ready for user test.
- [x] Checkpoint 8: Collaboration features work. Ready for user test.

## Phase 8 - Production Hardening Reanalysis

- [ ] Add a database migration for `ActivityLog` and verify schema sync after the latest model changes.
- [ ] Replace generic hard delete behavior with soft delete where the entities already expose `isDeleted`.
- [ ] Add attachment validation rules:
  - [ ] allowed file types
  - [ ] max file size configuration
  - [ ] delete endpoint / cleanup flow
- [ ] Replace remaining workflow updates that still use generic `Put`:
  - [ ] sprint update
  - [ ] any remaining entity updates with business rules
- [ ] Add pagination and filtering for notifications, comments, issues, and activity history.
- [ ] Add tests for authorization, workflow transitions, auto-assignment, mentions, and attachment upload.
