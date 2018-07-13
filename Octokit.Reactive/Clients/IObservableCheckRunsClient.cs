﻿using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Check Runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
    /// </remarks>
    public interface IObservableCheckRunsClient
    {
        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        IObservable<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun);

        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        IObservable<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun);

        /// <summary>
        /// Updates a Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#update-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="checkRunUpdate">The updates to the check run</param>
        IObservable<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate);

        /// <summary>
        /// Updates a Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#update-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="checkRunUpdate">The updates to the check run</param>
        IObservable<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        IObservable<CheckRunsResponse> GetAllForReference(string owner, string name, string reference);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        IObservable<CheckRunsResponse> GetAllForReference(long repositoryId, string reference);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        IObservable<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        IObservable<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        IObservable<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        IObservable<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options);

        /// <summary>
        /// Lists check runs for a check suite using its Id.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        IObservable<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId);

        /// <summary>
        /// Lists check runs for a check suite using its Id.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        IObservable<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId);

        /// <summary>
        /// Lists check runs for a check suite using its Id.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        IObservable<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest);

        /// <summary>
        /// Lists check runs for a check suite using its Id.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        IObservable<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest);

        /// <summary>
        /// Lists check runs for a check suite using its Id.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        IObservable<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);

        /// <summary>
        /// Lists check runs for a check suite using its Id.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        IObservable<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);
    }
}