using AgileAi.Data.Repository;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Handlers;
using AgileAi.Domain.Interfaces;
using AgileAi.Domain.Models;
using AgileAi.Domain.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AgileAi.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //  services.AddTransient<RequestDbContext>();

            services.AddMediatR(typeof(AddGenericHandler<>).Assembly);

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IRequestHandler<AddGenericCommand<AcceptanceCriterion>, AcceptanceCriterion>, AddGenericHandler<AcceptanceCriterion>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<AcceptanceCriterion>, AcceptanceCriterion>, GetGenericHandler<AcceptanceCriterion>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<AcceptanceCriterion>, IEnumerable<AcceptanceCriterion>>, GetListGenericHandler<AcceptanceCriterion>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<AcceptanceCriterion>, AcceptanceCriterion>, PutGenericHandler<AcceptanceCriterion>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<AcceptanceCriterion>, AcceptanceCriterion>, RemoveGenericHandler<AcceptanceCriterion>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<AIPredictionLog>, AIPredictionLog>, AddGenericHandler<AIPredictionLog>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<AIPredictionLog>, AIPredictionLog>, GetGenericHandler<AIPredictionLog>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<AIPredictionLog>, IEnumerable<AIPredictionLog>>, GetListGenericHandler<AIPredictionLog>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<AIPredictionLog>, AIPredictionLog>, PutGenericHandler<AIPredictionLog>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<AIPredictionLog>, AIPredictionLog>, RemoveGenericHandler<AIPredictionLog>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<Attachment>, Attachment>, AddGenericHandler<Attachment>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Attachment>, Attachment>, GetGenericHandler<Attachment>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<Attachment>, IEnumerable<Attachment>>, GetListGenericHandler<Attachment>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Attachment>, Attachment>, PutGenericHandler<Attachment>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<Attachment>, Attachment>, RemoveGenericHandler<Attachment>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<Comment>, Comment>, AddGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Comment>, Comment>, GetGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<Comment>, IEnumerable<Comment>>, GetListGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Comment>, Comment>, PutGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<Comment>, Comment>, RemoveGenericHandler<Comment>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<Epic>, Epic>, AddGenericHandler<Epic>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Epic>, Epic>, GetGenericHandler<Epic>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<Epic>, IEnumerable<Epic>>, GetListGenericHandler<Epic>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Epic>, Epic>, PutGenericHandler<Epic>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<Epic>, Epic>, RemoveGenericHandler<Epic>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<Issue>, Issue>, AddGenericHandler<Issue>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Issue>, Issue>, GetGenericHandler<Issue>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<Issue>, IEnumerable<Issue>>, GetListGenericHandler<Issue>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Issue>, Issue>, PutGenericHandler<Issue>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<Issue>, Issue>, RemoveGenericHandler<Issue>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<KanbanColumn>, KanbanColumn>, AddGenericHandler<KanbanColumn>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<KanbanColumn>, KanbanColumn>, GetGenericHandler<KanbanColumn>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<KanbanColumn>, IEnumerable<KanbanColumn>>, GetListGenericHandler<KanbanColumn>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<KanbanColumn>, KanbanColumn>, PutGenericHandler<KanbanColumn>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<KanbanColumn>, KanbanColumn>, RemoveGenericHandler<KanbanColumn>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<Notification>, Notification>, AddGenericHandler<Notification>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Notification>, Notification>, GetGenericHandler<Notification>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<Notification>, IEnumerable<Notification>>, GetListGenericHandler<Notification>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Notification>, Notification>, PutGenericHandler<Notification>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<Notification>, Notification>, RemoveGenericHandler<Notification>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<Project>, Project>, AddGenericHandler<Project>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Project>, Project>, GetGenericHandler<Project>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<Project>, IEnumerable<Project>>, GetListGenericHandler<Project>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Project>, Project>, PutGenericHandler<Project>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<Project>, Project>, RemoveGenericHandler<Project>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<ProjectMember>, ProjectMember>, AddGenericHandler<ProjectMember>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<ProjectMember>, ProjectMember>, GetGenericHandler<ProjectMember>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<ProjectMember>, IEnumerable<ProjectMember>>, GetListGenericHandler<ProjectMember>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<ProjectMember>, ProjectMember>, PutGenericHandler<ProjectMember>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<ProjectMember>, ProjectMember>, RemoveGenericHandler<ProjectMember>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<ScrumCeremony>, ScrumCeremony>, AddGenericHandler<ScrumCeremony>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<ScrumCeremony>, ScrumCeremony>, GetGenericHandler<ScrumCeremony>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<ScrumCeremony>, IEnumerable<ScrumCeremony>>, GetListGenericHandler<ScrumCeremony>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<ScrumCeremony>, ScrumCeremony>, PutGenericHandler<ScrumCeremony>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<ScrumCeremony>, ScrumCeremony>, RemoveGenericHandler<ScrumCeremony>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<Sprint>, Sprint>, AddGenericHandler<Sprint>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Sprint>, Sprint>, GetGenericHandler<Sprint>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<Sprint>, IEnumerable<Sprint>>, GetListGenericHandler<Sprint>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Sprint>, Sprint>, PutGenericHandler<Sprint>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<Sprint>, Sprint>, RemoveGenericHandler<Sprint>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<SubTask>, SubTask>, AddGenericHandler<SubTask>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<SubTask>, SubTask>, GetGenericHandler<SubTask>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<SubTask>, IEnumerable<SubTask>>, GetListGenericHandler<SubTask>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<SubTask>, SubTask>, PutGenericHandler<SubTask>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<SubTask>, SubTask>, RemoveGenericHandler<SubTask>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<User>, User>, AddGenericHandler<User>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<User>, User>, GetGenericHandler<User>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<User>, IEnumerable<User>>, GetListGenericHandler<User>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<User>, User>, PutGenericHandler<User>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<User>, User>, RemoveGenericHandler<User>>();

            services.AddTransient<IRequestHandler<AddGenericCommand<UserStory>, UserStory>, AddGenericHandler<UserStory>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<UserStory>, UserStory>, GetGenericHandler<UserStory>>();
            services.AddTransient<IRequestHandler<GetListGenericQuery<UserStory>, IEnumerable<UserStory>>, GetListGenericHandler<UserStory>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<UserStory>, UserStory>, PutGenericHandler<UserStory>>();
            services.AddTransient<IRequestHandler<RemoveGenericCommand<UserStory>, UserStory>, RemoveGenericHandler<UserStory>>();

            services.AddTransient<IRequestHandler<AutoAssignCommand, Issue>, AutoAssignHandler>();
            services.AddTransient<IRequestHandler<GenerateDescriptionCommand, string>, GenerateDescriptionHandler>();
            services.AddTransient<IRequestHandler<PredictPriorityCommand, string>, PredictPriorityHandler>();
            services.AddTransient<IRequestHandler<PredictVelocityQuery, double>, PredictVelocityHandler>();
            services.AddTransient<IRequestHandler<CreateProjectCommand, Project>, CreateProjectHandler>();
            services.AddTransient<IRequestHandler<AddProjectMemberCommand, ProjectMember>, AddProjectMemberHandler>();
            services.AddTransient<IRequestHandler<RemoveProjectMemberCommand, ProjectMember>, RemoveProjectMemberHandler>();
            services.AddTransient<IRequestHandler<CreateSprintCommand, Sprint>, CreateSprintHandler>();
            services.AddTransient<IRequestHandler<StartSprintCommand, Sprint>, StartSprintHandler>();
            services.AddTransient<IRequestHandler<CloseSprintCommand, Sprint>, CloseSprintHandler>();
            services.AddTransient<IRequestHandler<CreateUserStoryCommand, UserStory>, CreateUserStoryHandler>();
            services.AddTransient<IRequestHandler<AssignUserStoryToSprintCommand, UserStory>, AssignUserStoryToSprintHandler>();
            services.AddTransient<IRequestHandler<CreateIssueCommand, Issue>, CreateIssueHandler>();
            services.AddTransient<IRequestHandler<AssignIssueCommand, Issue>, AssignIssueHandler>();
            services.AddTransient<IRequestHandler<MoveIssueStatusCommand, Issue>, MoveIssueStatusHandler>();
            services.AddTransient<IRequestHandler<ToggleSubTaskCommand, SubTask>, ToggleSubTaskHandler>();
        }
    }
}
