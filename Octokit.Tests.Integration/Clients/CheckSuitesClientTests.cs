﻿using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class CheckSuitesClientTests
    {
        public class TheGetMethod
        {
            IGitHubClient _github;
            IGitHubClient _githubAppInstallation;

            public TheGetMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task GetsCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Need to get a CheckSuiteId so we can test the Get method
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var checkSuite = (await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha)).CheckSuites.First();

                    // Get Check Suite by Id
                    var result = await _github.Check.Suite.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, checkSuite.Id);

                    // Check result
                    Assert.Equal(checkSuite.Id, result.Id);
                    Assert.Equal(headCommit.Sha, result.HeadSha);
                }
            }
        }

        public class TheGetAllForReferenceMethod
        {
            IGitHubClient _github;
            IGitHubClient _githubAppInstallation;

            public TheGetAllForReferenceMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task GetsAllCheckSuites()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");

                    var checkSuites = await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha);

                    Assert.NotEmpty(checkSuites.CheckSuites);
                    foreach (var checkSuite in checkSuites.CheckSuites)
                    {
                        Assert.Equal(headCommit.Sha, checkSuite.HeadSha);
                    }
                }
            }
        }

        public class TheUpdatePreferencesMethod
        {
            IGitHubClient _github;
            IGitHubClient _githubAppInstallation;

            public TheUpdatePreferencesMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task UpdatesPreferences()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var preference = new CheckSuitePreferences(new[]
                    {
                        new CheckSuitePreferenceAutoTrigger(Helper.GitHubAppId, false)
                    });

                    var response = await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryOwner, repoContext.RepositoryName, preference);

                    Assert.Equal(repoContext.RepositoryId, response.Repository.Id);
                    Assert.Equal(Helper.GitHubAppId, response.Preferences.AutoTriggerChecks[0].AppId);
                    Assert.Equal(false, response.Preferences.AutoTriggerChecks[0].Setting);
                }
            }
        }

        public class TheCreateMethod
        {
            IGitHubClient _github;
            IGitHubClient _githubAppInstallation;

            public TheCreateMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task CreatesCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Turn off auto creation of check suite for this repo
                    var preference = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(Helper.GitHubAppId, false) });
                    var response = await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryOwner, repoContext.RepositoryName, preference);

                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check suite for the feature branch
                    var newCheckSuite = new NewCheckSuite(featureBranch.Object.Sha)
                    {
                        HeadBranch = "my-feature"
                    };
                    var result = await _githubAppInstallation.Check.Suite.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckSuite);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }
        }

        public class TheRequestMethod
        {
            IGitHubClient _github;
            IGitHubClient _githubAppInstallation;

            public TheRequestMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task RequestsCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");

                    var result = await _githubAppInstallation.Check.Suite.Request(repoContext.RepositoryOwner, repoContext.RepositoryName, new CheckSuiteTriggerRequest(headCommit.Sha));

                    Assert.True(result);
                }
            }
        }
    }
}
