﻿using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableCheckRunsClientTests
    {
        public class TheCreateMethod
        {
            IObservableGitHubClient _github;
            IObservableGitHubClient _githubAppInstallation;

            public TheCreateMethod()
            {
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task CreatesCheckRun()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var result = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }

            [GitHubAppsTest]
            public async Task CreatesCheckRunWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var result = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }
        }

        public class TheUpdateMethod
        {
            IObservableGitHubClient _github;
            IObservableGitHubClient _githubAppInstallation;

            public TheUpdateMethod()
            {
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task UpdatesCheckRun()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var checkRun = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Update the check run
                    var update = new CheckRunUpdate("new-name")
                    {
                        Status = CheckStatus.InProgress
                    };
                    var result = await _githubAppInstallation.Check.Run.Update(repoContext.RepositoryOwner, repoContext.RepositoryName, checkRun.Id, update);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                    Assert.Equal("new-name", result.Name);
                    Assert.Equal(CheckStatus.InProgress, result.Status);
                }
            }

            [GitHubAppsTest]
            public async Task UpdatesCheckRunWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var checkRun = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Update the check run
                    var update = new CheckRunUpdate("new-name")
                    {
                        Status = CheckStatus.InProgress
                    };
                    var result = await _githubAppInstallation.Check.Run.Update(repoContext.RepositoryId, checkRun.Id, update);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                    Assert.Equal("new-name", result.Name);
                    Assert.Equal(CheckStatus.InProgress, result.Status);
                }
            }
        }
    }
}