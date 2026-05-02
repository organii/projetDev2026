USE [dev2026DB];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRANSACTION;

DECLARE @ProductOwnerId uniqueidentifier = '10000000-0000-0000-0000-000000000001';
DECLARE @ScrumMasterId uniqueidentifier = '10000000-0000-0000-0000-000000000002';
DECLARE @BackendDevId uniqueidentifier = '10000000-0000-0000-0000-000000000003';
DECLARE @FrontendDevId uniqueidentifier = '10000000-0000-0000-0000-000000000004';
DECLARE @QaId uniqueidentifier = '10000000-0000-0000-0000-000000000005';
DECLARE @AiDevId uniqueidentifier = '10000000-0000-0000-0000-000000000006';

DECLARE @ProjectId uniqueidentifier = '20000000-0000-0000-0000-000000000001';
DECLARE @EpicAuthId uniqueidentifier = '30000000-0000-0000-0000-000000000001';
DECLARE @EpicPlanningId uniqueidentifier = '30000000-0000-0000-0000-000000000002';
DECLARE @EpicExecutionId uniqueidentifier = '30000000-0000-0000-0000-000000000003';
DECLARE @EpicAnalyticsId uniqueidentifier = '30000000-0000-0000-0000-000000000004';

INSERT INTO dbo.Users
    (UserId, Nom, Prenom, Email, MotDePasse, Telephone, Role, Filiale, RefreshToken, RefreshTokenExpiryTime, Token, isDeleted)
SELECT v.UserId, v.Nom, v.Prenom, v.Email, 'Dev2026@123', v.Telephone, v.Role, v.Filiale, NULL, DATEADD(day, 30, SYSUTCDATETIME()), NULL, 0
FROM (VALUES
    (@ProductOwnerId, 'Ben Salem', 'Amira', 'amira.bensalem@poulina.com', '+216 20 451 120', 'ProductOwner', 'Tunis'),
    (@ScrumMasterId, 'Mansouri', 'Youssef', 'youssef.mansouri@poulina.com', '+216 29 883 014', 'ScrumMaster', 'Sfax'),
    (@BackendDevId, 'Karray', 'Nour', 'nour.karray@poulina.com', '+216 55 762 331', 'BackendDeveloper', 'Sousse'),
    (@FrontendDevId, 'Jaziri', 'Lina', 'lina.jaziri@poulina.com', '+216 52 440 882', 'FrontendDeveloper', 'Tunis'),
    (@QaId, 'Trabelsi', 'Mehdi', 'mehdi.trabelsi@poulina.com', '+216 24 118 909', 'QA', 'Tunis'),
    (@AiDevId, 'Gharbi', 'Sami', 'sami.gharbi@poulina.com', '+216 56 610 772', 'AIDeveloper', 'Nabeul')
) AS v(UserId, Nom, Prenom, Email, Telephone, Role, Filiale)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Users u WHERE u.UserId = v.UserId);

IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE [Key] = 'MLTRC')
BEGIN
    INSERT INTO dbo.Projects
        (ProjectId, ProjectName, ProjectDescription, [Key], OwnerId, CreatedAt, UpdatedAt, IsFinished, FinishedAt, TotalCompletedPoints, isDeleted)
    VALUES
        (@ProjectId, 'Trace Server ML Training Project',
         'Coherent agile dataset for priority prediction, issue assignment, kanban flow, and sprint velocity demos.',
         'MLTRC', @ProductOwnerId, DATEADD(day, -98, SYSUTCDATETIME()), SYSUTCDATETIME(), 0, NULL, 85, 0);

    INSERT INTO dbo.ProjectMembers
        (ProjectMemberId, ProjectId, MemberId, isDeleted)
    SELECT NEWID(), @ProjectId, v.MemberId, 0
    FROM (VALUES (@ProductOwnerId), (@ScrumMasterId), (@BackendDevId), (@FrontendDevId), (@QaId), (@AiDevId)) v(MemberId);

    INSERT INTO dbo.KanbanColumns
        (ColumnId, ProjectId, Status, WipLimit, isDeleted)
    VALUES
        (NEWID(), @ProjectId, 1, 12, 0),
        (NEWID(), @ProjectId, 2, 5, 0),
        (NEWID(), @ProjectId, 3, 4, 0),
        (NEWID(), @ProjectId, 4, 12, 0);

    DECLARE @Sprints TABLE
    (
        SprintNo int NOT NULL PRIMARY KEY,
        SprintId uniqueidentifier NOT NULL,
        Name nvarchar(200) NOT NULL,
        StartOffset int NOT NULL,
        EndOffset int NOT NULL,
        Status int NOT NULL,
        CompletedPoints int NOT NULL
    );

    INSERT INTO @Sprints
        (SprintNo, SprintId, Name, StartOffset, EndOffset, Status, CompletedPoints)
    VALUES
        (1, '40000000-0000-0000-0000-000000000001', 'Sprint 1 - Secure Access Foundation', -84, -71, 5, 18),
        (2, '40000000-0000-0000-0000-000000000002', 'Sprint 2 - Project Planning Core', -70, -57, 5, 22),
        (3, '40000000-0000-0000-0000-000000000003', 'Sprint 3 - Execution Board', -56, -43, 5, 24),
        (4, '40000000-0000-0000-0000-000000000004', 'Sprint 4 - Reporting and Quality', -42, -29, 5, 21),
        (5, '40000000-0000-0000-0000-000000000005', 'Sprint 5 - AI Assisted Delivery', -14, -1, 2, 10),
        (6, '40000000-0000-0000-0000-000000000006', 'Sprint 6 - Forecasting Hardening', 0, 13, 1, 0);

    INSERT INTO dbo.Sprints
        (SprintId, Name, StartDate, EndDate, Status, ProjectId, CompletedPoints, isDeleted)
    SELECT SprintId, Name, DATEADD(day, StartOffset, SYSUTCDATETIME()), DATEADD(day, EndOffset, SYSUTCDATETIME()), Status, @ProjectId, CompletedPoints, 0
    FROM @Sprints;

    INSERT INTO dbo.Epics
        (EpicId, Title, Description, ProjectId, isDeleted)
    VALUES
        (@EpicAuthId, 'Secure Access and Permissions', 'Authentication, authorization, account tokens, and permission boundaries.', @ProjectId, 0),
        (@EpicPlanningId, 'Backlog and Sprint Planning', 'Product backlog, epics, user stories, estimation, sprint planning, and acceptance criteria.', @ProjectId, 0),
        (@EpicExecutionId, 'Kanban Execution Workflow', 'Issues, subtasks, comments, attachments, WIP limits, and execution board flow.', @ProjectId, 0),
        (@EpicAnalyticsId, 'AI Delivery Insights', 'Priority prediction, assignment recommendation, velocity analytics, and prediction logs.', @ProjectId, 0);

    DECLARE @Stories TABLE
    (
        StoryNo int NOT NULL PRIMARY KEY,
        StoryId uniqueidentifier NOT NULL,
        Title nvarchar(300) NOT NULL,
        Description nvarchar(max) NOT NULL,
        StoryPoints int NOT NULL,
        Priority int NOT NULL,
        MoSCoW int NOT NULL,
        EpicId uniqueidentifier NOT NULL,
        SprintNo int NULL,
        Status int NOT NULL,
        AssigneeId uniqueidentifier NULL,
        WorkType nvarchar(40) NOT NULL
    );

    INSERT INTO @Stories
        (StoryNo, StoryId, Title, Description, StoryPoints, Priority, MoSCoW, EpicId, SprintNo, Status, AssigneeId, WorkType)
    VALUES
        (1, '50000000-0000-0000-0000-000000000001', 'Sign in with JWT access tokens', 'Implement secure login, JWT access token creation, and refresh-token expiry validation for protected API calls.', 8, 4, 1, @EpicAuthId, 1, 4, @BackendDevId, 'Backend'),
        (2, '50000000-0000-0000-0000-000000000002', 'Restrict project access to members only', 'Reject project reads and updates when the authenticated user is not the owner or a project member.', 5, 4, 1, @EpicAuthId, 1, 4, @BackendDevId, 'Backend'),
        (3, '50000000-0000-0000-0000-000000000003', 'Return user profile after login', 'Return normalized user identity, role, filiale, and display name so the frontend can build the session header.', 3, 2, 2, @EpicAuthId, 1, 4, @FrontendDevId, 'Frontend'),
        (4, '50000000-0000-0000-0000-000000000004', 'Create project with unique key', 'Validate and create a project with a unique short key so teams can link sprints, epics, and members correctly.', 5, 3, 1, @EpicPlanningId, 2, 4, @BackendDevId, 'Backend'),
        (5, '50000000-0000-0000-0000-000000000005', 'Manage project membership', 'Add and list project members with stable roles to support assignment and authorization workflows.', 5, 3, 1, @EpicPlanningId, 2, 4, @BackendDevId, 'Backend'),
        (6, '50000000-0000-0000-0000-000000000006', 'Create epics and user stories', 'Product owner can create epics and user stories with title, description, points, priority, and MoSCoW value.', 8, 3, 1, @EpicPlanningId, 2, 4, @FrontendDevId, 'Frontend'),
        (7, '50000000-0000-0000-0000-000000000007', 'Add acceptance criteria to stories', 'Attach testable acceptance criteria to each user story before the story can be considered ready for sprint planning.', 3, 2, 2, @EpicPlanningId, 2, 4, @QaId, 'QA'),
        (8, '50000000-0000-0000-0000-000000000008', 'Create sprint and assign stories', 'Scrum master can create a sprint, set dates, and assign ready stories while keeping planned points visible.', 8, 3, 1, @EpicPlanningId, 2, 4, @FrontendDevId, 'Frontend'),
        (9, '50000000-0000-0000-0000-000000000009', 'Move issues across kanban statuses', 'Developers can move issues through todo, in progress, review, and done while preserving board order.', 8, 3, 1, @EpicExecutionId, 3, 4, @FrontendDevId, 'Frontend'),
        (10, '50000000-0000-0000-0000-000000000010', 'Enforce WIP limits on active columns', 'Warn users when in-progress or review columns exceed configured WIP limits for the project.', 5, 3, 2, @EpicExecutionId, 3, 4, @BackendDevId, 'Backend'),
        (11, '50000000-0000-0000-0000-000000000011', 'Add subtasks to issues', 'Break an issue into subtasks and calculate completion from checked subtasks for execution tracking.', 5, 2, 2, @EpicExecutionId, 3, 4, @FrontendDevId, 'Frontend'),
        (12, '50000000-0000-0000-0000-000000000012', 'Comment on issue progress', 'Team members can comment on issues with creation dates so reviewers can follow decisions and blockers.', 3, 2, 2, @EpicExecutionId, 3, 4, @BackendDevId, 'Backend'),
        (13, '50000000-0000-0000-0000-000000000013', 'Attach evidence files to issues', 'QA can attach screenshots or PDF notes to issues to document defects and acceptance validation.', 3, 2, 3, @EpicExecutionId, 3, 4, @QaId, 'QA'),
        (14, '50000000-0000-0000-0000-000000000014', 'Show sprint completed points', 'Display completed story points per closed sprint for velocity charting and release planning.', 5, 2, 2, @EpicAnalyticsId, 4, 4, @FrontendDevId, 'Frontend'),
        (15, '50000000-0000-0000-0000-000000000015', 'Log AI prediction requests', 'Store prediction type, input payload, predicted value, confidence, and date for audit and model evaluation.', 8, 3, 1, @EpicAnalyticsId, 4, 4, @AiDevId, 'AI'),
        (16, '50000000-0000-0000-0000-000000000016', 'Add assignment notifications', 'Notify assignees when issues are assigned so active work is visible without refreshing the board.', 3, 2, 2, @EpicExecutionId, 4, 4, @BackendDevId, 'Backend'),
        (17, '50000000-0000-0000-0000-000000000017', 'Test completed sprint reporting', 'Verify closed sprint totals, story statuses, and completed points before analytics are used by ML features.', 5, 3, 1, @EpicAnalyticsId, 4, 4, @QaId, 'QA'),
        (18, '50000000-0000-0000-0000-000000000018', 'Predict user story priority', 'Predict priority from story description, story points, MoSCoW value, and historical labels.', 8, 4, 1, @EpicAnalyticsId, 5, 3, @AiDevId, 'AI'),
        (19, '50000000-0000-0000-0000-000000000019', 'Recommend issue assignee', 'Recommend the best member based on issue text, work type, previous assignments, and active workload.', 8, 3, 1, @EpicAnalyticsId, 5, 2, @AiDevId, 'AI'),
        (20, '50000000-0000-0000-0000-000000000020', 'Forecast sprint velocity', 'Calculate average velocity from closed sprints and prepare a forecast for the next sprint capacity.', 5, 3, 2, @EpicAnalyticsId, 5, 2, @AiDevId, 'AI'),
        (21, '50000000-0000-0000-0000-000000000021', 'Improve backlog filters', 'Filter stories by priority, MoSCoW, sprint, and status to help product owners groom the backlog faster.', 3, 2, 3, @EpicPlanningId, 5, 1, @FrontendDevId, 'Frontend'),
        (22, '50000000-0000-0000-0000-000000000022', 'Export sprint summary CSV', 'Export sprint name, completed points, active issues, and story count for lightweight reporting.', 2, 1, 3, @EpicAnalyticsId, NULL, 1, @FrontendDevId, 'Frontend'),
        (23, '50000000-0000-0000-0000-000000000023', 'Archive finished project', 'Allow product owner to mark a project finished after all stories are done and reports are exported.', 3, 1, 4, @EpicPlanningId, NULL, 1, @BackendDevId, 'Backend');

    INSERT INTO dbo.UserStories
        (UserStoryId, Title, Description, StoryPoints, Priority, MoSCoW, EpicId, SprintId, Status, isDeleted)
    SELECT st.StoryId, st.Title, st.Description, st.StoryPoints, st.Priority, st.MoSCoW, st.EpicId, sp.SprintId, st.Status, 0
    FROM @Stories st
    LEFT JOIN @Sprints sp ON sp.SprintNo = st.SprintNo;

    INSERT INTO dbo.AcceptanceCriteria
        (CriterionId, Description, IsSatisfied, UserStoryId, isDeleted)
    SELECT NEWID(), CONCAT('Main workflow for "', Title, '" is implemented and covered by validation.'), CASE WHEN Status = 4 THEN 1 ELSE 0 END, StoryId, 0
    FROM @Stories
    UNION ALL
    SELECT NEWID(), CONCAT('Edge cases for "', Title, '" are reviewed before the story is marked done.'), CASE WHEN Status = 4 THEN 1 ELSE 0 END, StoryId, 0
    FROM @Stories
    WHERE Priority >= 3;

    INSERT INTO dbo.Issues
        (IssueId, Title, Status, [Order], UserStoryId, AssigneeId, isDeleted)
    SELECT NEWID(),
           CONCAT(WorkType, ' implementation - ', Title),
           CASE
               WHEN Status = 4 THEN 4
               WHEN Status = 3 THEN 3
               WHEN Status = 2 THEN 2
               ELSE 1
           END,
           StoryNo,
           StoryId,
           AssigneeId,
           0
    FROM @Stories;

    INSERT INTO dbo.SubTasks
        (SubTaskId, Title, IsCompleted, IssueId, isDeleted)
    SELECT NEWID(), CONCAT('Design solution for ', s.Title), CASE WHEN s.Status IN (3, 4) THEN 1 ELSE 0 END, i.IssueId, 0
    FROM @Stories s
    INNER JOIN dbo.Issues i ON i.UserStoryId = s.StoryId AND i.Title LIKE CONCAT(s.WorkType, ' implementation%')
    UNION ALL
    SELECT NEWID(), CONCAT('Implement and review ', s.Title), CASE WHEN s.Status = 4 THEN 1 ELSE 0 END, i.IssueId, 0
    FROM @Stories s
    INNER JOIN dbo.Issues i ON i.UserStoryId = s.StoryId AND i.Title LIKE CONCAT(s.WorkType, ' implementation%')
    UNION ALL
    SELECT NEWID(), CONCAT('Validate acceptance criteria for ', s.Title), CASE WHEN s.Status = 4 THEN 1 ELSE 0 END, i.IssueId, 0
    FROM @Stories s
    INNER JOIN dbo.Issues i ON i.UserStoryId = s.StoryId AND i.Title LIKE CONCAT(s.WorkType, ' implementation%')
    WHERE s.Priority >= 3;

    INSERT INTO dbo.Comments
        (CommentId, Content, CreatedAt, UpdatedAt, IssueId, AuthorId, isDeleted)
    SELECT NEWID(),
           CASE
               WHEN s.Status = 4 THEN 'Completed and accepted during sprint review.'
               WHEN s.Status = 3 THEN 'Ready for review; waiting for final QA validation.'
               WHEN s.Status = 2 THEN 'Implementation started and main technical path is clear.'
               ELSE 'Ready in backlog; not started yet.'
           END,
           DATEADD(day, -ABS(CHECKSUM(s.StoryNo)) % 60, SYSUTCDATETIME()),
           NULL,
           i.IssueId,
           CASE WHEN s.WorkType = 'QA' THEN @QaId WHEN s.WorkType = 'AI' THEN @AiDevId ELSE @ScrumMasterId END,
           0
    FROM @Stories s
    INNER JOIN dbo.Issues i ON i.UserStoryId = s.StoryId AND i.Title LIKE CONCAT(s.WorkType, ' implementation%');

    INSERT INTO dbo.Attachments
        (AttachmentId, FileName, FileType, FileSize, BlobUrl, IssueId, UploaderId, isDeleted)
    SELECT NEWID(),
           CONCAT('evidence-story-', s.StoryNo, '.png'),
           'image/png',
           120000 + (s.StoryNo * 7310),
           CONCAT('https://storage.local/dev2026/mltrc/evidence-story-', s.StoryNo, '.png'),
           i.IssueId,
           @QaId,
           0
    FROM @Stories s
    INNER JOIN dbo.Issues i ON i.UserStoryId = s.StoryId AND i.Title LIKE CONCAT(s.WorkType, ' implementation%')
    WHERE s.WorkType = 'QA' OR s.Priority >= 4;

    INSERT INTO dbo.ScrumCeremonies
        (CeremonyId, [Date], Type, Notes, Obstacles, SprintId)
    SELECT NEWID(), DATEADD(day, StartOffset, SYSUTCDATETIME()), 'Planning',
           CONCAT(Name, ': planned work follows team capacity and previous velocity.'),
           CASE WHEN Status = 5 THEN 'Resolved during sprint.' ELSE 'AI model validation still in progress.' END,
           SprintId
    FROM @Sprints
    UNION ALL
    SELECT NEWID(), DATEADD(day, EndOffset, SYSUTCDATETIME()), CASE WHEN Status = 5 THEN 'Review' ELSE 'Daily Scrum' END,
           CASE WHEN Status = 5 THEN CONCAT(Name, ': completed points recorded for velocity training.') ELSE CONCAT(Name, ': active work has mixed todo, in progress, and review statuses.') END,
           CASE WHEN Status = 5 THEN 'No open blocker.' ELSE 'Need more prediction samples before model evaluation.' END,
           SprintId
    FROM @Sprints;

    INSERT INTO dbo.AIPredictionLogs
        (PredictionId, PredictionType, InputData, PredictedValue, ConfidenceScore, CreatedAt, isDeleted)
    SELECT NEWID(),
           'Priority',
           CONCAT('{"storyPoints":', StoryPoints, ',"moscow":', MoSCoW, ',"text":"', REPLACE(Title, '"', '\"'), '"}'),
           CASE Priority WHEN 4 THEN 'Critical' WHEN 3 THEN 'High' WHEN 2 THEN 'Medium' ELSE 'Low' END,
           CASE Priority WHEN 4 THEN 0.91 WHEN 3 THEN 0.84 WHEN 2 THEN 0.78 ELSE 0.72 END,
           DATEADD(day, -StoryNo, SYSUTCDATETIME()),
           0
    FROM @Stories
    WHERE SprintNo IS NOT NULL
    UNION ALL
    SELECT NEWID(),
           'Assignment',
           CONCAT('{"workType":"', WorkType, '","storyPoints":', StoryPoints, ',"priority":', Priority, '}'),
           CONVERT(nvarchar(36), AssigneeId),
           CASE WorkType WHEN 'AI' THEN 0.89 WHEN 'Backend' THEN 0.84 WHEN 'Frontend' THEN 0.82 ELSE 0.79 END,
           DATEADD(day, -StoryNo, SYSUTCDATETIME()),
           0
    FROM @Stories
    WHERE AssigneeId IS NOT NULL
    UNION ALL
    SELECT NEWID(),
           'Velocity',
           CONCAT('{"closedSprintPoints":[18,22,24,21],"average":21.25,"activeCommittedPoints":34}'),
           '21.25',
           0.88,
           SYSUTCDATETIME(),
           0;

    INSERT INTO dbo.Notifications
        (NotificationId, Message, IsRead, CreatedAt, Link, ReceiverId)
    SELECT NEWID(),
           CONCAT('You are assigned to story: ', Title),
           CASE WHEN Status = 4 THEN 1 ELSE 0 END,
           DATEADD(day, -StoryNo, SYSUTCDATETIME()),
           CONCAT('/projects/MLTRC/stories/', CONVERT(nvarchar(36), StoryId)),
           AssigneeId
    FROM @Stories
    WHERE AssigneeId IS NOT NULL AND Status IN (1, 2, 3);
END

COMMIT TRANSACTION;

SELECT
    'ML logical seed completed for project MLTRC. Closed sprint velocity average should be 21.25 points.' AS Result;
GO
